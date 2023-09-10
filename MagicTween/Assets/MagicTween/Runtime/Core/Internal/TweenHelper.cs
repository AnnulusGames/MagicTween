using System.Runtime.CompilerServices;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    internal static class TweenHelper
    {
        public static float GetDuration(in Entity entity)
        {
            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var playbackSpeed = TweenWorld.EntityManager.GetComponentData<TweenPlaybackSpeed>(entity);
            return GetDuration(clip, playbackSpeed);
        }

        public static float GetDuration(in TweenClip clip, TweenPlaybackSpeed playbackSpeed)
        {
            if (clip.loops < 0) return -1f;
            if (playbackSpeed.speed == 0) return -1f;
            return (clip.delay + clip.duration * clip.loops) / playbackSpeed.speed;
        }

        public static bool TryPlay(in Entity entity, out bool started)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is not (TweenStatusType.WaitingForStart or TweenStatusType.Paused))
            {
                started = false;
                return false;
            }

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);
            var time = position.position - clip.delay;

            if (time < 0f)
            {
                started = false;
                status.status = TweenStatusType.Delayed;
            }
            else
            {
                started = status.status == TweenStatusType.WaitingForStart;
                status.status = TweenStatusType.Playing;
                TweenWorld.EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });
            }

            TweenWorld.EntityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryPause(in Entity entity)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is not (TweenStatusType.Delayed or TweenStatusType.Playing)) return false;

            status.status = TweenStatusType.Paused;
            TweenWorld.EntityManager.SetComponentData(entity, status);
            return true;
        }

        public static bool TryKill(in Entity entity)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed) return false;

            status.status = TweenStatusType.Killed;
            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.CleanupSystem.Enqueue(entity);
            return true;
        }

        public static bool TryComplete(in Entity entity)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed) return false;

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);
            if (clip.loops < 0) return false;

            status.status = TweenStatusType.Completed;
            position.position = clip.duration * clip.loops + clip.delay;
            position.completedLoops = clip.loops;

            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.EntityManager.SetComponentData(entity, position);
            TweenWorld.EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });
            return true;
        }

        public static bool TryComplete<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed)
            {
                currentValue = default;
                return false;
            }

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0)
            {
                currentValue = default;
                return false;
            }
            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);

            var plugin = default(TPlugin);
            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(entity);
            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(entity);

            status.status = TweenStatusType.Completed;
            position.position = clip.duration * clip.loops + clip.delay;
            position.completedLoops = clip.loops;

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                parameters.isRelative,
                parameters.invertMode != InvertMode.None
            );

            TweenWorld.EntityManager.SetComponentData(entity, position);
            TweenWorld.EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });

            return true;
        }

        public static bool TryCompleteAndKill(in Entity entity)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed) return false;

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0) return false;

            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);

            status.status = TweenStatusType.Killed;
            position.position = clip.duration + clip.duration;
            position.completedLoops = clip.loops;

            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.EntityManager.SetComponentData(entity, position);
            TweenWorld.EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });

            TweenWorld.CleanupSystem.Enqueue(entity);

            return true;
        }

        public static bool TryCompleteAndKill<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Completed or TweenStatusType.Killed)
            {
                currentValue = default;
                return false;
            }

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            if (clip.loops < 0)
            {
                currentValue = default;
                return false;
            }

            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);

            status.status = TweenStatusType.Killed;
            position.position = clip.duration + clip.duration;
            position.completedLoops = clip.loops;

            var plugin = default(TPlugin);
            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(entity);
            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(entity);

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                parameters.isRelative,
                parameters.invertMode != InvertMode.None
            );

            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.EntityManager.SetComponentData(entity, position);
            TweenWorld.EntityManager.SetComponentData(entity, new TweenStartedFlag() { value = true });

            TweenWorld.CleanupSystem.Enqueue(entity);

            return true;
        }

        public static bool TryRestart(in Entity entity)
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed) return false;

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);

            position.position = 0f;
            position.completedLoops = 0;
            status.status = clip.delay > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.EntityManager.SetComponentData(entity, position);
            return true;
        }

        public static bool TryRestart<TValue, TPlugin>(in Entity entity, out TValue currentValue)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            if (status.status is TweenStatusType.Invalid or TweenStatusType.Killed or TweenStatusType.WaitingForStart)
            {
                currentValue = default;
                return false;
            }

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var position = TweenWorld.EntityManager.GetComponentData<TweenPosition>(entity);
            var inverted = TweenWorld.EntityManager.GetComponentData<TweenInvertFlag>(entity).value;

            position.position = 0f;
            position.completedLoops = 0;
            status.status = clip.duration > 0f ? TweenStatusType.Delayed : TweenStatusType.Playing;

            var plugin = default(TPlugin);
            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(entity);
            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(entity);

            currentValue = plugin.Evaluate(
                entity,
                GetProgressOnCompleted(easing, clip.loops, clip.loopType),
                parameters.isRelative,
                inverted
            );

            TweenWorld.EntityManager.SetComponentData(entity, status);
            TweenWorld.EntityManager.SetComponentData(entity, position);

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
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = TweenWorld.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnKill(in Entity entity)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = TweenWorld.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnCompleteAndOnKill(in Entity entity)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = TweenWorld.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onStepComplete?.Invoke();
                callbacks.onComplete?.Invoke();
                callbacks.onKill?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnStartAndOnPlay(in Entity entity, bool started)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = TweenWorld.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                if (started) callbacks.onStart?.Invoke();
                callbacks.onPlay?.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCallOnPause(in Entity entity)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                var callbacks = TweenWorld.EntityManager.GetComponentObject<TweenCallbackActions>(entity);
                callbacks.onPause?.Invoke();
            }
        }
    }
}