using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Prime31.ZestKit;

namespace MagicTween.Benchmark
{
    public static class ZestKitHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            GameObject.Destroy(ZestKit.instance);
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            PropertyTweens.floatPropertyTo(instance, TestClass.PropertyName, 10f, duration).start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                PropertyTweens.floatPropertyTo(array[i], TestClass.PropertyName, 10f, duration).start();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].ZKpositionTo(Vector3.one * i, duration).start();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].ZKrotationTo(Quaternion.Euler(90f, 90f, 90f), duration).start();
            }
        }
    }
}