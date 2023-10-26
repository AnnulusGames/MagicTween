using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public sealed class LambdaTweenController<TValue, TPlugin> : TweenControllerBase<TValue, TPlugin>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        protected override void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessor<TValue>>(entity).setter(currentValue);
        }
    }

    public sealed class NoAllocLambdaTweenController<TValue, TPlugin> : TweenControllerBase<TValue, TPlugin>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        protected override void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var accessor = TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessorNoAlloc<TValue>>(entity);
            accessor.setter(accessor.target, currentValue);
        }
    }
}