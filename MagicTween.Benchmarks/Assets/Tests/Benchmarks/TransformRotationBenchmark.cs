using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace MagicTween.Benchmark
{
    public sealed class TransformRotationBenchmark
    {
        Transform[] transforms;
        const int WarmupCount = 3;
        const int MeasurementCount = 600;
        const int TweenCount = 50000;

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
        public IEnumerator AnimeTask()
        {
            AnimeTaskHelper.Init();
            AnimeTaskHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            AnimeTaskHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator AnimeRx()
        {
            AnimeRxHelper.Init();
            AnimeRxHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            AnimeRxHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator UnityTweens()
        {
            UnityTweensHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            UnityTweensHelper.CleanUp();
        }


        [UnityTest, Performance]
        public IEnumerator GoKit()
        {
            GoKitHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            GoKitHelper.CleanUp(transforms);
        }

        [UnityTest, Performance]
        public IEnumerator ZestKit()
        {
            ZestKitHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            ZestKitHelper.CleanUp();
        }


        [UnityTest, Performance]
        public IEnumerator LeanTween()
        {
            LeanTweenHelper.Init(transforms.Length);
            LeanTweenHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            LeanTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator PrimeTween()
        {
            PrimeTweenHelper.Init(transforms.Length);
            PrimeTweenHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            PrimeTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator DOTween()
        {
            DOTweenHelper.Init(transforms.Length + 1, 0);
            DOTweenHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            DOTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator MagicTween()
        {
            MagicTweenHelper.CreateRotationTweens(transforms, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            MagicTweenHelper.CleanUp();
        }
    }
}