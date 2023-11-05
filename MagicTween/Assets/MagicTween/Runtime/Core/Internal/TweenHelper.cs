using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
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
            return TryPlayCore(ref ECSCache.EntityManager, entity, out started);
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
            return TryPauseCore(ref ECSCache.EntityManager, entity);
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
            var result = TryKillCore(ref ECSCache.EntityManager, entity);
            if (result) ECSCache.CleanupSystem.Enqueue(entity);
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
            return TryCompleteCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);
        }

        public static bool TryComplete<TValue, TOptions, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        {
            var result = TryCompleteCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);

            if (!result)
            {
                currentValue = default;
                return false;
            }

            var plugin = default(TPlugin);
            var loopType = ECSCache.EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = ECSCache.EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = ECSCache.EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = ECSCache.EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = ECSCache.EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
                );
            }
            else
            {
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
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
            var result = TryCompleteAndKillCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);
            if (result) ECSCache.CleanupSystem.Enqueue(entity);
            return result;
        }

        public static bool TryCompleteAndKill<TValue, TOptions, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        {
            var result = TryCompleteAndKillCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);
            if (!result)
            {
                currentValue = default;
                return false;
            }

            var plugin = default(TPlugin);
            var loopType = ECSCache.EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var invertMode = ECSCache.EntityManager.GetComponentData<TweenParameterInvertMode>(entity).value;
            var isRelative = ECSCache.EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = ECSCache.EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = ECSCache.EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
                );
            }
            else
            {
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    invertMode != InvertMode.None
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
                );
            }

            ECSCache.CleanupSystem.Enqueue(entity);

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
            return TryRestartCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);
        }

        public static bool TryRestart<TValue, TOptions, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        {
            var result = TryRestartCore(ref ECSCache.EntityManager, entity, out var delay, out var loops);
            if (!result)
            {
                currentValue = default;
                return false;
            }

            var plugin = default(TPlugin);
            var loopType = ECSCache.EntityManager.GetComponentData<TweenParameterLoopType>(entity).value;
            var inverted = ECSCache.EntityManager.GetComponentData<TweenInvertFlag>(entity).value;
            var isRelative = ECSCache.EntityManager.GetComponentData<TweenParameterIsRelative>(entity).value;
            var ease = ECSCache.EntityManager.GetComponentData<TweenParameterEase>(entity).value;

            if (ease == Ease.Custom)
            {
                var customCurve = ECSCache.EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ref customCurve, loops, loopType),
                    isRelative,
                    inverted
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
                );
            }
            else
            {
                var context = new TweenEvaluationContext(
                    GetProgressOnCompleted(ease, loops, loopType),
                    isRelative,
                    inverted
                );
                currentValue = plugin.Evaluate(
                    entity,
                    ref ECSCache.EntityManager,
                    context
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
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = ECSCache.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnKill(in Entity entity)
        {
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = ECSCache.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnCompleteAndOnKill(in Entity entity)
        {
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = ECSCache.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnStartAndOnPlay(in Entity entity, bool started)
        {
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = ECSCache.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                if (started) callbacks.onStart?.Invoke();
                callbacks.onPlay?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnPause(in Entity entity)
        {
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = ECSCache.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onPause?.Invoke();
            }
        }
    }
}