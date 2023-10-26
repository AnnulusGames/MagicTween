using Unity.Entities;
using Unity.Transforms;
using MagicTween;
using MagicTween.Translators;

[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct ECSTransformSampleSystem : ISystem
{
    // Note: MagicTween's API is currently only available on the main thread.
    //       You cannot create tweens or control tweens with Play(), Complete(), etc. within a job.

    public void OnUpdate(ref SystemState state)
    {
        // Enumerate Entities with 'TweenTarget' and 'LocalTransform' attached and create tweens.
        foreach (var (transform, targetInfo, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<TweenTarget>>().WithEntityAccess())
        {
            // You can use Tween.Entity.To() to tween the value of the component linked to the Entity.

            // For the type argument, specify a Translator to reflect the tween value in the component.
            // Below, a translator corresponding to the position of the LocalTransform component is passed.
            Tween.Entity.To<PositionTranslator>(entity, targetInfo.ValueRO.toPosition, targetInfo.ValueRO.duration)
                .SetEase(Ease.OutQuad);

            Tween.Entity.To<EulerAnglesTranslator>(entity, targetInfo.ValueRO.toRotation, targetInfo.ValueRO.duration)
                .SetEase(Ease.OutQuad);
        }

        state.Enabled = false;
    }
}
