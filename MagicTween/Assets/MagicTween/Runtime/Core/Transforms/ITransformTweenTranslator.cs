using Unity.Entities;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms
{
    public interface ITransformTweenTranslator<TValue> : IComponentData where TValue : unmanaged
    {
        TValue GetValue(ref TransformAccess transformAccess);
        TValue GetValueManaged(Transform transform);
        void Apply(ref TransformAccess transformAccess, in TValue value);
        void ApplyManaged(Transform transform, in TValue value);
    }
}