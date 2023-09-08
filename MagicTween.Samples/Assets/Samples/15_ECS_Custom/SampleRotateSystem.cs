using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[RequireMatchingQueriesForUpdate]
[BurstCompile]
public partial struct SampleRotateSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // In order to visualize changes in values, I created a System that rotates objects according to component values.
        foreach (var (transform, component) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SampleComponentData>>())
        {
            // The rotation speed changes depending on the value of SampleComponentData.
            transform.ValueRW = transform.ValueRO.RotateY(component.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }
}
