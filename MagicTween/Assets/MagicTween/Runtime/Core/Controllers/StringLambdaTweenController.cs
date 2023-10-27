using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public sealed class StringLambdaTweenController : ITweenController<UnsafeText>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<UnsafeText, StringTweenPlugin, StringLambdaTweenController>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<UnsafeText, StringTweenPlugin, StringLambdaTweenController>(this, entity);

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

            TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessor<string>>(entity).setter(currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }

    public sealed class NoAllocStringLambdaTweenController : ITweenController<UnsafeText>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<UnsafeText, StringTweenPlugin, NoAllocStringLambdaTweenController>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<UnsafeText, StringTweenPlugin, NoAllocStringLambdaTweenController>(this, entity);

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

            var accessor = TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessorNoAlloc<string>>(entity);
            accessor.setter(accessor.target, currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }
}