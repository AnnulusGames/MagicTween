using Unity.Entities;

namespace MagicTween.Core
{
    public sealed class EntityTweenController<TValue, TPlugin> : TweenControllerBase<TValue, TPlugin>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        protected override void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
        }
    }
}