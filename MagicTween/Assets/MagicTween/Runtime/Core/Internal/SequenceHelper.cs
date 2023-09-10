using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween.Core
{
    using static TweenWorld;

    [BurstCompile]
    internal unsafe static class SequenceHelper
    {
        public static void Append<T>(Sequence sequence, T tween, bool join) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(sequence);
            AssertTween.IsActive(tween);
            AssertTween.IsRoot(tween);
            AssertTween.SequenceItemIsNotEqualsItSelf(sequence, tween);
            AssertTween.SequenceItemIsNotStarted(tween);
            AssertTween.SequenceItemIsCompletable(tween);

            var entity = sequence.GetEntity();
            var sequenceState = EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceDuration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var sequenceBuffer = EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(tween.GetEntity(), out var resolvedChildDuration);
            var childLoops = EntityManager.GetComponentData<TweenParameterLoops>(tween.GetEntity()).value;
            var childDelay = EntityManager.GetComponentData<TweenParameterDelay>(tween.GetEntity()).value;
            resolvedChildDuration = childLoops * resolvedChildDuration + childDelay;

            float offset;
            if (join)
            {
                var p = sequenceDuration - sequenceState.lastTweenDuration;
                offset = p;
                sequenceDuration = math.max(sequenceDuration, p + resolvedChildDuration);
            }
            else
            {
                offset = sequenceDuration;
                sequenceDuration += resolvedChildDuration;
            }
            sequenceState.lastTweenDuration = math.max(sequenceState.lastTweenDuration, resolvedChildDuration);

            sequenceBuffer.Add(new SequenceEntitiesGroup(tween.GetEntity(), offset));
            sequenceBuffer.AsNativeArray().Sort();

            LockChild(tween.GetEntity());
            SetSequenceComponents(entity, sequenceState, sequenceDuration);
        }

        public static void Insert<T>(Sequence sequence, T tween, float position) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(sequence);
            AssertTween.IsActive(tween);
            AssertTween.IsRoot(tween);
            AssertTween.SequenceItemIsNotEqualsItSelf(sequence, tween);
            AssertTween.SequenceItemIsNotStarted(tween);
            AssertTween.SequenceItemIsCompletable(tween);

            var entity = sequence.GetEntity();
            var sequenceState = EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceDuration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var sequenceBuffer = EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(tween.GetEntity(), out var resolvedChildDuration);
            var childLoops = EntityManager.GetComponentData<TweenParameterLoops>(tween.GetEntity()).value;
            var childDelay = EntityManager.GetComponentData<TweenParameterDelay>(tween.GetEntity()).value;
            resolvedChildDuration = childLoops * resolvedChildDuration + childDelay;
            sequenceDuration = math.max(sequenceDuration, position + resolvedChildDuration);

            sequenceBuffer.Add(new SequenceEntitiesGroup(tween.GetEntity(), position));
            sequenceBuffer.AsNativeArray().Sort();

            LockChild(tween.GetEntity());
            SetSequenceComponents(entity, sequenceState, sequenceDuration);
        }

        public static void Prepend<T>(Sequence sequence, T tween) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(sequence);
            AssertTween.IsActive(tween);
            AssertTween.IsRoot(tween);
            AssertTween.SequenceItemIsNotEqualsItSelf(sequence, tween);
            AssertTween.SequenceItemIsNotStarted(tween);
            AssertTween.SequenceItemIsCompletable(tween);

            var entity = sequence.GetEntity();
            var sequenceState = EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceDuration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var sequenceBuffer = EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(tween.GetEntity(), out var resolvedChildDuration);
            var childLoops = EntityManager.GetComponentData<TweenParameterLoops>(tween.GetEntity()).value;
            var childDelay = EntityManager.GetComponentData<TweenParameterDelay>(tween.GetEntity()).value;
            resolvedChildDuration = childLoops * resolvedChildDuration + childDelay;

            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                ((SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i)->position += resolvedChildDuration;
            }

            sequenceDuration += resolvedChildDuration;

            sequenceBuffer.Add(new SequenceEntitiesGroup(tween.GetEntity(), 0f));
            sequenceBuffer.AsNativeArray().Sort();

            LockChild(tween.GetEntity());
            SetSequenceComponents(entity, sequenceState, sequenceDuration);
        }

        public static void AppendInterval(Sequence sequence, float interval)
        {
            AssertTween.IsActive(sequence);
            AssertTween.SequenceIntervalIsHigherThanZero(interval);

            var entity = sequence.GetEntity();
            var sequenceState = EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceDuration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            sequenceDuration += interval;
            sequenceState.lastTweenDuration = interval;

            SetSequenceComponents(entity, sequenceState, sequenceDuration);
        }

        public static void PrependInterval(Sequence sequence, float interval)
        {
            AssertTween.IsActive(sequence);
            AssertTween.SequenceIntervalIsHigherThanZero(interval);

            var entity = sequence.GetEntity();
            var sequenceState = EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceBuffer = EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            var sequenceDuration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;

            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                ((SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i)->position += interval;
            }
            sequenceDuration += interval;

            SetSequenceComponents(entity, sequenceState, sequenceDuration);
        }

        static void AdjustChildParameters(in Entity entity, out float resolvedDuration)
        {
            var speed = EntityManager.GetComponentData<TweenParameterPlaybackSpeed>(entity).value;
            AdjustChildParametersCore(entity, speed, out resolvedDuration);
            EntityManager.SetComponentData(entity, new TweenParameterPlaybackSpeed(1f));
        }

        static void AdjustChildParametersCore(in Entity entity, in float speed, out float resolvedDuration)
        {
            var duration = EntityManager.GetComponentData<TweenParameterDuration>(entity).value;
            var delay = EntityManager.GetComponentData<TweenParameterDelay>(entity).value;

            duration /= speed;
            delay /= speed;

            if (EntityManager.HasBuffer<SequenceEntitiesGroup>(entity))
            {
                var sequenceBuffer = EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
                for (int i = 0; i < sequenceBuffer.Length; i++)
                {
                    var ptr = (SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i;
                    AdjustChildParametersCore(ptr->entity, speed, out var d);
                    ptr->position /= speed;
                }
            }

            EntityManager.SetComponentData(entity, new TweenParameterDuration(duration));
            EntityManager.SetComponentData(entity, new TweenParameterDelay(delay));

            resolvedDuration = duration;
        }

        static void LockChild(in Entity entity)
        {
            EntityManager.SetComponentData(entity, new TweenParameterAutoPlay(true));
            EntityManager.SetComponentData(entity, new TweenParameterAutoKill(false));
            EntityManager.SetComponentEnabled<TweenRootFlag>(entity, false);
        }

        static void SetSequenceComponents(in Entity entity, in SequenceState sequenceState, in float duration)
        {
            EntityManager.SetComponentData(entity, new TweenParameterDuration(duration));
            EntityManager.SetComponentData(entity, sequenceState);
        }
    }
}