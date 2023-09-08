using Unity.Entities;
using Unity.Mathematics;

public struct TweenMaterialTarget : IComponentData
{
    public float4 toColor;
    public float duration;
}