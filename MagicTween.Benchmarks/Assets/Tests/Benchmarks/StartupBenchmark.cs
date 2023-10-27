using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Benchmark
{
    public sealed class StartupBenchmark
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
        public void AnimeTaskSetup()
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
        public void AnimeRxSetup()
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
        public void UnityTweensSetup()
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
        public void GoKitSetup()
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
        public void ZestKitSetup()
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
        public void LeanTweenSetup()
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
        public void PrimeTweenSetup()
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
        public void DOTweenSetup()
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
        public void MagicTweenSetup()
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
        public void MagicTweenECSSetup()
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