using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TweenMaterialTargetAuthoring : MonoBehaviour
{
    public Color toColor;
    public float duration;

    class Baker : Baker<TweenMaterialTargetAuthoring>
    {
        public override void Bake(TweenMaterialTargetAuthoring authoring)
        {
            var color = new float4(authoring.toColor.r, authoring.toColor.g, authoring.toColor.b, authoring.toColor.a);
            
            var data = new TweenMaterialTarget()
            {
                toColor = color,
                duration = authoring.duration
            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}
