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
            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            var playbackSpeed = EntityManager.GetComponentData<TweenPlaybackSpeed>(entity);
            return GetDuration(clip, playbackSpeed);
        }

        public static float GetDuration(in TweenClip clip, TweenPlaybackSpeed playbackSpeed)
        {
            if (clip.loops < 0) return -1f;
            if (playbackSpeed.value == 0) return -1f;
            return (clip.delay + clip.duration * clip.loops) / playbackSpeed.value;
        }

        public static bool TryPlay(in Entity entity, out bool started)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is not (TweenStatusType.WaitingForStart or TweenStatusType.Paused))
            {
                started = false;
                return false;
            }

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            var position = EntityManager.GetComponentData<TweenPosition>(entity);
            var time = position.value - clip.delay;

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

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0) return false;

            status.status = TweenStatusType.Completed;
            var position = clip.duration * clip.loops + clip.delay;
            var completedLoops = clip.loops;

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(position));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(completedLoops));
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

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0)
            {
                currentValue = default;
                return false;
            }

            var plugin = default(TPlugin);
            var invertMode = EntityManager.GetComponentData<TweenInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenIsRelativeFlag>(entity).value;
            var easing = EntityManager.GetComponentData<TweenEasing>(entity);

            status.status = TweenStatusType.Completed;
            var position = clip.duration * clip.loops + clip.delay;
            var completedLoops = clip.loops;

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                isRelative,
                invertMode != InvertMode.None
            );

            EntityManager.SetComponentData(entity, new TweenPosition(position));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(completedLoops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));

            return true;
        }

        public static bool TryCompleteAndKill(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed) return false;

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0) return false;

            status.status = TweenStatusType.Killed;
            var position = clip.duration + clip.duration;
            var completedLoops = clip.loops;

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(position));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(completedLoops));
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

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0)
            {
                currentValue = default;
                return false;
            }

            status.status = TweenStatusType.Killed;
            var position = clip.duration + clip.duration;
            var completedLoops = clip.loops;

            var plugin = default(TPlugin);
            var invertMode = EntityManager.GetComponentData<TweenInvertMode>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenIsRelativeFlag>(entity).value;
            var easing = EntityManager.GetComponentData<TweenEasing>(entity);

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                isRelative,
                invertMode != InvertMode.None
            );

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(position));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(completedLoops));
            EntityManager.SetComponentData(entity, new TweenStartedFlag(true));

            CleanupSystem.Enqueue(entity);

            return true;
        }

        public static bool TryRestart(in Entity entity)
        {
            var status = EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed) return false;

            var clip = EntityManager.GetComponentData<TweenClip>(entity);

            status.status = clip.delay > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

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

            var clip = EntityManager.GetComponentData<TweenClip>(entity);
            status.status = clip.duration > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            var plugin = default(TPlugin);
            var inverted = EntityManager.GetComponentData<TweenInvertFlag>(entity).value;
            var isRelative = EntityManager.GetComponentData<TweenIsRelativeFlag>(entity).value;
            var easing = EntityManager.GetComponentData<TweenEasing>(entity);

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                isRelative,
                inverted
            );

            EntityManager.SetComponentData(entity, status);
            EntityManager.SetComponentData(entity, new TweenPosition(0f));
            EntityManager.SetComponentData(entity, new TweenCompletedLoops(0));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float GetProgressOnCompleted(in TweenEasing easing, int loops, LoopType loopType)
        {
            var easedValue = easing.GetEasedValue(1f);
            switch (loopType)
            {
                case LoopType.Restart:
                    easedValue = easing.GetEasedValue(1f);
                    break;
                case LoopType.Yoyo:
                    easedValue = easing.GetEasedValue(1f);
                    if (loops % 2 == 1) easedValue = 1f - easedValue;
                    break;
                case LoopType.Incremental:
                    easedValue = loops * easing.GetEasedValue(1f);
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