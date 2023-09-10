using System;
using Unity.Entities;

namespace MagicTween.Core.Components
{
    public struct SequenceState : IComponentData
    {
        public float lastTweenDuration;
    }

    [InternalBufferCapacity(10)]
    public struct SequenceEntitiesGroup : IBufferElementData, IComparable<SequenceEntitiesGroup>
    {
        public SequenceEntitiesGroup(Entity entity, float position)
        {
            this.entity = entity;
            this.position = position;
        }

        public Entity entity;
        public float position;

        public int CompareTo(SequenceEntitiesGroup other)
        {
            if (other.position == position) return 0;
            else if (other.position > position) return -1;
            else return 1;
        }
    }
}