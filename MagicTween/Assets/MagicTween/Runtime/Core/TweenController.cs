using MagicTween.Core;
using MagicTween.Core.Components;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

namespace MagicTween
{
    public sealed class EntityTweenController<TValue, TPlugin, TComponent, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
        where TComponent : unmanaged, IComponentData
        where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TPlugin, EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>(this, entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var targetEntity = TweenWorld.EntityManager.GetComponentData<TweenTargetEntity>(entity).target;
            var component = TweenWorld.EntityManager.GetComponentData<TComponent>(targetEntity);
            default(TTranslator).Apply(ref component, currentValue);
            TweenWorld.EntityManager.SetComponentData(targetEntity, component);
        }
    }

    public sealed class ManagedTweenController<TValue, TPlugin, TObject, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
        where TObject : class
        where TTranslator : class, ITweenTranslatorManaged<TValue, TObject>, new()
    {
        static readonly TTranslator translator = new();

        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, ManagedTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, ManagedTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity)  => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TPlugin, ManagedTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);

        public void SetValue(TValue value, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = value });
            var target = (TObject)TweenWorld.EntityManager.GetComponentObject<TweenTargetObject>(entity).target;
            translator.Apply(target, value);
        }
    }

    public sealed class DelegateTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, DelegateTweenController<TValue, TPlugin>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, DelegateTweenController<TValue, TPlugin>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TPlugin, DelegateTweenController<TValue, TPlugin>>(this, entity);

        public void SetValue(TValue value, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = value });
            var delegates = TweenWorld.EntityManager.GetComponentData<TweenDelegates<TValue>>(entity);
            delegates.setter?.Invoke(value);
        }
    }

    public sealed class NoAllocDelegateTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, NoAllocDelegateTweenController<TValue, TPlugin>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, NoAllocDelegateTweenController<TValue, TPlugin>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TPlugin, NoAllocDelegateTweenController<TValue, TPlugin>>(this, entity);

        public void SetValue(TValue value, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = value });
            var delegates = TweenWorld.EntityManager.GetComponentData<TweenDelegatesNoAlloc<TValue>>(entity);
            delegates.setter?.Invoke(delegates.target, value);
        }
    }

    public sealed class StringDelegateTweenController : ITweenController<UnsafeText>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<UnsafeText, StringTweenPlugin, StringDelegateTweenController>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<UnsafeText, StringTweenPlugin, StringDelegateTweenController>(this, entity);

        public void Kill(in Entity entity)
            => TweenControllerHelper.Kill(entity);

        public void Pause(in Entity entity)
            => TweenControllerHelper.Pause(entity);

        public void Play(in Entity entity)
            => TweenControllerHelper.Play(entity);

        public void Restart(in Entity entity)
            => TweenControllerHelper.Restart(entity);

        public void SetValue(UnsafeText currentValue, in Entity entity)
        {
            var text = TweenWorld.EntityManager.GetComponentData<TweenValue<UnsafeText>>(entity);
            text.value.CopyFrom(currentValue);
            TweenWorld.EntityManager.SetComponentData(entity, text);

            TweenWorld.EntityManager.GetComponentData<TweenDelegates<string>>(entity).setter?.Invoke(currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }

    public sealed class SequenceTweenController : ITweenController
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

    public sealed class UnitTweenController : ITweenController
    {
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Complete(in Entity entity) => TweenControllerHelper.Complete(entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart(entity);
    }
}