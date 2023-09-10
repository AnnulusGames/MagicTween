using UnityEntity = Unity.Entities.Entity;
using MagicTween.Core;

namespace MagicTween
{
    public readonly partial struct Tween : ITweenHandle
    {
        public Tween(in UnityEntity entity)
        {
            this.entity = entity;
        }

        readonly UnityEntity entity;

        public UnityEntity GetEntity() => entity;
        public Tween AsUnitTween() => this;
    }

    public readonly partial struct Tween<TValue, TOptions> : ITweenHandle
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
    {
        public Tween(in UnityEntity entity)
        {
            this.entity = entity;
        }

        readonly UnityEntity entity;

        public UnityEntity GetEntity() => entity;
        public Tween AsUnitTween() => new(entity);

        public static implicit operator Tween(Tween<TValue, TOptions> tween)
        {
            return tween.AsUnitTween();
        }
    }
}