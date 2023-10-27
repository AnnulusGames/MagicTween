using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using PrimeTween;

namespace MagicTween.Benchmark
{
    public static class PrimeTweenHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            PrimeTween.Tween.StopAll();

            var obj = GameObject.Find("PrimeTweenManager");
            if (obj != null) GameObject.Destroy(obj);
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(int count)
        {
            PrimeTweenConfig.SetTweensCapacity(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            PrimeTween.Tween.Custom(instance, 0f, 10f, duration, (obj, x) => obj.value = x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                PrimeTween.Tween.Custom(array[i], i, i + 10f, duration, (obj, x) => obj.value = x);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                PrimeTween.Tween.Position(transforms[i], Vector3.one * i, duration);
            }
        }
    }
}