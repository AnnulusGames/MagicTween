using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public static class TweenPropertyAccessorPool<T>
    {
        readonly static Stack<TweenPropertyAccessor<T>> stack;
        const int InitialSize = 256;

        static TweenPropertyAccessorPool()
        {
            stack = new(InitialSize);
            Prewarm(InitialSize);
        }

        public static void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stack.Push(new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessor<T> Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessor<T> Rent(TweenGetter<T> getter, TweenSetter<T> setter)
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            result.getter = getter;
            result.setter = setter;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenPropertyAccessor<T> instance)
        {
            if (instance.getter != null)
            {
                instance.getter = null;
                instance.setter = null;
                stack.Push(instance);
            }
        }
    }

    public static class TweenPropertyAccessorNoAllocPool<T>
    {
        readonly static Stack<TweenPropertyAccessorNoAlloc<T>> stack;
        const int InitialSize = 256;

        static TweenPropertyAccessorNoAllocPool()
        {
            stack = new(InitialSize);
            Prewarm(InitialSize);
        }

        public static void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stack.Push(new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessorNoAlloc<T> Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessorNoAlloc<T> Rent(object target, TweenGetter<object, T> getter, TweenSetter<object, T> setter)
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            result.target = target;
            result.getter = getter;
            result.setter = setter;

            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenPropertyAccessorNoAlloc<T> instance)
        {
            if (instance.target != null)
            {
                instance.target = null;
                instance.getter = null;
                instance.setter = null;
                stack.Push(instance);
            }
        }
    }

    public static class TweenCallbackActionsPool
    {
        readonly static Stack<TweenCallbackActions> stack;
        const int InitialSize = 256;

        static TweenCallbackActionsPool()
        {
            stack = new(InitialSize);
            Prewarm(InitialSize);
        }

        public static void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stack.Push(new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenCallbackActions Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenCallbackActions instance)
        {
            if (instance.HasAction())
            {
                instance.onStart = null;
                instance.onPlay = null;
                instance.onPause = null;
                instance.onUpdate = null;
                instance.onStepComplete = null;
                instance.onComplete = null;
                instance.onKill = null;
                instance.onRewind = null;
                stack.Push(instance);
            }
        }
    }

    public static class TweenCallbackActionsNoAllocPool
    {
        readonly static Stack<TweenCallbackActionsNoAlloc> stack;
        const int InitialSize = 256;

        static TweenCallbackActionsNoAllocPool()
        {
            stack = new(InitialSize);
            Prewarm(InitialSize);
        }

        public static void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stack.Push(new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenCallbackActionsNoAlloc Rent(object target)
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            result.target = target;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenCallbackActionsNoAlloc instance)
        {
            if (instance.target != null)
            {
                instance.target = null;
                instance.onStart = null;
                instance.onPlay = null;
                instance.onPause = null;
                instance.onUpdate = null;
                instance.onStepComplete = null;
                instance.onComplete = null;
                instance.onKill = null;
                instance.onRewind = null;
                stack.Push(instance);
            }
        }
    }
}
