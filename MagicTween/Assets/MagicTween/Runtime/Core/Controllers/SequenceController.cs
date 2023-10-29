using MagicTween.Core.Components;
using Unity.Entities;

namespace MagicTween.Core.Controllers
{
    public sealed class SequenceController : ITweenController
    {
        public void Complete(in Entity entity)
        {
            var canComplete = TweenHelper.TryComplete(entity);
            if (!canComplete) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.Complete(childEntity);
            }

            TweenHelper.TryCallOnComplete(entity);
        }

        public void CompleteAndKill(in Entity entity)
        {
            var canCompleteAndKill = TweenHelper.TryCompleteAndKill(entity);
            if (!canCompleteAndKill) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.CompleteAndKill(childEntity);
            }

            TweenHelper.TryCallOnCompleteAndOnKill(entity);
        }

        public void Restart(in Entity entity)
        {
            if (!TweenWorld.EntityManager.GetComponentData<TweenStartedFlag>(entity).value)
            {
                Play(entity);
                return;
            }

            var canRestart = TweenHelper.TryRestart(entity);
            if (!canRestart) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.Restart(childEntity);
            }
        }

        public void Play(in Entity entity)
        {
            var canPlay = TweenHelper.TryPlay(entity, out var started);
            if (!canPlay) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.Play(childEntity);
            }

            TweenHelper.TryCallOnStartAndOnPlay(entity, started);
        }

        public void Pause(in Entity entity)
        {
            var canPause = TweenHelper.TryPause(entity);
            if (!canPause) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.Pause(childEntity);
            }

            TweenHelper.TryCallOnPause(entity);
        }

        public void Kill(in Entity entity)
        {
            var canKill = TweenHelper.TryKill(entity);
            if (!canKill) return;

            var sequenceBuffer = TweenWorld.EntityManager.GetBuffer<SequenceEntitiesGroup>(entity);
            for (int i = 0; i < sequenceBuffer.Length; i++)
            {
                var childEntity = sequenceBuffer[i].entity;
                var controller = TweenControllerContainer.FindControllerById(TweenWorld.EntityManager.GetComponentData<TweenControllerReference>(childEntity).controllerId);
                controller.Kill(childEntity);
            }

            TweenHelper.TryCallOnKill(entity);
        }
    }
}