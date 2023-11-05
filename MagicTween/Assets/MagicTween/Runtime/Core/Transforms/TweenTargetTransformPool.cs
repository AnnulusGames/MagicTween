#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MagicTween.Core.Transforms
{
    public static class TweenTargetTransformPool
    {
        readonly static Stack<TweenTargetTransform> stack;
        const int InitialSize = 256;

        static TweenTargetTransformPool()
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
        public static TweenTargetTransform Rent()
        {
            if (!stack.TryPop(out var result))
            {
                result = new();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(TweenTargetTransform instance)
        {
            if (instance.target != null)
            {
                instance.target = null;
                stack.Push(instance);
            }
        }
    }
}
#endif