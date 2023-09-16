using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    using static TweenWorld;

    [BurstCompile]
    internal static class TweenHelper
    {
        [BurstCompile]
        public static float GetDuration(ref EntityManager entityManager, in Entity entity)
        {
            var duration = entityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var delay = entityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var loops = entityManager.GetComponentData<TweenParameterLoops>(entity).value;
            var playbackSpeed = entityManager.GetComponentData<TweenParameterPlaybackSpeed>(entity).value;
            return GetDuration(duration, loops, delay, playbackSpeed);
        }

        [BurstCompile]
        public static float GetDuration(float duration, int loops, float delay, float playbackSpeed)
        {
            if (loops < 0) return -1f;
            if (playbackSpeed == 0f) return -1f;
            return (delay + duration * loops) / playbackSpeed;
        }

        public static bool TryPlay(in Entity entity, out bool started)
        {
            return TryPlayCore(ref EntityManagerRef, entity, out started);
        }

        [BurstCompile]
        static bool TryPlayCore(ref EntityManager entityManager, in Entity entity, out bool started)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is not (TweenStatusType.WaitingForStart or TweenStatusType.Paused))
            {
                started = false;
                return false;
            }

            var delay = entityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var position = entityManager.GetComponentData<TweenPosition>(entity).value;
            var time = position - delay;

            if (time < 0f)
            {
                started = false;
                status.value = TweenStatusType.Delayed;
            }
            else
            {
                started = status.value == TweenStatusType.WaitingForStart;
                status.value = TweenStatusType.Playing;
                entityManager.SetComponentData(entity, new TweenStartedFlag(true));
            }

            entityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryPause(in Entity entity)
        {
            return TryPauseCore(ref EntityManagerRef, entity);
        }

        [BurstCompile]
        static bool TryPauseCore(ref EntityManager entityManager, in Entity entity)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is not (TweenStatusType.Delayed or TweenStatusType.Playing)) return false;

            status.value = TweenStatusType.Paused;
            entityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryKill(in Entity entity)
        {
            var result = TryKillCore(ref EntityManagerRef, entity);
            if (result) CleanupSystem.Enqueue(entity);
            return result;
        }

        [BurstCompile]
        static bool TryKillCore(ref EntityManager entityManager, in Entity entity)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is TweenStatusType.Killed) return false;

            status.value = TweenStatusType.Killed;
            entityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryComplete(in Entity entity)
        {
            return TryCompleteCore(ref EntityManagerRef, entity, out var delay, out var loops);
        }

        public static bool TryComplete<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var result = TryCompleteCore(ref EntityManagerRef, entity, out var delay, out var loops);

            if (!result)
            {
                currentValue = default;
                return false;
            }

            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
            }
            else
            {
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
            }

            return true;
        }

        [BurstCompile]
        static bool TryCompleteCore(ref EntityManager entityManager, in Entity entity, out float delay, out int loops)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is TweenStatusType.Completed or TweenStatusType.Killed)
            {
                delay = default;
                loops = default;
                return false;
            }

            loops = entityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0)
            {
                delay = default;
                return false;
            }

            delay = entityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = entityManager.GetComponentData<TweenParameterDuration>(entity).value;

            status.value = TweenStatusType.Completed;

            entityManager.SetComponentData(entity, status);
            entityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            entityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            entityManager.SetComponentData(entity, new TweenStartedFlag(true));

            return true;
        }

        public static bool TryCompleteAndKill(in Entity entity)
        {
            var result = TryCompleteAndKillCore(ref EntityManagerRef, entity, out var delay, out var loops);
            if (result) CleanupSystem.Enqueue(entity);
            return result;
        }

        public static bool TryCompleteAndKill<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var result = TryCompleteAndKillCore(ref EntityManagerRef, entity, out var delay, out var loops);
            if (!result)
            {
                currentValue = default;
                return false;
            }
            
            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
            }
            else
            {
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
            }

            CleanupSystem.Enqueue(entity);

            return true;
        }

        [BurstCompile]
        static bool TryCompleteAndKillCore(ref EntityManager entityManager, in Entity entity, out float delay, out int loops)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is TweenStatusType.Completed or TweenStatusType.Killed)
            {
                delay = default;
                loops = default;
                return false;
            }

            loops = entityManager.GetComponentData<TweenParameterLoops>(entity).value;
            if (loops < 0)
            {
                delay = default;
                return false;
            }

            delay = entityManager.GetComponentData<TweenParameterDelay>(entity).value;
            var duration = entityManager.GetComponentData<TweenParameterDuration>(entity).value;

            status.value = TweenStatusType.Killed;

            entityManager.SetComponentData(entity, status);
            entityManager.SetComponentData(entity, new TweenPosition(duration * loops + delay));
            entityManager.SetComponentData(entity, new TweenCompletedLoops(loops));
            entityManager.SetComponentData(entity, new TweenStartedFlag(true));

            return true;
        }

        public static bool TryRestart(in Entity entity)
        {
            return TryRestartCore(ref EntityManagerRef, entity, out var delay, out var loops);
        }

        public static bool TryRestart<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var result = TryRestartCore(ref EntityManagerRef, entity, out var delay, out var loops);
            if (!result)
            {
                currentValue = default;
                return false;
            }

            var plugin = default(TPlugin);
            var loopType = EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var inverted = EntityManager.GetComponentData<TweenInvertFlag>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    inverted
                );
            }
            else
            {
                currentValue = plugin.Evaluate(
                    entity,
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    inverted
                );
            }

            return true;
        }

        [BurstCompile]
        static bool TryRestartCore(ref EntityManager entityManager, in Entity entity, out float delay, out int loops)
        {
            var status = entityManager.GetComponentData<TweenStatus>(entity);
            if (status.value is TweenStatusType.Killed)
            {
                delay = default;
                loops = default;
                return false;
            }

            loops = entityManager.GetComponentData<TweenParameterLoops>(entity).value;
            delay = entityManager.GetComponentData<TweenParameterDelay>(entity).value;
            status.value = delay > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            entityManager.SetComponentData(entity, status);
            entityManager.SetComponentData(entity, new TweenPosition(0f));
            entityManager.SetComponentData(entity, new TweenCompletedLoops(0));
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