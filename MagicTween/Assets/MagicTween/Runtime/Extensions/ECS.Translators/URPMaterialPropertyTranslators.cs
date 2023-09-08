#if MAGICTWEEN_SUPPORT_ENTITIES_GRAPHICS && MAGICTWEEN_SUPPORT_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using MagicTween.Translators;

namespace MagicTween.Translators
{
    [BurstCompile]
    public struct URPBaseColorTranslator : ITweenTranslator<float4, URPMaterialPropertyBaseColor>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyBaseColor component, in float4 value) => component.Value = value;

        [BurstCompile]
        public float4 GetValue(ref URPMaterialPropertyBaseColor component) => component.Value;
    }

    [BurstCompile]
    public struct URPBumpScaleTranslator : ITweenTranslator<float, URPMaterialPropertyBumpScale>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyBumpScale component, in float value) => component.Value = value;

        [BurstCompile]
        public float GetValue(ref URPMaterialPropertyBumpScale component) => component.Value;
    }

    [BurstCompile]
    public struct URPCutoffTranslator : ITweenTranslator<float, URPMaterialPropertyCutoff>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyCutoff component, in float value) => component.Value = value;

        [BurstCompile]
        public float GetValue(ref URPMaterialPropertyCutoff component) => component.Value;
    }

    [BurstCompile]
    public struct URPEmissionColorTranslator : ITweenTranslator<float4, URPMaterialPropertyEmissionColor>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyEmissionColor component, in float4 value) => component.Value = value;

        [BurstCompile]
        public float4 GetValue(ref URPMaterialPropertyEmissionColor component) => component.Value;
    }

    [BurstCompile]
    public struct URPMetallicTranslator : ITweenTranslator<float, URPMaterialPropertyMetallic>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyMetallic component, in float value) => component.Value = value;

        [BurstCompile]
        public float GetValue(ref URPMaterialPropertyMetallic component) => component.Value;
    }

    [BurstCompile]
    public struct URPOcclusionStrengthTranslator : ITweenTranslator<float, URPMaterialPropertyOcclusionStrength>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertyOcclusionStrength component, in float value) => component.Value = value;

        [BurstCompile]
        public float GetValue(ref URPMaterialPropertyOcclusionStrength component) => component.Value;
    }

    [BurstCompile]
    public struct URPSmoothnessTranslator : ITweenTranslator<float, URPMaterialPropertySmoothness>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertySmoothness component, in float value) => component.Value = value;

        [BurstCompile]
        public float GetValue(ref URPMaterialPropertySmoothness component) => component.Value;
    }

    [BurstCompile]
    public struct URPSpecColorTranslator : ITweenTranslator<float4, URPMaterialPropertySpecColor>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref URPMaterialPropertySpecColor component, in float4 value) => component.Value = value;

        [BurstCompile]
        public float4 GetValue(ref URPMaterialPropertySpecColor component) => component.Value;
    }
}

namespace MagicTween.Core
{
    [BurstCompile]
    public sealed partial class URPBaseColorTranslationSystem : TweenTranslationSystemBase<float4, URPMaterialPropertyBaseColor, URPBaseColorTranslator> { }

    [BurstCompile]
    public sealed partial class URPBumpScaleTranslationSystem : TweenTranslationSystemBase<float, URPMaterialPropertyBumpScale, URPBumpScaleTranslator> { }

    [BurstCompile]
    public sealed partial class URPCutoffTranslationSystem : TweenTranslationSystemBase<float, URPMaterialPropertyCutoff, URPCutoffTranslator> { }

    [BurstCompile]
    public sealed partial class URPEmissionColorTranslationSystem : TweenTranslationSystemBase<float4, URPMaterialPropertyEmissionColor, URPEmissionColorTranslator> { }

    [BurstCompile]
    public sealed partial class URPMetallicTranslationSystem : TweenTranslationSystemBase<float, URPMaterialPropertyMetallic, URPMetallicTranslator> { }

    [BurstCompile]
    public sealed partial class URPOcclusionStrengthTranslationSystem : TweenTranslationSystemBase<float, URPMaterialPropertyOcclusionStrength, URPOcclusionStrengthTranslator> { }

    [BurstCompile]
    public sealed partial class URPSmoothnessTranslationSystem : TweenTranslationSystemBase<float, URPMaterialPropertySmoothness, URPSmoothnessTranslator> { }

    [BurstCompile]
    public sealed partial class URPSpecColorTranslationSystem : TweenTranslationSystemBase<float4, URPMaterialPropertySpecColor, URPSpecColorTranslator> { }
}

#endif