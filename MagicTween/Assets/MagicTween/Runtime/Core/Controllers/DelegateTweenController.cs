using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core.Controllers
{
    public sealed class DelegateTweenController<TValue, TOptions, TPlugin> : ITweenController<TValue>
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
        where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TOptions, TPlugin, DelegateTweenController<TValue, TOptions, TPlugin>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TOptions, TPlugin, DelegateTweenController<TValue, TOptions, TPlugin>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TOptions, TPlugin, DelegateTweenController<TValue, TOptions, TPlugin>>(this, entity);

        public void SetValue(TValue value, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = value });
            var delegates = TweenWorld.EntityManager.GetComponentData<TweenDelegates<TValue>>(entity);
            delegates.setter?.Invoke(value);
        }
    }
}