using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core.Components
{
    public sealed class TweenPropertyAccessor<T> : IComponentData
    {
        public TweenGetter<T> getter;
        public TweenSetter<T> setter;

        public TweenPropertyAccessor() { }

        // Implement pooling using Dispose of Managed Component.
        // This is not the expected use of Dispose, but it works.
        public void Dispose()
        {
            TweenPropertyAccessorPool<T>.Return(this);
        }
    }

    // Since it is difficult to use generics due to ECS restrictions,
    // use UnsafeUtility.As to force assignment of delegates when creating components.
    // So the type of the target and the type of the TweenGetter/TweenSetter argument must match absolutely,
    // otherwise undefined behavior will result.
    public sealed class TweenPropertyAccessorNoAlloc<T> : IComponentData, IDisposable
    {
        [HideInInspector] public object target;
        [HideInInspector] public TweenGetter<object, T> getter;
        [HideInInspector] public TweenSetter<object, T> setter;

        public TweenPropertyAccessorNoAlloc() { }

        public void Dispose()
        {
            TweenPropertyAccessorNoAllocPool<T>.Return(this);
        }
    }

    public sealed class TweenCallbackActions : IComponentData, IDisposable
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

        public void Dispose()
        {
            TweenCallbackActionsPool.Return(this);
        }

        internal bool HasAction()
        {
            if (onStart != null) return true;
            if (onPlay != null) return true;
            if (onPause != null) return true;
            if (onUpdate != null) return true;
            if (onStepComplete != null) return true;
            if (onComplete != null) return true;
            if (onKill != null) return true;
            if (onRewind != null) return true;
            return false;
        }
    }
}