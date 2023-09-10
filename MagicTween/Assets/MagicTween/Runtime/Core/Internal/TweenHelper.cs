using System.Runtime.CompilerServices;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    using static TweenWorld;

    internal static class TweenHelper
    {
        public static float GetDuration(in Entity entity)
        {
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            var playbackSpeed = EntityManager.GetComponentData<TweenParameterPlaybackSpeed>(entity).value;
            return GetDuration(duration, loops, delay, playbackSpeed);
        }

        public static float GetDuration(float duration, int loops, float delay, float playbackSpeed)
        {
            if (loops < 0) return -1f;
            if (playbackSpeed == 0f) return -1f;
            return (delay + duration * loops) / playbackSpeed;
        }

        public static bool TryPlay(in Entity entity, out bool started)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is not (TweenStatusType.WaitingForStart or TweenStatusType.Paused))
            {
                started = false;
                return false;
            }

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var position = EntityManager.GetComponentData<TweenPosition>(entity).value;
            var time = position - delay;

            if (time < 0f)
            {
                started = false;
                status.status = TweenStatusType.Delayed;
            }
            else
            {
                started = status.status == TweenStatusType.WaitingForStart;
                status.status = TweenStatusType.Playing;
                EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });
            }

            EntityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryPause(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is not (TweenStatusType.Delayed or TweenStatusType.Playing)) return false;

            status.status = TweenStatusType.Paused;
            EntityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryKill(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed) return false;

            status.status = TweenStatusType.Killed;
            EntityManager.SetComponentData(entity, status);
            CleanupSystem.Enqueue(entity);
            return true;
        }

        public static bool TryComplete(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed) return false;

            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0) return false;

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            status.status = TweenStatusType.Completed;

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));
            return true;
        }

        public static bool TryComplete<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed)
            {
                currentValue = default;
                return false;
            }

            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0)
            {
                currentValue = default;
                return false;
            }

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;
            var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;

            status.status = TweenStatusType.Completed;
            
            currentValue = plugin.Evaluate(
                entity,
                ease == Ease.Custom ? GetProgressOnCompleted(ref customCurve, loops, loopType) : GetProgressOnCompleted(ease, loops, loopType),
                isRelative,
                invertMode != InvertMode.None
            );

            EntityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));

            return true;
        }

        public static bool TryCompleteAndKill(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed) return false;

            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0) return false;

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            status.status = TweenStatusType.Killed;

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));

            CleanupSystem.Enqueue(entity);

            return true;
        }

        public static bool TryCompleteAndKill<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed)
            {
                currentValue = default;
                return false;
            }


            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0)
            {
                currentValue = default;
                return false;
            }

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            status.status = TweenStatusType.Killed;

            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;
            var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;

            currentValue = plugin.Evaluate(
                entity,
                ease == Ease.Custom ? GetProgressOnCompleted(ref customCurve, loops, loopType) : GetProgressOnCompleted(ease, loops, loopType),
                isRelative,
                invertMode != InvertMode.None
            );

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));

            CleanupSystem.Enqueue(entity);

            return true;
        }

        public static bool TryRestart(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed) return false;

            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;
            status.status = delay > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(0f));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(0));
            return true;
        }

        public static bool TryRestart<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed or TweenStatusType.WaitingForStart)
            {
                currentValue = default;
                return false;
            }

            var loops = EntityManager.GetComponentData<TweenParameterLoops>(entity).value;
            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;

            status.status = delay > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var inverted = EntityManager.GetComponentData<TweenInvertFlag>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;
            var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;

            currentValue = plugin.Evaluate(
                entity,
                ease == Ease.Custom ? GetProgressOnCompleted(ref customCurve, loops, loopType) : GetProgressOnCompleted(ease, loops, loopType),
                isRelative,
                inverted
            );

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(0f));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(0));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float GetProgressOnCompleted(Ease ease, int loops, LoopType loopType)
        {
            float easedValue = default;
            switch (loopType)
            {
                case LoopType.Restart:
                    easedValue = EaseUtility.Evaluate(1f, ease);
                    break;
                case LoopType.Yoyo:
                    easedValue = EaseUtility.Evaluate(1f, ease);
                    if (loops % 2 == 1) easedValue = 1f - easedValue;
                    break;
                case LoopType.Incremental:
                    easedValue = loops * EaseUtility.Evaluate(1f, ease);
                    break;
            }
            return easedValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float GetProgressOnCompleted(ref ValueAnimationCurve customCurve, int loops, LoopType loopType)
        {
            float easedValue = default;
            switch (loopType)
            {
                case LoopType.Restart:
                    easedValue = customCurve.Evaluate(1f);
                    break;
                case LoopType.Yoyo:
                    easedValue = customCurve.Evaluate(1f);
                    if (loops % 2 == 1) easedValue = 1f - easedValue;
                    break;
                case LoopType.Incremental:
                    easedValue = loops * customCurve.Evaluate(1f);
                    break;
            }
            return easedValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnComplete(in Entity entity)
        {
            if (EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnKill(in Entity entity)
        {
            if (EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnCompleteAndOnKill(in Entity entity)
        {
            if (EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnStartAndOnPlay(in Entity entity, bool started)
        {
            if (EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                if (started) callbacks.onStart?.Invoke();
                callbacks.onPlay?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnPause(in Entity entity)
        {
            if (EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onPause?.Invoke();
            }
        }
    }
}