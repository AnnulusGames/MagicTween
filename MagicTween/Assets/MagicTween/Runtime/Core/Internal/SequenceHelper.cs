using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using MagicTween.Diagnostics;

namespace MagicTween.Core
{
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

            var childEntity = tween.GetEntity();
            var childClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(childEntity);
            var childPlaySettings = TweenWorld.EntityManager.GetComponentData<TweenPlaySettings>(childEntity);
            var childSpeed = TweenWorld.EntityManager.GetComponentData<TweenPlaybackSpeed>(childEntity);
            var childDuration = TweenHelper.GetDuration(childClip, childSpeed);

            AssertTween.SequenceItemIsCompletable(childDuration);

            var entity = sequence.GetEntity();
            var sequenceState = TweenWorld.EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            childDuration = TweenHelper.GetDuration(childClip, childSpeed);

            float offset;
            if (join)
            {
                var p = sequenceClip.duration - sequenceState.lastTweenDuration;
                offset = p;
                sequenceClip.duration = math.max(sequenceClip.duration, p + childDuration);
            }
            else
            {
                offset = sequenceClip.duration;
                sequenceClip.duration += childDuration;
            }
            sequenceState.lastTweenDuration = math.max(sequenceState.lastTweenDuration, childDuration);

            sequenceBuffer.Add(new SequenceEntitiesGroup(childEntity, offset));
            sequenceBuffer.AsNativeArray().Sort();

            SetChildComponentsAndLock(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            SetSequenceComponents(entity, ref sequenceState, ref sequenceClip);
        }

        public static void Insert<T>(Sequence sequence, T tween, float position) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(sequence);
            AssertTween.IsActive(tween);
            AssertTween.IsRoot(tween);
            AssertTween.SequenceItemIsNotEqualsItSelf(sequence, tween);
            AssertTween.SequenceItemIsNotStarted(tween);

            var childEntity = tween.GetEntity();
            var childClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(childEntity);
            var childPlaySettings = TweenWorld.EntityManager.GetComponentData<TweenPlaySettings>(childEntity);
            var childSpeed = TweenWorld.EntityManager.GetComponentData<TweenPlaybackSpeed>(childEntity);
            var childDuration = TweenHelper.GetDuration(childClip, childSpeed);

            AssertTween.SequenceItemIsCompletable(childDuration);

            var entity = sequence.GetEntity();
            var sequenceState = TweenWorld.EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            sequenceClip.duration = math.max(sequenceClip.duration, position + childDuration);

            sequenceBuffer.Add(new SequenceEntitiesGroup(childEntity, position));
            sequenceBuffer.AsNativeArray().Sort();

            SetChildComponentsAndLock(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            SetSequenceComponents(entity, ref sequenceState, ref sequenceClip);
        }

        public static void Prepend<T>(Sequence sequence, T tween) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(sequence);
            AssertTween.IsActive(tween);
            AssertTween.IsRoot(tween);
            AssertTween.SequenceItemIsNotEqualsItSelf(sequence, tween);
            AssertTween.SequenceItemIsNotStarted(tween);

            var childEntity = tween.GetEntity();
            var childClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(childEntity);
            var childPlaySettings = TweenWorld.EntityManager.GetComponentData<TweenPlaySettings>(childEntity);
            var childSpeed = TweenWorld.EntityManager.GetComponentData<TweenPlaybackSpeed>(childEntity);
            var childDuration = TweenHelper.GetDuration(childClip, childSpeed);

            AssertTween.SequenceItemIsCompletable(childDuration);

            var entity = sequence.GetEntity();
            var sequenceState = TweenWorld.EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);

            AdjustChildParameters(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            childDuration = TweenHelper.GetDuration(childClip, childSpeed);

            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                ((SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i)->position += childDuration;
            }

            sequenceClip.duration += childDuration;

            sequenceBuffer.Add(new SequenceEntitiesGroup(childEntity, 0f));
            sequenceBuffer.AsNativeArray().Sort();

            SetChildComponentsAndLock(childEntity, ref childPlaySettings, ref childSpeed, ref childClip);
            SetSequenceComponents(entity, ref sequenceState, ref sequenceClip);
        }

        public static void AppendInterval(Sequence sequence, float interval)
        {
            AssertTween.IsActive(sequence);
            AssertTween.SequenceIntervalIsHigherThanZero(interval);

            var entity = sequence.GetEntity();
            var sequenceState = TweenWorld.EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);

            sequenceClip.duration += interval;
            sequenceState.lastTweenDuration = interval;
            sequenceClip.duration = math.max(sequenceClip.duration, sequenceClip.duration);

            SetSequenceComponents(entity, ref sequenceState, ref sequenceClip);
        }

        public static void PrependInterval(Sequence sequence, float interval)
        {
            AssertTween.IsActive(sequence);
            AssertTween.SequenceIntervalIsHigherThanZero(interval);

            var entity = sequence.GetEntity();
            var sequenceState = TweenWorld.EntityManager.GetComponentData<SequenceState>(entity);
            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            var sequenceClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);

            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                ((SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i)->position += interval;
            }

            sequenceClip.duration += interval;

            SetSequenceComponents(entity, ref sequenceState, ref sequenceClip);
        }

        static void AdjustChildParameters(in Entity entity, ref TweenPlaySettings playSettings, ref TweenPlaybackSpeed speed, ref TweenClip clip)
        {
            playSettings.autoPlay = true;
            playSettings.autoKill = false;
            AdjustChildParametersCore(entity, speed.speed, ref clip);
            speed.speed = 1;
        }

        static void AdjustChildParametersCore(in Entity entity, float speed, ref TweenClip clip)
        {
            clip.duration /= speed;
            clip.delay /= speed;

            if (TweenWorld.EntityManager.HasBuffer<SequenceEntitiesGroup>(entity))
            {
                var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
                for (int i = 0; i < sequenceBuffer.Length; i++)
                {
                    var ptr = (SequenceEntitiesGroup*)sequenceBuffer.GetUnsafePtr() + i;

                    var childClip = TweenWorld.EntityManager.GetComponentData<TweenClip>(ptr->entity);
                    var childSpeed = TweenWorld.EntityManager.GetComponentData<TweenPlaybackSpeed>(ptr->entity);
                    AdjustChildParametersCore(ptr->entity, speed, ref childClip);
                    TweenWorld.EntityManager.SetComponentData(ptr->entity, childClip);
                    TweenWorld.EntityManager.SetComponentData(ptr->entity, childSpeed);

                    ptr->position /= speed;
                }
            }
        }

        static void SetChildComponentsAndLock(in Entity entity, ref TweenPlaySettings playSettings, ref TweenPlaybackSpeed speed, ref TweenClip clip)
        {
            TweenWorld.EntityManager.SetComponentData(entity, playSettings);
            TweenWorld.EntityManager.SetComponentData(entity, speed);
            TweenWorld.EntityManager.SetComponentData(entity, clip);
            TweenWorld.EntityManager.SetComponentEnabled<TweenRootFlag>(entity, false);
        }

        static void SetSequenceComponents(in Entity entity, ref SequenceState sequenceState, ref TweenClip clip)
        {
            TweenWorld.EntityManager.SetComponentData(entity, clip);
            TweenWorld.EntityManager.SetComponentData(entity, sequenceState);
        }
    }
}