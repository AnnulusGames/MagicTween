using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using PrimeTween;

public static class PrimeTweenTester
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp()
    {
        Tween.StopAll();

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
    public static void CreateFloatTweens(TestClass[] array, float duration)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Tween.Custom(array[i], i, i + 10f, duration, (obj, x) => obj.value = x);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreatePositionTweens(Transform[] transforms, float duration)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            Tween.Position(transforms[i], Vector3.one * i, duration);
        }
    }
}