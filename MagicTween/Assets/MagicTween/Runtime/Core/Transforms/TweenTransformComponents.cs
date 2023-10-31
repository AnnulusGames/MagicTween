#if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core.Transforms
{
    public sealed class TweenTargetTransform : IComponentData, IDisposable
    {
        public Transform target;
        public int instanceId;
        public bool isRegistered;

        public void Dispose()
        {
            TransformManager.Unregister(this);
            TweenTargetTransformPool.Return(this);
        }
    }
}
#endif