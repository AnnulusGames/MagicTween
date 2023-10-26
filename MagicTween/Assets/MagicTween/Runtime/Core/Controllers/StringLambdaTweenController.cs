using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public sealed class StringLambdaTweenController : TweenControllerBase<UnsafeText, StringTweenPlugin>
    {
        protected override void SetValue(UnsafeText currentValue, in Entity entity)
        {
            var text = TweenWorld.EntityManager.GetComponentData<TweenValue<UnsafeText>>(entity);
            text.value.CopyFrom(currentValue);
            TweenWorld.EntityManager.SetComponentData(entity, text);

            TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessor<string>>(entity).setter(currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }

    public sealed class UnsafeStringLambdaTweenController : TweenControllerBase<UnsafeText, StringTweenPlugin>
    {
        protected override void SetValue(UnsafeText currentValue, in Entity entity)
        {
            var text = TweenWorld.EntityManager.GetComponentData<TweenValue<UnsafeText>>(entity);
            text.value.CopyFrom(currentValue);
            TweenWorld.EntityManager.SetComponentData(entity, text);

            var accessor = TweenWorld.EntityManager.GetComponentData<TweenPropertyAccessorNoAlloc<string>>(entity);
            accessor.setter(accessor.target, currentValue.ConvertToString());
            currentValue.Dispose();
        }
    }
}