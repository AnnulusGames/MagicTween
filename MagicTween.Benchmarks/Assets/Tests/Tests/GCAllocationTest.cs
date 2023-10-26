using System;
using NUnit.Framework;
using Unity.PerformanceTesting;

public class GCAllocationTest
{
    TestClass instance;

    const int WarmupCount = 5;
    const int MeasurementCount = 1000;

    void MeasureGCAlloc(Action action)
    {
        if (instance == null) instance = new();

        for (int i = 0; i < WarmupCount; i++) action();
        GC.Collect();
        var prevMemory = GC.GetTotalMemory(false);
        for (int i = 0; i < MeasurementCount; i++) action();
        var allocation = (GC.GetTotalMemory(false) - prevMemory) / MeasurementCount;

        Measure.Custom(new SampleGroup("GC Alloc", SampleUnit.Byte), allocation);
    }

    [Test, Performance]
    public void AnimeTaskSetup()
    {
        AnimeTaskTester.Init();

        MeasureGCAlloc(() =>
        {
            AnimeTaskTester.CreateFloatTween(instance, 10f);
        });

        AnimeTaskTester.CleanUp();
    }

    [Test, Performance]
    public void AnimeRxSetup()
    {
        AnimeRxTester.Init();

        MeasureGCAlloc(() =>
        {
            AnimeRxTester.CreateFloatTween(instance, 10f);
        });

        AnimeRxTester.CleanUp();
    }

    [Test, Performance]
    public void UnityTweensSetup()
    {
        UnityTweensTester.Init();

        MeasureGCAlloc(() =>
        {
            UnityTweensTester.CreateFloatTween(instance, 10f);
        });

        UnityTweensTester.CleanUp();
    }

    [Test, Performance]
    public void GoKitSetup()
    {
        MeasureGCAlloc(() =>
        {
            GoKitTester.CreateFloatTween(instance, 10f);
        });

        GoKitTester.CleanUp(instance);
    }

    [Test, Performance]
    public void ZestKitSetup()
    {
        MeasureGCAlloc(() =>
        {
            ZestKitTester.CreateFloatTween(instance, 10f);
        });

        ZestKitTester.CleanUp();
    }


    [Test, Performance]
    public void LeanTweenSetup()
    {
        LeanTweenTester.Init(WarmupCount + MeasurementCount + 100);

        MeasureGCAlloc(() =>
        {
            LeanTweenTester.CreateFloatTween(instance, 10f);
        });

        LeanTweenTester.CleanUp();
    }

    [Test, Performance]
    public void DOTweenSetup()
    {
        DOTweenTester.Init(WarmupCount + MeasurementCount + 100, 0);

        MeasureGCAlloc(() =>
        {
            DOTweenTester.CreateFloatTween(instance, 10f);
        });

        DOTweenTester.CleanUp();
    }

    [Test, Performance]
    public void PrimeTweenSetup()
    {
        PrimeTweenTester.Init(WarmupCount + MeasurementCount + 100);

        MeasureGCAlloc(() =>
        {
            PrimeTweenTester.CreateFloatTween(instance, 10f);
        });

        PrimeTweenTester.CleanUp();
    }

    [Test, Performance]
    public void MagicTweenSetup()
    {
        MagicTween.Core.TweenPropertyAccessorUnsafePool<float>.Prewarm(WarmupCount + MeasurementCount + 100);

        MeasureGCAlloc(() =>
        {
            MagicTweenTester.CreateFloatTween(instance, 10f);
        });

        MagicTweenTester.CleanUp();
    }
}
