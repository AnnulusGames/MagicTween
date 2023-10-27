using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public sealed class LambdaTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<TValue, TPlugin, LambdaTweenController<TValue, TPlugin>>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, LambdaTweenController<TValue, TPlugin>>(this, entity);

        public void Kill(in Entity entity)
            => TweenControllerHelper.Kill(entity);

        public void Pause(in Entity entity)
            => TweenControllerHelper.Pause(entity);

        public void Play(in Entity entity)
            => TweenControllerHelper.Play(entity);

        public void Restart(in Entity entity)
            => TweenControllerHelper.Restart<TValue, TPlugin, LambdaTweenController<TValue, TPlugin>>(this, entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessor<TValue>>(entity).setter(currentValue);
        }
    }

    public sealed class NoAllocLambdaTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<TValue, TPlugin, NoAllocLambdaTweenController<TValue, TPlugin>>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, NoAllocLambdaTweenController<TValue, TPlugin>>(this, entity);

        public void Kill(in Entity entity)
            => TweenControllerHelper.Kill(entity);

        public void Pause(in Entity entity)
            => TweenControllerHelper.Pause(entity);

        public void Play(in Entity entity)
            => TweenControllerHelper.Play(entity);

        public void Restart(in Entity entity)
            => TweenControllerHelper.Restart<TValue, TPlugin, NoAllocLambdaTweenController<TValue, TPlugin>>(this, entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var accessor = TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessorNoAlloc<TValue>>(entity);
            accessor.setter(accessor.target, currentValue);
        }
    }
}