using Unity.Entities;
using UnityEngine;

public class SampleComponentAuthoring : MonoBehaviour
{
    public float value;

    class Baker : Baker<SampleComponentAuthoring>
    {
        public override void Bake(SampleComponentAuthoring authoring)
        {
            var data = new SampleComponentData()
            {
                value = authoring.value
            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}
