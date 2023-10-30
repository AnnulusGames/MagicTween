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
            if (isRegistered) TransformManager.Unregister(this);
            TweenTargetTransformPool.Return(this);
        }
    }
}