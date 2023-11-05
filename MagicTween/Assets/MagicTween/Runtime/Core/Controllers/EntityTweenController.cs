using MagicTween.Core.Components;
using Unity.Entities;

namespace MagicTween.Core.Controllers
{
    public sealed class EntityTweenController<TValue, TOptions, TPlugin, TComponent, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
        where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        where TComponent : unmanaged, IComponentData
        where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TOptions, TPlugin, EntityTweenController<TValue, TOptions, TPlugin, TComponent, TTranslator>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TOptions, TPlugin, EntityTweenController<TValue, TOptions, TPlugin, TComponent, TTranslator>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TOptions, TPlugin, EntityTweenController<TValue, TOptions, TPlugin, TComponent, TTranslator>>(this, entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            ECSCache.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var targetEntity = ECSCache.EntityManager.GetComponentData<TweenTargetEntity>(entity).target;
            var component = ECSCache.EntityManager.GetComponentData<TComponent>(targetEntity);
            default(TTranslator).Apply(ref component, currentValue);
            ECSCache.EntityManager.SetComponentData(targetEntity, component);
        }
    }
}