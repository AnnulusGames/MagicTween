using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using MagicTween.Core.Components;
using MagicTween.Plugins;

namespace MagicTween.Core.Controllers
{
    public sealed class StringDelegateTweenController : ITweenController<UnsafeText>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<UnsafeText, StringTweenPlugin, StringDelegateTweenController>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<UnsafeText, StringTweenPlugin, StringDelegateTweenController>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart(entity);

        public void SetValue(UnsafeText currentValue, in Entity entity)
        {
            var text = TweenWorld.EntityManager.GetComponentData<TweenValue<UnsafeText>>(entity);
            text.value.CopyFrom(currentValue);
            TweenWorld.EntityManager.SetComponentData(entity, text);

            TweenWorld.EntityManager.GetComponentData<TweenDelegates<string>>(entity).setter?.Invoke(currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }
}