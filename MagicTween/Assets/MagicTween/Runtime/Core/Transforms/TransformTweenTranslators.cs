using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms
{
    [BurstCompile]
    internal struct TransformPositionTranslator : ITransformTweenTranslator<float3>
    {
        [BurstCompile] public readonly float3 GetValue(ref TransformAccess transformAccess) => transformAccess.position;
        public readonly float3 GetValueManaged(Transform transform) => transform.position;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in float3 value) => transformAccess.position = value;
        public readonly void ApplyManaged(Transform transform, in float3 value) => transform.position = value;
    }

    [BurstCompile]
    internal struct TransformPositionXTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.position.x;
        public readonly float GetValueManaged(Transform transform) => transform.position.x;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.position;
            p.x = value;
            transformAccess.position = p;
        }
        
        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.position;
            p.x = value;
            transform.position = p;
        }
    }

    [BurstCompile]
    internal struct TransformPositionYTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.position.y;
        public readonly float GetValueManaged(Transform transform) => transform.position.y;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.position;
            p.y = value;
            transformAccess.position = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.position;
            p.y = value;
            transform.position = p;
        }
    }

    [BurstCompile]
    internal struct TransformPositionZTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.position.z;
        public readonly float GetValueManaged(Transform transform) => transform.position.z;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.position;
            p.z = value;
            transformAccess.position = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.position;
            p.z = value;
            transform.position = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalPositionTranslator : ITransformTweenTranslator<float3>
    {
        [BurstCompile] public readonly float3 GetValue(ref TransformAccess transformAccess) => transformAccess.localPosition;
        public readonly float3 GetValueManaged(Transform transform) => transform.localPosition;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in float3 value) => transformAccess.localPosition = value;
        public readonly void ApplyManaged(Transform transform, in float3 value) => transform.localPosition = value;
    }

    [BurstCompile]
    internal struct TransformLocalPositionXTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localPosition.x;
        public readonly float GetValueManaged(Transform transform) => transform.localPosition.x;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localPosition;
            p.x = value;
            transformAccess.localPosition = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localPosition;
            p.x = value;
            transform.localPosition = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalPositionYTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localPosition.y;
        public readonly float GetValueManaged(Transform transform) => transform.localPosition.y;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localPosition;
            p.y = value;
            transformAccess.localPosition = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localPosition;
            p.y = value;
            transform.localPosition = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalPositionZTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localPosition.z;
        public readonly float GetValueManaged(Transform transform) => transform.localPosition.z;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localPosition;
            p.z = value;
            transformAccess.localPosition = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localPosition;
            p.z = value;
            transform.localPosition = p;
        }
    }

    [BurstCompile]
    internal struct TransformRotationTranslator : ITransformTweenTranslator<quaternion>
    {
        [BurstCompile] public readonly quaternion GetValue(ref TransformAccess transformAccess) => transformAccess.rotation;
        public readonly quaternion GetValueManaged(Transform transform) => transform.rotation;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in quaternion value) => transformAccess.rotation = value;
        public readonly void ApplyManaged(Transform transform, in quaternion value) => transform.rotation = value;
    }

    [BurstCompile]
    internal struct TransformLocalRotationTranslator : ITransformTweenTranslator<quaternion>
    {
        [BurstCompile] public readonly quaternion GetValue(ref TransformAccess transformAccess) => transformAccess.localRotation;
        public readonly quaternion GetValueManaged(Transform transform) => transform.localRotation;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in quaternion value) => transformAccess.localRotation = value;
        public readonly void ApplyManaged(Transform transform, in quaternion value) => transform.localRotation = value;
    }

    [BurstCompile]
    internal struct TransformEulerAnglesTranslator : ITransformTweenTranslator<float3>
    {
        [BurstCompile] public readonly float3 GetValue(ref TransformAccess transformAccess) => transformAccess.rotation.eulerAngles;
        public readonly float3 GetValueManaged(Transform transform) => transform.eulerAngles;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in float3 value) => transformAccess.rotation = Quaternion.Euler(value);
        public readonly void ApplyManaged(Transform transform, in float3 value) => transform.eulerAngles = value;
    }

    [BurstCompile]
    internal struct TransformEulerAnglesXTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.rotation.eulerAngles.x;
        public readonly float GetValueManaged(Transform transform) => transform.eulerAngles.x;

        [BurstCompile] 
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.rotation.eulerAngles;
            p.x = value;
            transformAccess.rotation = Quaternion.Euler(p);
        }
        
        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.eulerAngles;
            p.x = value;
            transform.eulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformEulerAnglesYTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.rotation.eulerAngles.y;
        public readonly float GetValueManaged(Transform transform) => transform.eulerAngles.y;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.rotation.eulerAngles;
            p.y = value;
            transformAccess.rotation = Quaternion.Euler(p);
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.eulerAngles;
            p.y = value;
            transform.eulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformEulerAnglesZTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.rotation.eulerAngles.z;
        public readonly float GetValueManaged(Transform transform) => transform.eulerAngles.z;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.rotation.eulerAngles;
            p.z = value;
            transformAccess.rotation = Quaternion.Euler(p);
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.eulerAngles;
            p.z = value;
            transform.eulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalEulerAnglesTranslator : ITransformTweenTranslator<float3>
    {
        [BurstCompile] public readonly float3 GetValue(ref TransformAccess transformAccess) => transformAccess.localRotation.eulerAngles;
        public readonly float3 GetValueManaged(Transform transform) => transform.localEulerAngles;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in float3 value) => transformAccess.localRotation = Quaternion.Euler(value);
        public readonly void ApplyManaged(Transform transform, in float3 value) => transform.localEulerAngles = value;
    }

    [BurstCompile]
    internal struct TransformLocalEulerAnglesXTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localRotation.eulerAngles.x;
        public readonly float GetValueManaged(Transform transform) => transform.localEulerAngles.x;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localRotation.eulerAngles;
            p.x = value;
            transformAccess.localRotation = Quaternion.Euler(p);
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localEulerAngles;
            p.x = value;
            transform.localEulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalEulerAnglesYTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localRotation.eulerAngles.y;
        public readonly float GetValueManaged(Transform transform) => transform.localEulerAngles.y;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localRotation.eulerAngles;
            p.y = value;
            transformAccess.localRotation = Quaternion.Euler(p);
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localEulerAngles;
            p.y = value;
            transform.localEulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalEulerAnglesZTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localRotation.eulerAngles.z;
        public readonly float GetValueManaged(Transform transform) => transform.localEulerAngles.z;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localRotation.eulerAngles;
            p.z = value;
            transformAccess.localRotation = Quaternion.Euler(p);
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localEulerAngles;
            p.z = value;
            transform.localEulerAngles = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalScaleTranslator : ITransformTweenTranslator<float3>
    {
        [BurstCompile] public readonly float3 GetValue(ref TransformAccess transformAccess) => transformAccess.localScale;
        public readonly float3 GetValueManaged(Transform transform) => transform.localScale;
        [BurstCompile] public readonly void Apply(ref TransformAccess transformAccess, in float3 value) => transformAccess.localScale = value;
        public readonly void ApplyManaged(Transform transform, in float3 value) => transform.localScale = value;
    }

    [BurstCompile]
    internal struct TransformLocalScaleXTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localScale.x;
        public readonly float GetValueManaged(Transform transform) => transform.localScale.x;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localScale;
            p.x = value;
            transformAccess.localScale = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localScale;
            p.x = value;
            transform.localScale = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalScaleYTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localScale.y;
        public readonly float GetValueManaged(Transform transform) => transform.localScale.y;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localScale;
            p.y = value;
            transformAccess.localScale = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localScale;
            p.y = value;
            transform.localScale = p;
        }
    }

    [BurstCompile]
    internal struct TransformLocalScaleZTranslator : ITransformTweenTranslator<float>
    {
        [BurstCompile] public readonly float GetValue(ref TransformAccess transformAccess) => transformAccess.localScale.z;
        public readonly float GetValueManaged(Transform transform) => transform.localScale.z;

        [BurstCompile]
        public readonly void Apply(ref TransformAccess transformAccess, in float value)
        {
            var p = transformAccess.localScale;
            p.z = value;
            transformAccess.localScale = p;
        }

        public readonly void ApplyManaged(Transform transform, in float value)
        {
            var p = transform.localScale;
            p.z = value;
            transform.localScale = p;
        }
    }
}