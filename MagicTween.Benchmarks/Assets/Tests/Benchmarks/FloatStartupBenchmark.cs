using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Benchmark
{
    public sealed class FloatStartupBenchmark
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
            entities = MagicTweenECSHelper.CreateEntities(TweenCount, Allocator.Persistent);
        }

        [TearDown]
        public void TearDown()
        {
            array = null;
            entities.Dispose();
            GC.Collect();
        }

        [Test, Performance]
        public void AnimeTask()
        {
            AnimeTaskHelper.Init();

            Measure.Method(() =>
            {
                AnimeTaskHelper.CreateFloatTweens(array, 10f);
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
                AnimeRxHelper.CreateFloatTweens(array, 10f);
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
                UnityTweensHelper.CreateFloatTweens(array, 10f);
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
                GoKitHelper.CreateFloatTweens(array, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            GoKitHelper.CleanUp(array);
        }

        [Test, Performance]
        public void ZestKit()
        {
            Measure.Method(() =>
            {
                ZestKitHelper.CreateFloatTweens(array, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            ZestKitHelper.CleanUp();
        }


        [Test, Performance]
        public void LeanTween()
        {
            LeanTweenHelper.Init(array.Length);

            Measure.Method(() =>
            {
                LeanTweenHelper.CreateFloatTweens(array, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            LeanTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void PrimeTween()
        {
            PrimeTweenHelper.Init(array.Length);

            Measure.Method(() =>
            {
                PrimeTweenHelper.CreateFloatTweens(array, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            PrimeTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void DOTween()
        {
            DOTweenHelper.Init(array.Length + 1, 0);

            Measure.Method(() =>
            {
                DOTweenHelper.CreateFloatTweens(array, 10f);
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
                MagicTweenHelper.CreateFloatTweens(array, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            MagicTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void MagicTweenECS()
        {
            Measure.Method(() =>
            {
                MagicTweenECSHelper.CreateFloatTweens(entities, 10f);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .Run();

            MagicTweenECSHelper.CleanUp();
        }
    }
}