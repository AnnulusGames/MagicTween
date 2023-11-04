using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core.Components
{
    public sealed class TweenDelegates<T> : IComponentData, IDisposable
    {
        public TweenGetter<T> getter;
        public TweenSetter<T> setter;

        public TweenDelegates() { }

        // Implement pooling using Dispose of Managed Component.
        // This is not the expected use of Dispose, but it works.
        public void Dispose()
        {
            TweenDelegatesPool<T>.Return(this);
        }
    }

    // Since it is difficult to use generics due to ECS restrictions,
    // use UnsafeUtility.As to force assignment of delegates when creating components.
    // So the type of the target and the type of the TweenGetter/TweenSetter argument must match absolutely,
    // otherwise undefined behavior will result.
    public sealed class TweenDelegatesNoAlloc<T> : IComponentData, IDisposable
    {
        [HideInInspector] public object target;
        [HideInInspector] public TweenGetter<object, T> getter;
        [HideInInspector] public TweenSetter<object, T> setter;

        public TweenDelegatesNoAlloc() { }

        public void Dispose()
        {
            TweenDelegatesNoAllocPool<T>.Return(this);
        }
    }

    public sealed class TweenCallbackActions : IComponentData, IDisposable
    {
        public FastAction onStart = new();

        public FastAction onPlay = new();
        public FastAction onPause = new();
        public FastAction onUpdate = new();
        public FastAction onStepComplete = new();
        public FastAction onComplete = new();
        public FastAction onKill = new();

        // internal callback
        public FastAction onRewind = new();

        public void Dispose()
        {
            TweenCallbackActionsPool.Return(this);
        }

        internal bool HasAction()
        {
            if (onStart.Count > 0) return true;
            if (onPlay.Count > 0) return true;
            if (onPause.Count > 0) return true;
            if (onUpdate.Count > 0) return true;
            if (onStepComplete.Count > 0) return true;
            if (onComplete.Count > 0) return true;
            if (onKill.Count > 0) return true;
            if (onRewind.Count > 0) return true;
            return false;
        }
    }
}