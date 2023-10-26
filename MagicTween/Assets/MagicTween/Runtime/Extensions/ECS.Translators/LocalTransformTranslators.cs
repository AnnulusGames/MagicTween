using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using MagicTween.Core;
using MagicTween.Translators;

namespace MagicTween.Translators
{
    [BurstCompile]
    public struct PositionTranslator : ITweenTranslator<float3, LocalTransform>
    {
        public Entity TargetEntity { get; set; }
        
        [BurstCompile]
        public void Apply(ref LocalTransform component, in float3 value) => component.Position = value;

        [BurstCompile]
        public float3 GetValue(ref LocalTransform component) => component.Position;
    }

    [BurstCompile]
    public struct PositionXTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile] public void Apply(ref LocalTransform component, in float value)
            => component.Position = new float3(value, component.Position.y, component.Position.z);
        [BurstCompile] public float GetValue(ref LocalTransform component) => component.Position.x;
    }

    [BurstCompile]
    public struct PositionYTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value)
            => component.Position = new float3(component.Position.x, value, component.Position.z);
        [BurstCompile] public float GetValue(ref LocalTransform component) => component.Position.y;
    }

    [BurstCompile]
    public struct PositionZTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value)
            => component.Position = new float3(component.Position.x, component.Position.y, value);
        [BurstCompile] public float GetValue(ref LocalTransform component) => component.Position.z;
    }

    [BurstCompile]
    public struct RotationTranslator : ITweenTranslator<quaternion, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in quaternion value) => component.Rotation = value;

        [BurstCompile]
        public quaternion GetValue(ref LocalTransform component) => component.Rotation;
    }

    [BurstCompile]
    public struct EulerAnglesTranslator : ITweenTranslator<float3, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float3 value) => component.Rotation = MathUtils.ToQuaternion(value);

        [BurstCompile]
        public float3 GetValue(ref LocalTransform component) => MathUtils.ToEulerAngles(component.Rotation);
    }

    [BurstCompile]
    public struct EulerAnglesXTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value)
        {
            var eulerAngles = MathUtils.ToEulerAngles(component.Rotation);
            eulerAngles.x = value;
            component.Rotation = MathUtils.ToQuaternion(eulerAngles);
        }

        [BurstCompile]
        public float GetValue(ref LocalTransform component)
        {
            return MathUtils.ToEulerAngles(component.Rotation).x;
        }
    }

    [BurstCompile]
    public struct EulerAnglesYTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value)
        {
            var eulerAngles = MathUtils.ToEulerAngles(component.Rotation);
            eulerAngles.y = value;
            component.Rotation = MathUtils.ToQuaternion(eulerAngles);
        }

        [BurstCompile]
        public float GetValue(ref LocalTransform component)
        {
            return MathUtils.ToEulerAngles(component.Rotation).y;
        }
    }

    [BurstCompile]
    public struct EulerAnglesZTranslator : ITweenTranslator<float, LocalTransform>
    {
        public Entity TargetEntity { get; set; }

        [BurstCompile]
        public void Apply(ref LocalTransform component, in float value)
        {
            var eulerAngles = MathUtils.ToEulerAngles(component.Rotation);
            eulerAngles.z = value;
            component.Rotation = MathUtils.ToQuaternion(eulerAngles);
        }

        [BurstCompile]
        public float GetValue(ref LocalTransform component)
        {
            return MathUtils.ToEulerAngles(component.Rotation).z;
        }
    }

    [BurstCompile]
    public struct ScaleTranslator : ITweenTranslator<float, LocalTransform>
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
    public sealed partial class PositionTranslationSystem : TweenTranslationSystemBase<float3, LocalTransform, PositionTranslator> { }

    [BurstCompile]
    public sealed partial class PositionXTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, PositionXTranslator> { }

    [BurstCompile]
    public sealed partial class PositionYTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, PositionYTranslator> { }

    [BurstCompile]
    public sealed partial class PositionZTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, PositionZTranslator> { }

    [BurstCompile]
    public sealed partial class RotationTranslationSystem : TweenTranslationSystemBase<quaternion, LocalTransform, RotationTranslator> { }

    [BurstCompile]
    public sealed partial class EulerAnglesTranslationSystem : TweenTranslationSystemBase<float3, LocalTransform, EulerAnglesTranslator> { }

    [BurstCompile]
    public sealed partial class EulerAnglesXTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, EulerAnglesXTranslator> { }

    [BurstCompile]
    public sealed partial class EulerAnglesYTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, EulerAnglesYTranslator> { }

    [BurstCompile]
    public sealed partial class EulerAnglesZTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, EulerAnglesZTranslator> { }

    [BurstCompile]
    public sealed partial class ScaleTranslationSystem : TweenTranslationSystemBase<float, LocalTransform, ScaleTranslator> { }
}