using System;
using System.Runtime.CompilerServices;
using MagicTween;
using UnityEngine;

public static class MagicTweenTester
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
            transforms[i].TweenPosition(Vector3.one * i, duration);
        }
    }
}
