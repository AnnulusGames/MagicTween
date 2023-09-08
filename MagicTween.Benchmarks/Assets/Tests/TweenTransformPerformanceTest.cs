using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

public class TweenTransformPerformanceTest
{
    Transform[] transforms;
    const int WarmupCount = 3;
    const int MeasurementCount = 600;
    const int TweenCount = 25000;

    [SetUp]
    public void Setup()
    {
        transforms = new Transform[TweenCount];
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i] = new GameObject().transform;
        }
    }

    [TearDown]
    public void TearDown()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject.Destroy(transforms[i].gameObject);
        }
        transforms = null;
    }

    [UnityTest, Performance]
    public IEnumerator AnimeTaskUpdateTest()
    {
        AnimeTaskTester.Init();
        AnimeTaskTester.CreatePositionTweens(transforms, 1000f);
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
        AnimeRxTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        AnimeRxTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator UnityTweensUpdateTest()
    {
        UnityTweensTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        UnityTweensTester.CleanUp();
    }


    [UnityTest, Performance]
    public IEnumerator GoKitUpdateTest()
    {
        GoKitTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        GoKitTester.CleanUp(transforms);
    }

    [UnityTest, Performance]
    public IEnumerator ZestKitUpdateTest()
    {
        ZestKitTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        ZestKitTester.CleanUp();
    }


    [UnityTest, Performance]
    public IEnumerator LeanTweenUpdateTest()
    {
        LeanTweenTester.Init(transforms.Length);
        LeanTweenTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        LeanTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator PrimeTweenUpdateTest()
    {
        PrimeTweenTester.Init(transforms.Length);
        PrimeTweenTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        PrimeTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator DOTweenUpdateTest()
    {
        DOTweenTester.Init(transforms.Length + 1, 0);
        DOTweenTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        DOTweenTester.CleanUp();
    }

    [UnityTest, Performance]
    public IEnumerator MagicTweenUpdateTest()
    {
        MagicTweenTester.CreatePositionTweens(transforms, 1000f);
        yield return Measure.Frames()
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();
        MagicTweenTester.CleanUp();
    }
}