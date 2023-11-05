using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween.Plugins
{
    public struct PunchTweenOptions : ITweenOptions
    {
        public int frequency;
        public float dampingRatio;
    }

    [BurstCompile]
    public readonly struct PunchTweenPlugin : ITweenPlugin<float, PunchTweenOptions>
    {
        [BurstCompile]
        public float Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float>>(entity).value;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    [BurstCompile]
    public readonly struct Punch2TweenPlugin : ITweenPlugin<float2, PunchTweenOptions>
    {
        [BurstCompile]
        public float2 Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float2>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float2>>(entity).value;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    [BurstCompile]
    public readonly struct Punch3TweenPlugin : ITweenPlugin<float3, PunchTweenOptions>
    {
        [BurstCompile]
        public float3 Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float3>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).value;
            var strength = entityManager.GetComponentData<VibrationStrength<float3>>(entity).value;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }
}