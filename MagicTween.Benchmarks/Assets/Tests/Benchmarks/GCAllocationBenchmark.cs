using System;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.PerformanceTesting;

namespace MagicTween.Benchmark
{
    public sealed class GCAllocationBenchmark
    {
        TestClass instance;

        const int WarmupCount = 5;
        const int MeasurementCount = 1000;

        void MeasureGCAlloc(Action action)
        {
            if (instance == null) instance = new();

            Measure.Method(action)
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .GC()
                .Run();
        }

        [Test, Performance]
        public void AnimeTask()
        {
            AnimeTaskHelper.Init();

            MeasureGCAlloc(() =>
            {
                AnimeTaskHelper.CreateFloatTween(instance, 10f);
            });

            AnimeTaskHelper.CleanUp();
        }

        [Test, Performance]
        public void AnimeRx()
        {
            AnimeRxHelper.Init();

            MeasureGCAlloc(() =>
            {
                AnimeRxHelper.CreateFloatTween(instance, 10f);
            });

            AnimeRxHelper.CleanUp();
        }

        [Test, Performance]
        public void UnityTweens()
        {
            UnityTweensHelper.Init();

            MeasureGCAlloc(() =>
            {
                UnityTweensHelper.CreateFloatTween(instance, 10f);
            });

            UnityTweensHelper.CleanUp();
        }

        [Test, Performance]
        public void GoKit()
        {
            MeasureGCAlloc(() =>
            {
                GoKitHelper.CreateFloatTween(instance, 10f);
            });

            GoKitHelper.CleanUp(instance);
        }

        [Test, Performance]
        public void ZestKit()
        {
            MeasureGCAlloc(() =>
            {
                ZestKitHelper.CreateFloatTween(instance, 10f);
            });

            ZestKitHelper.CleanUp();
        }


        [Test, Performance]
        public void LeanTween()
        {
            LeanTweenHelper.Init(WarmupCount + MeasurementCount + 100);

            MeasureGCAlloc(() =>
            {
                LeanTweenHelper.CreateFloatTween(instance, 10f);
            });

            LeanTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void DOTween()
        {
            DOTweenHelper.Init(WarmupCount + MeasurementCount + 100, 0);

            MeasureGCAlloc(() =>
            {
                DOTweenHelper.CreateFloatTween(instance, 10f);
            });

            DOTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void PrimeTween()
        {
            PrimeTweenHelper.Init(WarmupCount + MeasurementCount + 100);

            MeasureGCAlloc(() =>
            {
                PrimeTweenHelper.CreateFloatTween(instance, 10f);
            });

            PrimeTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void MagicTween()
        {
            Core.TweenDelegatesNoAllocPool<float>.Prewarm(WarmupCount + MeasurementCount + 100);

            // In Unity ECS, managed components are managed as a huge array, but the process of expanding this array may affect GC Allocation measurement.
            // To avoid this, add a Dummy managed component and adjust the array size in advance.
            var world = World.DefaultGameObjectInjectionWorld;
            var archetype = world.EntityManager.CreateArchetype(ComponentType.ReadWrite<DummyManagedComponent>());
            var entities = world.EntityManager.CreateEntity(archetype, WarmupCount + MeasurementCount, Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                world.EntityManager.SetComponentData(entities[i], new DummyManagedComponent());
            }
            world.EntityManager.DestroyEntity(entities);

            MeasureGCAlloc(() =>
            {
                MagicTweenHelper.CreateFloatTween(instance, 10f);
            });

            MagicTweenHelper.CleanUp();
        }

        class DummyManagedComponent : IComponentData { }
    }
}