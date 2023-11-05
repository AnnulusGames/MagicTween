using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicTween.Benchmark
{
    public static class MagicTweenHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            Tween.Clear();
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            Tween.FromTo(instance, (obj, x) => obj.value = x, 0f, 10f, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var index = i;
                Tween.FromTo(array[index], (obj, x) => obj.value = x, i, i + 10f, duration);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].TweenPosition(Vector3.zero, Vector3.one * i, duration);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].TweenRotation(Quaternion.Euler(90f, 90f, 90f), duration);
            }
        }
    }

}