using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Plugins;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public readonly partial struct ShakeTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float>> start;
        readonly RefRW<TweenValue<float>> current;
        readonly RefRO<VibrationStrength<float>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float StartValue => start.ValueRO.value;
        public float CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float Strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions Options => optionsRef.ValueRO.value;
        public ref ShakeRandomState Random => ref randomRef.ValueRW;
    }

    public readonly partial struct Shake2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float2>> start;
        readonly RefRW<TweenValue<float2>> current;
        readonly RefRO<VibrationStrength<float2>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float2 StartValue => start.ValueRO.value;
        public float2 CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float2 Strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions Options => optionsRef.ValueRO.value;

        public ref ShakeRandomState Random => ref randomRef.ValueRW;
    }

    public readonly partial struct Shake3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float3>> start;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<VibrationStrength<float3>> strengthRef;
        readonly RefRO<TweenOptions<ShakeTweenOptions>> optionsRef;
        readonly RefRW<ShakeRandomState> randomRef;

        public float3 StartValue => start.ValueRO.value;
        public float3 CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public float3 Strength => strengthRef.ValueRO.value;
        public ShakeTweenOptions Options => optionsRef.ValueRO.value;

        public ref ShakeRandomState Random => ref randomRef.ValueRW;
    }
}