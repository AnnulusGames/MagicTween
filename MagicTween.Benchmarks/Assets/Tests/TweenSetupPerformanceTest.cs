using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using Unity.Collections;
using Unity.Entities;

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
        Measure.Method(() =>
        {
            AnimeTaskTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            AnimeTaskTester.Init();
        })
        .CleanUp(() =>
        {
            AnimeTaskTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void AnimeRxSetup()
    {
        Measure.Method(() =>
        {
            AnimeRxTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            AnimeRxTester.Init();
        })
        .CleanUp(() =>
        {
            AnimeRxTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void UnityTweensSetup()
    {
        Measure.Method(() =>
        {
            UnityTweensTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            UnityTweensTester.Init();
        })
        .CleanUp(() =>
        {
            UnityTweensTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void GoKitSetup()
    {
        Measure.Method(() =>
        {
            GoKitTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            
        })
        .CleanUp(() =>
        {
            GoKitTester.CleanUp(array);
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void ZestKitSetup()
    {
        Measure.Method(() =>
        {
            ZestKitTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            
        })
        .CleanUp(() =>
        {
            ZestKitTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }


    [Test, Performance]
    public void LeanTweenSetup()
    {
        Measure.Method(() =>
        {
            LeanTweenTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            LeanTweenTester.Init(array.Length);
        })
        .CleanUp(() =>
        {
            LeanTweenTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void PrimeTweenSetup()
    {
        Measure.Method(() =>
        {
            PrimeTweenTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            PrimeTweenTester.Init(array.Length);
        })
        .CleanUp(() =>
        {
            PrimeTweenTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void DOTweenSetup()
    {
        Measure.Method(() =>
        {
            DOTweenTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {
            DOTweenTester.Init(array.Length + 1, 0);
        })
        .CleanUp(() =>
        {
            DOTweenTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void MagicTweenSetup()
    {
        Measure.Method(() =>
        {
            MagicTweenTester.CreateFloatTweens(array, 10f);
        })
        .SetUp(() =>
        {

        })
        .CleanUp(() =>
        {
            MagicTweenTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }

    [Test, Performance]
    public void MagicTweenECSSetup()
    {
        Measure.Method(() =>
        {
            MagicTweenECSTester.CreateFloatTweens(entities, 10f);
        })
        .SetUp(() =>
        {
            
        })
        .CleanUp(() =>
        {
            MagicTweenECSTester.CleanUp();
        })
        .WarmupCount(WarmupCount)
        .MeasurementCount(MeasurementCount)
        .Run();
    }
}
