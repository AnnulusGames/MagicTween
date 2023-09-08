using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using MagicTween.Core;
using MagicTween.Translators;

namespace MagicTween.Translators
{
    [BurstCompile]
    public struct LocalTransformPositionTranslator : ITweenTranslator<float3, LocalTransform>
    {
        public Entity TargetEntity { get; set; }
        
        [BurstCompile]
        public void Apply(ref LocalTransform component, in float3 value) => component.Position = value;

        [BurstCompile]
        public float3 GetValue(ref LocalTransform component) => component.Position;
    }

    [BurstCompile]
    public struct LocalTransformRotationTranslator : ITweenTranslator<quaternion, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in quaternion value) => component.Rotation = value;

        [BurstCompile]
        public quaternion GetValue(ref LocalTransform component) => component.Rotation;
    }

    [BurstCompile]
    public struct LocalTransformEulerAnglesTranslator : ITweenTranslator<float3, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float3 value) => component.Rotation = MathUtils.ToQuaternion(value);

        [BurstCompile]
        public float3 GetValue(ref LocalTransform component) => MathUtils.ToEulerAngles(component.Rotation);
    }

    [BurstCompile]
    public struct LocalTransformScaleTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value) => component.Scale = value;

        [BurstCompile]
        public float GetValue(ref LocalTransform component) => component.Scale;
    }

}

namespace MagicTween.Core
{
    [BurstCompile]
    public sealed partial class LocalTransformPositionTranslationSystem : TweenTranslationSystemBase<float3, LocalTransform, LocalTransformPositionTranslator> { }

    [BurstCompile]
    public sealed partial class LocalTransformRotationTranslationSystem : TweenTranslationSystemBase<quaternion, LocalTransform, LocalTransformRotationTranslator> { }

    [BurstCompile]
    public sealed partial class LocalTransformEulerAnglesTranslationSystem : TweenTranslationSystemBase<float3, LocalTransform, LocalTransformEulerAnglesTranslator> { }

    [BurstCompile]
    public sealed partial class LocalTransformScaleTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, LocalTransformScaleTranslator> { }
}