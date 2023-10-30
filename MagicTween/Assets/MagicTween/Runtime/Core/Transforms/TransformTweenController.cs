using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core.Transforms
{
    internal sealed class TransformTweenController<TValue, TPlugin, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
        where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
    {
        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, TransformTweenController<TValue, TPlugin, TTranslator>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, TransformTweenController<TValue, TPlugin, TTranslator>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart(entity);

        public void SetValue(TValue currentValue, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = currentValue });
            var target = TweenWorld.EntityManager.GetComponentData<TweenTargetTransform>(entity);
            if (target.isRegistered) TransformManager.Unregister(target);
            default(TTranslator).ApplyManaged(target.target, currentValue);
        }
    }
}