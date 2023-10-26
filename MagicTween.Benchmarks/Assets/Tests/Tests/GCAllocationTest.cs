using System;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.PerformanceTesting;

public class GCAllocationTest
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
        AnimeTaskTester.Init();

        MeasureGCAlloc(() =>
        {
            AnimeTaskTester.CreateFloatTween(instance, 10f);
        });

        AnimeTaskTester.CleanUp();
    }

    [Test, Performance]
    public void AnimeRxSetup()
    {
        AnimeRxTester.Init();

        MeasureGCAlloc(() =>
        {
            AnimeRxTester.CreateFloatTween(instance, 10f);
        });

        AnimeRxTester.CleanUp();
    }

    [Test, Performance]
    public void UnityTweensSetup()
    {
        UnityTweensTester.Init();

        MeasureGCAlloc(() =>
        {
            UnityTweensTester.CreateFloatTween(instance, 10f);
        });

        UnityTweensTester.CleanUp();
    }

    [Test, Performance]
    public void GoKitSetup()
    {
        MeasureGCAlloc(() =>
        {
            GoKitTester.CreateFloatTween(instance, 10f);
        });

        GoKitTester.CleanUp(instance);
    }

    [Test, Performance]
    public void ZestKitSetup()
    {
        MeasureGCAlloc(() =>
        {
            ZestKitTester.CreateFloatTween(instance, 10f);
        });

        ZestKitTester.CleanUp();
    }


    [Test, Performance]
    public void LeanTweenSetup()
    {
        LeanTweenTester.Init(WarmupCount + MeasurementCount + 100);

        MeasureGCAlloc(() =>
        {
            LeanTweenTester.CreateFloatTween(instance, 10f);
        });

        LeanTweenTester.CleanUp();
    }

    [Test, Performance]
    public void DOTweenSetup()
    {
        DOTweenTester.Init(WarmupCount + MeasurementCount + 100, 0);

        MeasureGCAlloc(() =>
        {
            DOTweenTester.CreateFloatTween(instance, 10f);
        });

        DOTweenTester.CleanUp();
    }

    [Test, Performance]
    public void PrimeTweenSetup()
    {
        PrimeTweenTester.Init(WarmupCount + MeasurementCount + 100);

        MeasureGCAlloc(() =>
        {
            PrimeTweenTester.CreateFloatTween(instance, 10f);
        });

        PrimeTweenTester.CleanUp();
    }

    [Test, Performance]
    public void MagicTweenSetup()
    {
        MagicTween.Core.TweenPropertyAccessorUnsafePool<float>.Prewarm(WarmupCount + MeasurementCount + 100);

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
            MagicTweenTester.CreateFloatTween(instance, 10f);
        });

        MagicTweenTester.CleanUp();
    }

    class DummyManagedComponent : IComponentData { }
}
