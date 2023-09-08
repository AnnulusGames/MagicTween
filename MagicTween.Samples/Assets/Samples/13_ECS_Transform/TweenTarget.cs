using Unity.Entities;
using Unity.Mathematics;

public struct TweenTarget : IComponentData
{
    public float3 toPosition;
    public float3 toRotation;
    public float duration;
}
