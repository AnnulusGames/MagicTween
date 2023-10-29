using MagicTween.Core.Components;
using Unity.Entities;

namespace MagicTween.Core.Controllers
{
    public sealed class EntityTweenController<TValue, TPlugin, TComponent, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
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
}