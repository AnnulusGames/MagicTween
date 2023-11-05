using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace MagicTween.Benchmark
{
    public sealed class TransformPositionStartupBenchmark
    {
        Transform[] transforms;
        const int WarmupCount = 0;
        const int MeasurementCount = 1;
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
            GC.Collect();
        }

        [Test, Performance]
        public void AnimeTask()
        {
            AnimeTaskHelper.Init();

            Measure.Method(() =>
            {
                AnimeTaskHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            AnimeTaskHelper.CleanUp();
        }

        [Test, Performance]
        public void AnimeRx()
        {
            AnimeRxHelper.Init();

            Measure.Method(() =>
            {
                AnimeRxHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            AnimeRxHelper.CleanUp();
        }

        [Test, Performance]
        public void UnityTweens()
        {
            UnityTweensHelper.Init();

            Measure.Method(() =>
            {
                UnityTweensHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            UnityTweensHelper.CleanUp();
        }

        [Test, Performance]
        public void GoKit()
        {
            Measure.Method(() =>
            {
                GoKitHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            GoKitHelper.CleanUp(transforms);
        }

        [Test, Performance]
        public void ZestKit()
        {
            Measure.Method(() =>
            {
                ZestKitHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            ZestKitHelper.CleanUp();
        }


        [Test, Performance]
        public void LeanTween()
        {
            LeanTweenHelper.Init(transforms.Length);

            Measure.Method(() =>
            {
                LeanTweenHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            LeanTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void PrimeTween()
        {
            PrimeTweenHelper.Init(transforms.Length);

            Measure.Method(() =>
            {
                PrimeTweenHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            PrimeTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void DOTween()
        {
            DOTweenHelper.Init(transforms.Length + 1, 0);

            Measure.Method(() =>
            {
                DOTweenHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            DOTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void MagicTween()
        {
            Measure.Method(() =>
            {
                MagicTweenHelper.CreatePositionTweens(transforms, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            MagicTweenHelper.CleanUp();
        }
    }
}