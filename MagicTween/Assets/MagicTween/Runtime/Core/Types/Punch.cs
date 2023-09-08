using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using MagicTween.Core;

[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float>))]
[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float2>))]
[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<PunchTweenOptions>))]

namespace MagicTween.Core
{
    public struct PunchTweenOptions : ITweenOptions
    {
        public int frequency;
        public float dampingRatio;
    }

    public struct VibrationStrength<TValue> : IComponentData
        where TValue : unmanaged
    {
        public TValue strength;
    }

    public readonly partial struct PunchTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float>> start;
        readonly RefRW<TweenValue<float>> current;
        readonly RefRO<VibrationStrength<float>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;

        public float startValue => start.ValueRO.value;
        public float currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float strength => strengthRef.ValueRO.strength;
        public PunchTweenOptions options => optionsRef.ValueRO.options;
    }

    public readonly struct PunchTweenPlugin : ITweenPlugin<float>
    {
        public float Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<float>>(entity).value;
            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).options;
            var strength = TweenWorld.EntityManager.GetComponentData<VibrationStrength<float>>(entity).strength;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var result);
            return startValue + result;
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PunchTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, PunchTweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.strength, valueAspect.options.frequency, valueAspect.options.dampingRatio, aspect.progress, out var result);
                valueAspect.currentValue = valueAspect.startValue + result;
            }
        }
    }

    public readonly partial struct Punch2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float2>> start;
        readonly RefRW<TweenValue<float2>> current;
        readonly RefRO<VibrationStrength<float2>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;

        public float2 startValue => start.ValueRO.value;
        public float2 currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float2 strength => strengthRef.ValueRO.strength;
        public PunchTweenOptions options => optionsRef.ValueRO.options;
    }

    [BurstCompile]
    public readonly struct Punch2TweenPlugin : ITweenPlugin<float2>
    {
        public float2 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<float2>>(entity).value;
            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).options;
            var strength = TweenWorld.EntityManager.GetComponentData<VibrationStrength<float2>>(entity).strength;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var result);
            return startValue + result;
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Punch2TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Punch2TweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.strength, valueAspect.options.frequency, valueAspect.options.dampingRatio, aspect.progress, out var result);
                valueAspect.currentValue = valueAspect.startValue + result;
            }
        }
    }

    public readonly partial struct Punch3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float3>> start;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<VibrationStrength<float3>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;

        public float3 startValue => start.ValueRO.value;
        public float3 currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float3 strength => strengthRef.ValueRO.strength;
        public PunchTweenOptions options => optionsRef.ValueRO.options;
    }

    [BurstCompile]
    public readonly struct Punch3TweenPlugin : ITweenPlugin<float3>
    {
        public float3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<float3>>(entity).value;
            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(entity).options;
            var strength = TweenWorld.EntityManager.GetComponentData<VibrationStrength<float3>>(entity).strength;
            VibrationUtils.EvaluateStrength(strength, options.frequency, options.dampingRatio, t, out var result);
            return startValue + result;
        }
    }
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Punch3TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Punch3TweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.strength, valueAspect.options.frequency, valueAspect.options.dampingRatio, aspect.progress, out var result);
                valueAspect.currentValue = valueAspect.startValue + result;
            }
        }
    }

}