using System;
using MagicTween.Core;
using Unity.Entities;
using UnityEngine;

namespace MagicTween
{
    public struct TweenTargetEntity : IComponentData
    {
        public Entity target;
    }

    public sealed class TweenTargetObject : IComponentData, IDisposable
    {
        public object target;

        public void Dispose()
        {
            TweenTargetObjectPool.Return(this);
        }
    }
}