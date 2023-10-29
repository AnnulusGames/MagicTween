using Unity.Entities;
using MagicTween;

[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct ECSCustomTweenSampleSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (component, entity) in SystemAPI.Query<RefRO<SampleComponentData>>().WithEntityAccess())
        {
            // You can tween your own ComponentData values by passing a custom Translator as a type argument.
            Tween.Entity.FromTo<SampleComponentData, SampleTranslator>(entity, 1f, 10f, 4f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }

        state.Enabled = false;
    }
}
