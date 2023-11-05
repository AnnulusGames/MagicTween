#if MAGICTWEEN_SUPPORT_UNITASK
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using MagicTween.Core;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public enum CancelBehaviour : byte
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

        public static UniTask.Awaiter GetAwaiter<T>(this T tween)
            where T : struct, ITweenHandle
        {
            return AwaitForKill(tween).GetAwaiter();
        }

        public static UniTask WithCancellation<T>(this T tween, CancellationToken cancellationToken)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour.KillAndCancelAwait, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask AwaitForKill<T>(this T tween, CancelBehaviour CancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask AwaitForComplete<T>(this T tween, CancelBehaviour CancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour, cancellationToken, CallbackType.Complete, out var token), token);
        }

        public static UniTask AwaitForPause<T>(this T tween, CancelBehaviour CancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour, cancellationToken, CallbackType.Pause, out var token), token);
        }

        public static UniTask AwaitForPlay<T>(this T tween, CancelBehaviour CancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour, cancellationToken, CallbackType.Play, out var token), token);
        }

        public static UniTask AwaitForStepComplete<T>(this T tween, CancelBehaviour CancelBehaviour = CancelBehaviour.KillAndCancelAwait, CancellationToken cancellationToken = default)
            where T : struct, ITweenHandle
        {
            AssertTween.IsValid(tween);
            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween.AsUnitTween(), CancelBehaviour, cancellationToken, CallbackType.StepComplete, out var token), token);
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

            readonly FastAction onCompleteCallbackDelegate;

            Tween tween;
            CancelBehaviour cancelBehaviour;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationRegistration;
            CallbackType callbackType;
            bool canceled;

            FastAction originalCompleteAction;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            TweenConfiguredSource()
            {
                onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
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
                result.canceled = false;

                var callbacks = tween.GetOrAddCallbackActions();

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

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(x =>
                    {
                        var source = (TweenConfiguredSource)x;
                        switch (source.cancelBehaviour)
                        {
                            case CancelBehaviour.Kill:
                            default:
                                source.tween.Kill();
                                break;
                            case CancelBehaviour.KillAndCancelAwait:
                                source.canceled = true;
                                source.tween.Kill();
                                break;
                            case CancelBehaviour.Complete:
                                source.tween.Complete();
                                break;
                            case CancelBehaviour.CompleteAndCancelAwait:
                                source.canceled = true;
                                source.tween.Complete();
                                break;
                            case CancelBehaviour.CompleteAndKill:
                                source.tween.CompleteAndKill();
                                break;
                            case CancelBehaviour.CompleteAndKillAndCancelAwait:
                                source.canceled = true;
                                source.tween.CompleteAndKill();
                                break;
                            case CancelBehaviour.CancelAwait:
                                source.RestoreOriginalCallback();
                                source.core.TrySetCanceled(source.cancellationToken);
                                break;
                        }
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            void OnCompleteCallbackDelegate()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    if (this.cancelBehaviour == CancelBehaviour.KillAndCancelAwait
                        || this.cancelBehaviour == CancelBehaviour.CompleteAndKillAndCancelAwait
                        || this.cancelBehaviour == CancelBehaviour.CompleteAndCancelAwait
                        || this.cancelBehaviour == CancelBehaviour.CancelAwait)
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

            static void DoCancelBeforeCreate(Tween tween, CancelBehaviour cancelBehaviour)
            {
                switch (cancelBehaviour)
                {
                    case CancelBehaviour.Kill:
                    case CancelBehaviour.KillAndCancelAwait:
                    default:
                        tween.Kill();
                        break;
                    case CancelBehaviour.CompleteAndKill:
                    case CancelBehaviour.CompleteAndKillAndCancelAwait:
                        tween.CompleteAndKill();
                        break;
                    case CancelBehaviour.Complete:
                    case CancelBehaviour.CompleteAndCancelAwait:
                        tween.Complete();
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
                cancellationRegistration.Dispose();

                RestoreOriginalCallback();

                tween = default;
                cancellationToken = default;
                originalCompleteAction = default;
                return pool.TryPush(this);
            }

            void RestoreOriginalCallback()
            {
                var callbacks = tween.GetOrAddCallbackActions();
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
            }
        }
    }

    sealed class PooledTweenCallback
    {
        static readonly ConcurrentQueue<PooledTweenCallback> pool = new ConcurrentQueue<PooledTweenCallback>();

        readonly FastAction runDelegate;

        Action continuation;

        PooledTweenCallback()
        {
            runDelegate = new(Run);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FastAction Create(Action continuation)
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