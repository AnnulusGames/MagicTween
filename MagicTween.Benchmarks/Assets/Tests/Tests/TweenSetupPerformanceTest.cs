using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using Unity.Collections;
using Unity.Entities;
using MagicTween;

public class TweenSetupPerformanceTest
{
    TestClass[] array;
    NativeArray<Entity> entities;

    const int WarmupCount = 0;
    const int MeasurementCount = 1;
    const int TweenCount = 64000;

    [SetUp]
    public void Init()
    {
        array = new TestClass[TweenCount];
        for (int i = 0; i < array.Length; i++) array[i] = new();
        entities = MagicTweenECSTester.CreateEntities(TweenCount, Allocator.Persistent);
    }

    [TearDown]
    public void TearDown()
    {
        array = null;
        entities.Dispose();
        GC.Collect();
    }

    [Test, Performance]
    public void AnimeTaskSetup()
    {
        AnimeTaskTester.Init();

        Measure.Method(() =>
        {
            AnimeTaskTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        AnimeTaskTester.CleanUp();
    }

    [Test, Performance]
    public void AnimeRxSetup()
    {
        AnimeRxTester.Init();

        Measure.Method(() =>
        {
            AnimeRxTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        AnimeRxTester.CleanUp();
    }

    [Test, Performance]
    public void UnityTweensSetup()
    {
        UnityTweensTester.Init();

        Measure.Method(() =>
        {
            UnityTweensTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        UnityTweensTester.CleanUp();
    }

    [Test, Performance]
    public void GoKitSetup()
    {
        Measure.Method(() =>
        {
            GoKitTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        GoKitTester.CleanUp(array);
    }

    [Test, Performance]
    public void ZestKitSetup()
    {
        Measure.Method(() =>
        {
            ZestKitTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        ZestKitTester.CleanUp();
    }


    [Test, Performance]
    public void LeanTweenSetup()
    {
        LeanTweenTester.Init(array.Length);

        Measure.Method(() =>
        {
            LeanTweenTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        LeanTweenTester.CleanUp();
    }

    [Test, Performance]
    public void PrimeTweenSetup()
    {
        PrimeTweenTester.Init(array.Length);

        Measure.Method(() =>
        {
            PrimeTweenTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        PrimeTweenTester.CleanUp();
    }

    [Test, Performance]
    public void DOTweenSetup()
    {
        DOTweenTester.Init(array.Length + 1, 0);

        Measure.Method(() =>
        {
            DOTweenTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        DOTweenTester.CleanUp();
    }

    [Test, Performance]
    public void MagicTweenSetup()
    {
        Measure.Method(() =>
        {
            MagicTweenTester.CreateFloatTweens(array, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        MagicTweenTester.CleanUp();
    }

    [Test, Performance]
    public void MagicTweenECSSetup()
    {
        Measure.Method(() =>
        {
            MagicTweenECSTester.CreateFloatTweens(entities, 10f);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();

        MagicTweenECSTester.CleanUp();
    }
}
