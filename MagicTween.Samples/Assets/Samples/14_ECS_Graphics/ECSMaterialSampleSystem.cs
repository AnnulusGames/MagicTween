using Unity.Entities;
using Unity.Rendering;
using Unity.Burst;
using MagicTween;
using MagicTween.Translators;

[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(InitializationSystemGroup))]
[BurstCompile]
public partial struct ECSMaterialSampleSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (baseColor, targetInfo, entity) in SystemAPI.Query<RefRW<URPMaterialPropertyBaseColor>, RefRO<TweenMaterialTarget>>().WithEntityAccess())
        {
            // Tween the 'BaseColor' property of the URP's Material.
            Tween.Entity.To<URPMaterialPropertyBaseColor, URPBaseColorTranslator>(entity, targetInfo.ValueRO.toColor, targetInfo.ValueRO.duration)
                .SetEase(Ease.OutQuad);
        }

        state.Enabled = false;
    }
}
