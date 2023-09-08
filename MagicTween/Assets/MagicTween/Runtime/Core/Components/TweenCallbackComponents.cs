using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core
{
    public sealed class TweenPropertyAccessor<T> : IComponentData
    {
        public readonly TweenGetter<T> getter;
        public readonly TweenSetter<T> setter;

        public TweenPropertyAccessor() { }
        public TweenPropertyAccessor(TweenGetter<T> getter, TweenSetter<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }

    // Since it is difficult to use generics due to ECS restrictions,
    // use UnsafeUtility.As to force assignment of delegates when creating components.
    // So the type of the target and the type of the TweenGetter/TweenSetter argument must match absolutely,
    // otherwise undefined behavior will result.
    public sealed class TweenPropertyAccessorUnsafe<T> : IComponentData
    {
        [HideInInspector] public readonly object target;
        [HideInInspector] public readonly TweenGetter<object, T> getter;
        [HideInInspector] public readonly TweenSetter<object, T> setter;

        public TweenPropertyAccessorUnsafe() { }
        public TweenPropertyAccessorUnsafe(object target, TweenGetter<object, T> getter, TweenSetter<object, T> setter)
        {
            this.target = target;
            this.getter = getter;
            this.setter = setter;
        }
    }
    
    public sealed class TweenCallbackActions : IComponentData
    {
        public Action onStart;
        public Action onPlay;
        public Action onPause;
        public Action onUpdate;
        public Action onStepComplete;
        public Action onComplete;
        public Action onKill;

        // internal callback
        public Action onRewind;
    }
}