using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Prime31.ZestKit;

public static class ZestKitTester
{
    const string PropertyName = "valueProperty";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp()
    {
        GameObject.Destroy(ZestKit.instance);
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateFloatTween(TestClass instance, float duration)
    {
        PropertyTweens.floatPropertyTo(instance, PropertyName, 10f, duration).start();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateFloatTweens(TestClass[] array, float duration)
    {
        for (int i = 0; i < array.Length; i++)
        {
            PropertyTweens.floatPropertyTo(array[i], PropertyName, 10f, duration).start();
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
}