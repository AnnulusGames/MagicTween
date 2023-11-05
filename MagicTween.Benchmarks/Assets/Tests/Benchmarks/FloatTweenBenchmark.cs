using System;
using System.Collections;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace MagicTween.Benchmark
{
    public class FloatTweenBenchmark
    {
        TestClass[] array;
        NativeArray<Entity> entities;

        const int WarmupCount = 3;
        const int MeasurementCount = 600;
        const int TweenCount = 32000;

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

        [UnityTest, Performance]
        public IEnumerator AnimeTask()
        {
            AnimeTaskHelper.Init();
            AnimeTaskHelper.CreateFloatTweens(array, 1000f);
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
            AnimeRxHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            AnimeRxHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator UnityTweens()
        {
            UnityTweensHelper.Init();
            UnityTweensHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            UnityTweensHelper.CleanUp();
        }


        [UnityTest, Performance]
        public IEnumerator GoKit()
        {
            GoKitHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            GoKitHelper.CleanUp(array);
        }

        [UnityTest, Performance]
        public IEnumerator ZestKit()
        {
            ZestKitHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            ZestKitHelper.CleanUp();
        }


        [UnityTest, Performance]
        public IEnumerator LeanTween()
        {
            LeanTweenHelper.Init(array.Length);
            LeanTweenHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            LeanTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator PrimeTween()
        {
            PrimeTweenHelper.Init(array.Length);
            PrimeTweenHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            PrimeTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator DOTween()
        {
            DOTweenHelper.Init(array.Length + 1, 0);
            DOTweenHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            DOTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator MagicTween()
        {
            MagicTweenHelper.CreateFloatTweens(array, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            MagicTweenHelper.CleanUp();
        }

        [UnityTest, Performance]
        public IEnumerator MagicTweenECS()
        {
            MagicTweenECSHelper.CreateFloatTweens(entities, 1000f);
            yield return Measure.Frames()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            MagicTweenECSHelper.CleanUp();
        }
    }
}