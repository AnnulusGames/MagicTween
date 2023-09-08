using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TweenTargetAuthoring : MonoBehaviour
{
    public float3 toPosition;
    public float3 toRotation;
    public float duration;

    class Baker : Baker<TweenTargetAuthoring>
    {
        public override void Bake(TweenTargetAuthoring authoring)
        {
            var data = new TweenTarget()
            {
                toPosition = authoring.toPosition,
                toRotation = authoring.toRotation,
                duration = authoring.duration
            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}
