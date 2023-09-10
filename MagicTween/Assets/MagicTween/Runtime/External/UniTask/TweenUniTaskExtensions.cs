#if MAGICTWEEN_SUPPORT_UNITASK
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public enum CancelBehaviour
    {
        Kill,
        Complete,
        CompleteAndKill,
        CancelAwait,
        KillAndCancelAwait,
        CompleteAndCancelAwait,
        CompleteAndKillAndCancelAwait,
    }

    public static class TweenUniTaskExtensions
    {
        enum CallbackType
        {
            Kill,
            Complete,
            Pause,
            Play,
            StepComplete
        }

        public static TweenAwaiter GetAwaiter<T>(this T tween)
            where T : struct, ITweenHandle
        {
            return new TweenAwaiter(tween.AsUnitTween());
        }

        public static UniTask WithCancellation<T>(this T tween, CancellationToken cancellationToken)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour.KillAndCancelAwait, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask AwaitForKill<T>(this T tween, CancelBehaviour tweenCancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), tweenCancelBehaviour, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask AwaitForComplete<T>(this T tween, CancelBehaviour tweenCancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), tweenCancelBehaviour, cancellationToken, CallbackType.Complete, out var token), token);
        }

        public static UniTask AwaitForPause<T>(this T tween, CancelBehaviour tweenCancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), tweenCancelBehaviour, cancellationToken, CallbackType.Pause, out var token), token);
        }

        public static UniTask AwaitForPlay<T>(this T tween, CancelBehaviour tweenCancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), tweenCancelBehaviour, cancellationToken, CallbackType.Play, out var token), token);
        }

        public static UniTask AwaitForStepComplete<T>(this T tween, CancelBehaviour tweenCancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), tweenCancelBehaviour, cancellationToken, CallbackType.StepComplete, out var token), token);
        }

        public struct TweenAwaiter : ICriticalNotifyCompletion
        {
            readonly Tween tween;

            public bool IsCompleted => !tween.IsActive();

            public TweenAwaiter(Tween tween)
            {
                this.tween = tween;
            }

            public TweenAwaiter GetAwaiter()
            {
                return this;
            }

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                TweenWorld.EntityManager.GetComponentData<TweenCallbackActions>(tween.GetEntity()).onKill = PooledTweenCallback.Create(continuation);
            }
        }

        sealed class TweenConfiguredSource : IUniTaskSource, ITaskPoolNode<TweenConfiguredSource>
        {
            static TaskPool<TweenConfiguredSource> pool;
            TweenConfiguredSource nextNode;
            public ref TweenConfiguredSource NextNode => ref nextNode;

            static TweenConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(TweenConfiguredSource), () => pool.Size);
            }

            readonly Action onCompleteCallbackDelegate;
            readonly Action onUpdateDelegate;

            Tween tween;
            CancelBehaviour cancelBehaviour;
            CancellationToken cancellationToken;
            CallbackType callbackType;
            bool canceled;

            Action originalUpdateAction;
            Action originalCompleteAction;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            TweenConfiguredSource()
            {
                onCompleteCallbackDelegate = OnCompleteCallbackDelegate;
                onUpdateDelegate = OnUpdate;
            }

            public static IUniTaskSource Create(Tween tween, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken, CallbackType callbackType, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    DoCancelBeforeCreate(tween, cancelBehaviour);
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new TweenConfiguredSource();
                }

                result.tween = tween;
                result.cancelBehaviour = cancelBehaviour;
                result.cancellationToken = cancellationToken;
                result.callbackType = callbackType;

                var callbacks = tween.GetOrAddCallbackActions();

                result.originalUpdateAction = callbacks.onUpdate;
                result.canceled = false;

                if (result.originalUpdateAction == result.onUpdateDelegate)
                {
                    result.originalUpdateAction = null;
                }

                callbacks.onUpdate = result.onUpdateDelegate;

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        result.originalCompleteAction = callbacks.onKill;
                        callbacks.onKill = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Complete:
                        result.originalCompleteAction = callbacks.onComplete;
                        callbacks.onComplete = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Pause:
                        result.originalCompleteAction = callbacks.onPause;
                        callbacks.onPause = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Play:
                        result.originalCompleteAction = callbacks.onPlay;
                        callbacks.onPlay = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.StepComplete:
                        result.originalCompleteAction = callbacks.onStepComplete;
                        callbacks.onStepComplete = result.onCompleteCallbackDelegate;
                        break;
                    default:
                        break;
                }

                if (result.originalCompleteAction == result.onCompleteCallbackDelegate)
                {
                    result.originalCompleteAction = null;
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            void OnCompleteCallbackDelegate()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    if (cancelBehaviour == CancelBehaviour.KillAndCancelAwait
                        || cancelBehaviour == CancelBehaviour.CompleteAndCancelAwait
                        || cancelBehaviour == CancelBehaviour.CompleteAndKillAndCancelAwait
                        || cancelBehaviour == CancelBehaviour.CancelAwait)
                    {
                        canceled = true;
                    }
                }
                if (canceled)
                {
                    core.TrySetCanceled(cancellationToken);
                }
                else
                {
                    originalCompleteAction?.Invoke();
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            void OnUpdate()
            {
                originalUpdateAction?.Invoke();

                if (!cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var callbacks = TweenWorld.EntityManager.GetComponentData<TweenCallbackActions>(tween.GetEntity());

                switch (cancelBehaviour)
                {
                    case CancelBehaviour.Kill:
                    default:
                        tween.Kill();
                        break;
                    case CancelBehaviour.KillAndCancelAwait:
                        canceled = true;
                        tween.Kill();
                        break;
                    case CancelBehaviour.Complete:
                        tween.Complete();
                        break;
                    case CancelBehaviour.CompleteAndCancelAwait:
                        canceled = true;
                        tween.Complete();
                        break;
                    case CancelBehaviour.CompleteAndKill:
                        tween.CompleteAndKill();
                        break;
                    case CancelBehaviour.CompleteAndKillAndCancelAwait:
                        canceled = true;
                        tween.CompleteAndKill();
                        break;
                    case CancelBehaviour.CancelAwait:
                        switch (callbackType)
                        {
                            case CallbackType.Kill:
                                callbacks.onKill = originalCompleteAction;
                                break;
                            case CallbackType.Complete:
                                callbacks.onComplete = originalCompleteAction;
                                break;
                            case CallbackType.Pause:
                                callbacks.onPause = originalCompleteAction;
                                break;
                            case CallbackType.Play:
                                callbacks.onPlay = originalCompleteAction;
                                break;
                            case CallbackType.StepComplete:
                                callbacks.onStepComplete = originalCompleteAction;
                                break;
                            default:
                                break;
                        }

                        core.TrySetCanceled(cancellationToken);
                        break;
                }
            }

            static void DoCancelBeforeCreate(Tween tween, CancelBehaviour tweenCancelBehaviour)
            {

                switch (tweenCancelBehaviour)
                {
                    case CancelBehaviour.Kill:
                    case CancelBehaviour.KillAndCancelAwait:
                    default:
                        tween.Kill();
                        break;
                    case CancelBehaviour.Complete:
                    case CancelBehaviour.CompleteAndCancelAwait:
                        tween.Complete();
                        break;
                    case CancelBehaviour.CompleteAndKillAndCancelAwait:
                        tween.CompleteAndKill();
                        break;
                    case CancelBehaviour.CancelAwait:
                        break;
                }
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                var callbacks = tween.GetOrAddCallbackActions();
                callbacks.onUpdate = originalUpdateAction;

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        callbacks.onKill = originalCompleteAction;
                        break;
                    case CallbackType.Complete:
                        callbacks.onComplete = originalCompleteAction;
                        break;
                    case CallbackType.Pause:
                        callbacks.onPause = originalCompleteAction;
                        break;
                    case CallbackType.Play:
                        callbacks.onPlay = originalCompleteAction;
                        break;
                    case CallbackType.StepComplete:
                        callbacks.onStepComplete = originalCompleteAction;
                        break;
                    default:
                        break;
                }

                tween = default;
                cancellationToken = default;
                originalUpdateAction = default;
                originalCompleteAction = default;
                return pool.TryPush(this);
            }
        }
    }

    sealed class PooledTweenCallback
    {
        static readonly ConcurrentQueue<PooledTweenCallback> pool = new ConcurrentQueue<PooledTweenCallback>();

        readonly Action runDelegate;

        Action continuation;

        PooledTweenCallback()
        {
            runDelegate = Run;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Create(Action continuation)
        {
            if (!pool.TryDequeue(out var item))
            {
                item = new PooledTweenCallback();
            }

            item.continuation = continuation;
            return item.runDelegate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            var call = continuation;
            continuation = null;
            if (call != null)
            {
                pool.Enqueue(this);
                call.Invoke();
            }
        }
    }
}
#endif