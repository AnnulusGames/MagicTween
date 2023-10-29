using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core.Controllers
{
    public sealed class DelegateTweenController<TValue, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
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
}