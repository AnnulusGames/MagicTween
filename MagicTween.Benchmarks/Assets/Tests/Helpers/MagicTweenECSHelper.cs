using System;
using System.Runtime.CompilerServices;
using MagicTween;
using Unity.Entities;
using Unity.Collections;
using Unity.Burst;

namespace MagicTween.Benchmark
{
    [BurstCompile]
    public struct TestTweenTranslator : ITweenTranslator<float, TestData>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref TestData component, in float value)
        {
            component.value = value;
        }

        [BurstCompile]
        public float GetValue(ref TestData component)
        {
            return component.value;
        }
    }

    public partial class TestTweenTranslationSystem : TweenTranslationSystemBase<float, TestData, TestTweenTranslator> { }

    public static class MagicTweenECSHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            Tween.Clear();
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NativeArray<Entity> CreateEntities(int count, Allocator allocator)
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.DestroyEntity(entityManager.CreateEntityQuery(typeof(TestData)));
            var archetype = entityManager.CreateArchetype(typeof(TestData));
            return entityManager.CreateEntity(archetype, count, allocator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(in NativeArray<Entity> entities, float duration)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                Tween.Entity.FromTo<TestTweenTranslator>(entities[i], 0f, 10f, duration);
            }
        }
    }

}