using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    internal static class TweenPropertyAccessorPool<T>
    {
        readonly static Stack<TweenPropertyAccessor<T>> stack;
        const int InitialSize = 32;

        static TweenPropertyAccessorPool()
        {
            stack = new(InitialSize);
            for (int i = 0; i < InitialSize; i++)
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

    internal static class TweenPropertyAccessorUnsafePool<T>
    {
        readonly static Stack<TweenPropertyAccessorUnsafe<T>> stack;
        const int InitialSize = 32;

        static TweenPropertyAccessorUnsafePool()
        {
            stack = new(InitialSize);
            for (int i = 0; i < InitialSize; i++)
            {
                stack.Push(new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessorUnsafe<T> Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenPropertyAccessorUnsafe<T> Rent(object target, TweenGetter<object, T> getter, TweenSetter<object, T> setter)
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
        public static void Return(TweenPropertyAccessorUnsafe<T> instance)
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
}
