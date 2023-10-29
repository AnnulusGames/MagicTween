using System;
using Unity.Entities;

namespace MagicTween.Core.Components
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