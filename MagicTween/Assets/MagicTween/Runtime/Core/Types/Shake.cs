using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenOptions<ShakeTweenOptions>))]

namespace MagicTween.Core
{
    public struct ShakeTweenOptions : ITweenOptions
    {
        public int frequency;
        public float dampingRatio;
        public uint randomSeed;
    }

    public struct ShakeRandomState : IComponentData
    {
        public Random random;
    }

    public readonly partial struct ShakeTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float>> start;
        readonly RefRW<TweenValue<float>> current;
        readonly RefRO<VibrationStrength<float>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float startValue => start.ValueRO.value;
        public float currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions options => optionsRef.ValueRO.value;

        public ref ShakeRandomState random => ref randomRef.ValueRW;
    }

    [BurstCompile]
    public readonly struct ShakeTweenPlugin : ITweenPlugin<float>
    {
        public float Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, out float result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, t, ref random, out result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });
        }

        [BurstCompile]
        public static void EvaluateCore(in float startValue, in ShakeTweenOptions options, in float strength, float t, ref Random random, out float result)
        {
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var s);
            float multipliar;
            if (options.randomSeed == 0)
            {
                multipliar = SharedRandom.NextFloat(-1f, 1f);
            }
            else
            {
                if (random.state == 0) random.InitState(options.randomSeed);
                multipliar = random.NextFloat(-1f, 1f);
            }
            result = startValue + s * multipliar;
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct ShakeTweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, ShakeTweenAspect valueAspect)
            {
                ShakeTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.options, valueAspect.strength, aspect.progress, ref valueAspect.random.random, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    public readonly partial struct Shake2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float2>> start;
        readonly RefRW<TweenValue<float2>> current;
        readonly RefRO<VibrationStrength<float2>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float2 startValue => start.ValueRO.value;
        public float2 currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float2 strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions options => optionsRef.ValueRO.value;

        public ref ShakeRandomState random => ref randomRef.ValueRW;
    }

    [BurstCompile]
    public readonly struct Shake2TweenPlugin : ITweenPlugin<float2>
    {
        public float2 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, out float2 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float2>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float2>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, t, ref random, out result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });
        }

        [BurstCompile]
        public static void EvaluateCore(in float2 startValue, in ShakeTweenOptions options, in float2 strength, float t, ref Random random, out float2 result)
        {
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var s);
            float2 multipliar;
            if (options.randomSeed == 0)
            {
                multipliar = new float2(SharedRandom.NextFloat(-1f, 1f), SharedRandom.NextFloat(-1f, 1f));
            }
            else
            {
                if (random.state == 0) random.InitState(options.randomSeed);
                multipliar = new float2(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f));
            }
            result = startValue + new float2(s.x * multipliar.x, s.y * multipliar.y);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Shake2TweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, Shake2TweenAspect valueAspect)
            {
                Shake2TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.options, valueAspect.strength, aspect.progress, ref valueAspect.random.random, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    public readonly partial struct Shake3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float3>> start;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<VibrationStrength<float3>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float3 startValue => start.ValueRO.value;
        public float3 currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float3 strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions options => optionsRef.ValueRO.value;

        public ref ShakeRandomState random => ref randomRef.ValueRW;
    }

    [BurstCompile]
    public readonly struct Shake3TweenPlugin : ITweenPlugin<float3>
    {
        public float3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, out float3 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float3>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float3>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, t, ref random, out result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });
        }

        [BurstCompile]
        public static void EvaluateCore(in float3 startValue, in ShakeTweenOptions options, in float3 strength, float t, ref Random random, out float3 result)
        {
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var s);
            float3 multipliar;
            if (options.randomSeed == 0)
            {
                multipliar = new float3(SharedRandom.NextFloat(-1f, 1f), SharedRandom.NextFloat(-1f, 1f), SharedRandom.NextFloat(-1f, 1f));
            }
            else
            {
                if (random.state == 0) random.InitState(options.randomSeed);
                multipliar = new float3(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f));
            }
            result = startValue + new float3(s.x * multipliar.x, s.y * multipliar.y, s.z * multipliar.z);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Shake3TweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, Shake3TweenAspect valueAspect)
            {
                Shake3TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.options, valueAspect.strength, aspect.progress, ref valueAspect.random.random, out var result);
                valueAspect.currentValue = result;
            }
        }
    }
}