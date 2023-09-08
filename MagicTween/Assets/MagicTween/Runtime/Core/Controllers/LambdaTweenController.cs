using Unity.Entities;

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

    public sealed class UnsafeLambdaTweenController<TValue, TPlugin> : TweenControllerBase<TValue, TPlugin>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        protected override void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var accessor = TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessorUnsafe<TValue>>(entity);
            accessor.setter(accessor.target, currentValue);
        }
    }
}