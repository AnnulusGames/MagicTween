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
        const int MeasurementCount = 10000;

        void MeasureGCAlloc(Action action)
        {
            if (instance == null) instance = new();

            for (int i = 0; i < WarmupCount; i++) action();
            GC.Collect();
            var prevMemory = GC.GetTotalMemory(false);
            for (int i = 0; i < MeasurementCount; i++) action();
            var allocation = (GC.GetTotalMemory(false) - prevMemory) / MeasurementCount;

            Measure.Custom(new SampleGroup("GC Alloc", SampleUnit.Byte), allocation);
        }

        [Test, Performance]
        public void AnimeTaskSetup()
        {
            AnimeTaskHelper.Init();

            MeasureGCAlloc(() =>
            {
                AnimeTaskHelper.CreateFloatTween(instance, 10f);
            });

            AnimeTaskHelper.CleanUp();
        }

        [Test, Performance]
        public void AnimeRxSetup()
        {
            AnimeRxHelper.Init();

            MeasureGCAlloc(() =>
            {
                AnimeRxHelper.CreateFloatTween(instance, 10f);
            });

            AnimeRxHelper.CleanUp();
        }

        [Test, Performance]
        public void UnityTweensSetup()
        {
            UnityTweensHelper.Init();

            MeasureGCAlloc(() =>
            {
                UnityTweensHelper.CreateFloatTween(instance, 10f);
            });

            UnityTweensHelper.CleanUp();
        }

        [Test, Performance]
        public void GoKitSetup()
        {
            MeasureGCAlloc(() =>
            {
                GoKitHelper.CreateFloatTween(instance, 10f);
            });

            GoKitHelper.CleanUp(instance);
        }

        [Test, Performance]
        public void ZestKitSetup()
        {
            MeasureGCAlloc(() =>
            {
                ZestKitHelper.CreateFloatTween(instance, 10f);
            });

            ZestKitHelper.CleanUp();
        }


        [Test, Performance]
        public void LeanTweenSetup()
        {
            LeanTweenHelper.Init(WarmupCount + MeasurementCount + 100);

            MeasureGCAlloc(() =>
            {
                LeanTweenHelper.CreateFloatTween(instance, 10f);
            });

            LeanTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void DOTweenSetup()
        {
            DOTweenHelper.Init(WarmupCount + MeasurementCount + 100, 0);

            MeasureGCAlloc(() =>
            {
                DOTweenHelper.CreateFloatTween(instance, 10f);
            });

            DOTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void PrimeTweenSetup()
        {
            PrimeTweenHelper.Init(WarmupCount + MeasurementCount + 100);

            MeasureGCAlloc(() =>
            {
                PrimeTweenHelper.CreateFloatTween(instance, 10f);
            });

            PrimeTweenHelper.CleanUp();
        }

        [Test, Performance]
        public void MagicTweenSetup()
        {
            MagicTween.Core.TweenPropertyAccessorNoAllocPool<float>.Prewarm(WarmupCount + MeasurementCount + 100);

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