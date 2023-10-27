using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicTween.Benchmark
{
    public static class LeanTweenHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            LeanTween.cancelAll();
            var obj = GameObject.Find("~LeanTween");
            if (obj != null) GameObject.Destroy(obj);
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(int count)
        {
            LeanTween.init(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            LeanTween.value(0f, 10f, duration).setOnUpdate(x => instance.value = x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var index = i;
                LeanTween.value(i, i + 10f, duration).setOnUpdate(x => array[index].value = x);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                LeanTween.move(transforms[i].gameObject, Vector3.one * i, duration);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                LeanTween.rotate(transforms[i].gameObject, new Vector3(90f, 90f, 90f), duration);
            }
        }
    }
}