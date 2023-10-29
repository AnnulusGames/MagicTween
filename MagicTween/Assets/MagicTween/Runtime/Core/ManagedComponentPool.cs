using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public static class TweenDelegatesPool<T>
    {
        readonly static Stack<TweenDelegates<T>> stack;
        const int InitialSize = 256;

        static TweenDelegatesPool()
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
        public static TweenDelegates<T> Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenDelegates<T> Rent(TweenGetter<T> getter, TweenSetter<T> setter)
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
        public static void Return(TweenDelegates<T> instance)
        {
            if (instance.getter != null)
            {
                instance.getter = null;
                instance.setter = null;
                stack.Push(instance);
            }
        }
    }

    public static class TweenDelegatesNoAllocPool<T>
    {
        readonly static Stack<TweenDelegatesNoAlloc<T>> stack;
        const int InitialSize = 256;

        static TweenDelegatesNoAllocPool()
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
        public static TweenDelegatesNoAlloc<T> Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenDelegatesNoAlloc<T> Rent(object target, TweenGetter<object, T> getter, TweenSetter<object, T> setter)
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
        public static void Return(TweenDelegatesNoAlloc<T> instance)
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
        public static TweenCallbackActionsNoAlloc Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenCallbackActionsNoAlloc instance)
        {
            if (instance.HasAction())
            {
                instance.onStart.Clear();
                instance.onPlay.Clear();
                instance.onPause.Clear();
                instance.onUpdate.Clear();
                instance.onStepComplete.Clear();
                instance.onComplete.Clear();
                instance.onKill.Clear();
                instance.onRewind.Clear();
                stack.Push(instance);
            }
        }
    }

    public static class TweenTargetObjectPool
    {
        readonly static Stack<TweenTargetObject> stack;
        const int InitialSize = 256;

        static TweenTargetObjectPool()
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
        public static TweenTargetObject Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenTargetObject instance)
        {
            if (instance.target != null)
            {
                instance.target = null;
                stack.Push(instance);
            }
        }
    }

}
