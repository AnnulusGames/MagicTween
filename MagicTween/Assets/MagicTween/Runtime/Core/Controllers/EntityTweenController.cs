using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public sealed class EntityTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Complete(in Entity entity)
            => TweenControllerHelper.Complete<TValue, TPlugin, EntityTweenController<TValue, TPlugin>>(this, entity);

        public void CompleteAndKill(in Entity entity)
            => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, EntityTweenController<TValue, TPlugin>>(this, entity);

        public void Kill(in Entity entity)
            => TweenControllerHelper.Kill(entity);

        public void Pause(in Entity entity)
            => TweenControllerHelper.Pause(entity);

        public void Play(in Entity entity)
            => TweenControllerHelper.Play(entity);

        public void Restart(in Entity entity)
            => TweenControllerHelper.Restart<TValue, TPlugin, EntityTweenController<TValue, TPlugin>>(this, entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
        }
    }
}