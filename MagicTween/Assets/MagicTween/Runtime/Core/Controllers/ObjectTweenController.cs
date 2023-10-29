using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core.Controllers
{
    public sealed class ObjectTweenController<TValue, TPlugin, TObject, TTranslator> : ITweenController<TValue>
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
        where TObject : class
        where TTranslator : unmanaged, ITweenTranslatorManaged<TValue, TObject>
    {
        static readonly TTranslator translator = new();

        public void Complete(in Entity entity) => TweenControllerHelper.Complete<TValue, TPlugin, ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill<TValue, TPlugin, ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart<TValue, TPlugin, ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>(this, entity);

        public void SetValue(TValue value, in Entity entity)
        {
            TweenWorld.EntityManager.SetComponentData(entity, new TweenValue<TValue>() { value = value });
            var target = (TObject)TweenWorld.EntityManager.GetComponentObject<TweenTargetObject>(entity).target;
            translator.Apply(target, value);
        }
    }
}