using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public static class DOTweenTester
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
}

