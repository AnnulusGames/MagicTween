using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Plugins;
using MagicTween.Core.Components;

namespace MagicTween.Core.Aspects
{
    public readonly partial struct PunchTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float>> start;
        readonly RefRW<TweenValue<float>> current;
        readonly RefRO<VibrationStrength<float>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;
#pragma warning disable CS0414
        readonly RefRO<TweenPluginTag<PunchTweenPlugin>> pluginRef;
#pragma warning restore CS0414

        public float StartValue => start.ValueRO.value;
        public float CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float Strength => strengthRef.ValueRO.value;
        public PunchTweenOptions Options => optionsRef.ValueRO.value;
    }

    public readonly partial struct Punch2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float2>> start;
        readonly RefRW<TweenValue<float2>> current;
        readonly RefRO<VibrationStrength<float2>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;
#pragma warning disable CS0414
        readonly RefRO<TweenPluginTag<Punch2TweenPlugin>> pluginRef;
#pragma warning restore CS0414

        public float2 StartValue => start.ValueRO.value;
        public float2 CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float2 Strength => strengthRef.ValueRO.value;
        public PunchTweenOptions Options => optionsRef.ValueRO.value;
    }

    public readonly partial struct Punch3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float3>> start;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<VibrationStrength<float3>> strengthRef;
        readonly RefRO<TweenOptions<PunchTweenOptions>> optionsRef;
#pragma warning disable CS0414
        readonly RefRO<TweenPluginTag<Punch3TweenPlugin>> pluginRef;
#pragma warning restore CS0414

        public float3 StartValue => start.ValueRO.value;
        public float3 CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float3 Strength => strengthRef.ValueRO.value;
        public PunchTweenOptions Options => optionsRef.ValueRO.value;
    }

}