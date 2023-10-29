using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween.Plugins
{
    public struct ShakeTweenOptions : ITweenOptions
    {
        public int frequency;
        public float dampingRatio;
        public uint randomSeed;
    }

    [BurstCompile]
    public readonly struct ShakeTweenPlugin : ITweenPluginBase<float>
    {
        [BurstCompile]
        public float Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, context.Progress, ref random, out var result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });

            return result;
        }

        [BurstCompile]
        internal static void EvaluateCore(in float startValue, in ShakeTweenOptions options, in float strength, float t, ref Random random, out float result)
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
    public readonly struct Shake2TweenPlugin : ITweenPluginBase<float2>
    {
        [BurstCompile]
        public float2 Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float2>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float2>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, context.Progress, ref random, out var result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });

            return result;
        }

        [BurstCompile]
        internal static void EvaluateCore(in float2 startValue, in ShakeTweenOptions options, in float2 strength, float t, ref Random random, out float2 result)
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
    public readonly struct Shake3TweenPlugin : ITweenPluginBase<float3>
    {
        [BurstCompile]
        public float3 Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float3>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float3>>(entity).value;
            var random = entityManager.GetComponentData<ShakeRandomState>(entity).random;
            EvaluateCore(startValue, options, strength, context.Progress, ref random, out var result);
            entityManager.SetComponentData<ShakeRandomState>(entity, new() { random = random });

            return result;
        }

        [BurstCompile]
        internal static void EvaluateCore(in float3 startValue, in ShakeTweenOptions options, in float3 strength, float t, ref Random random, out float3 result)
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
}