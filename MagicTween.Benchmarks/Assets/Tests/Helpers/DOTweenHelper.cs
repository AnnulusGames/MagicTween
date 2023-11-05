using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace MagicTween.Benchmark
{
    public static class DOTweenHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            DOTween.Clear(true);
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(int tweenerCount, int sequenceCount)
        {
            DOTween.SetTweensCapacity(tweenerCount, sequenceCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            DOVirtual.Float(0f, 10f, duration, x => instance.value = x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var index = i;
                DOVirtual.Float(i, i + 10f, duration, x => array[index].value = x);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].DOMove(Vector3.one * i, duration);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].DORotateQuaternion(Quaternion.Euler(90f, 90f, 90f), duration);
            }
        }
    }
}