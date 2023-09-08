using System;
using System.Collections;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

public class TweenFloatPerformanceTest
{    
    TestClass[] array;
    NativeArray<Entity> entities;

    const int WarmupCount = 3;
    const int MeasurementCount = 600;
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

    [UnityTest, Performance]
    public IEnumerator AnimeTaskUpdateTest()
    {
        AnimeTaskTester.Init();
        AnimeTaskTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        AnimeTaskTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator AnimeRxUpdateTest()
    {
        AnimeRxTester.Init();
        AnimeRxTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        AnimeRxTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator UnityTweensUpdateTest()
    {
        UnityTweensTester.Init();
        UnityTweensTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        UnityTweensTester.CleanUp();
    }


    [UnityTest, Performance]
    public IEnumerator GoKitUpdateTest()
    {
        GoKitTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        GoKitTester.CleanUp(array);
    }

    [UnityTest, Performance]
    public IEnumerator ZestKitUpdateTest()
    {
        ZestKitTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        ZestKitTester.CleanUp();
    }


    [UnityTest, Performance]
    public IEnumerator LeanTweenUpdateTest()
    {
        LeanTweenTester.Init(array.Length);
        LeanTweenTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        LeanTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator PrimeTweenUpdateTest()
    {
        PrimeTweenTester.Init(array.Length);
        PrimeTweenTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        PrimeTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator DOTweenUpdateTest()
    {
        DOTweenTester.Init(array.Length + 1, 0);
        DOTweenTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        DOTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator MagicTweenUpdateTest()
    {
        MagicTweenTester.CreateFloatTweens(array, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        MagicTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator MagicTweenECSUpdateTest()
    {
        MagicTweenECSTester.CreateFloatTweens(entities, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        MagicTweenECSTester.CleanUp();
    }
}
