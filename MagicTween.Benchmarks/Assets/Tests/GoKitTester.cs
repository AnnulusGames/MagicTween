using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GoKitTester
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp(TestClass[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Go.killAllTweensWithTarget(array[i]);
        }
        GameObject.Destroy(Go.instance);
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp(TestClass instance)
    {
        Go.killAllTweensWithTarget(instance);
        GameObject.Destroy(Go.instance);
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp(Transform[] tranforms)
    {
        for (int i = 0; i < tranforms.Length; i++)
        {
            Go.killAllTweensWithTarget(tranforms[i]);
        }
        GameObject.Destroy(Go.instance);
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateFloatTween(TestClass instance, float duration)
    {
        GoTweenConfig goConfig = new GoTweenConfig();
        goConfig.clearProperties();
        goConfig.floatProp(TestClass.PropertyName, 10f);
        Go.to(instance, duration, goConfig);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateFloatTweens(TestClass[] array, float duration)
    {
        GoTweenConfig goConfig = new GoTweenConfig();

        for (int i = 0; i < array.Length; i++)
        {
            goConfig.clearProperties();
            goConfig.floatProp(TestClass.PropertyName, i + 10f);
            Go.to(array[i], duration, goConfig);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreatePositionTweens(Transform[] transforms, float duration)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].positionTo(duration, Vector3.one * i);
        }
    }
}