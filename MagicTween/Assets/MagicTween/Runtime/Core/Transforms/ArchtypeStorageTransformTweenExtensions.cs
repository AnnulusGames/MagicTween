#if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using MagicTween.Core.Components;
using MagicTween.Plugins;

namespace MagicTween.Core.Transforms
{
    internal static class ArchtypeStorageTransformTweenExtensions
    {
        readonly struct TransformTweenTypeKey<TTranslator> { }
        readonly struct TransformShakeTweenTypeKey<TTranslator> { }
        readonly struct TransformPunchTweenTypeKey<TTranslator> { }

        [BurstCompile]
        public static EntityArchetype GetTransformTweenArchetype<TValue, TOptions, TPlugin, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
            where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
        {
            if (!storage.TryGet<TransformTweenTypeKey<TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenTargetTransform>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<TransformTweenTypeKey<TTranslator>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetTransformPunchTweenArchetype<TValue, TPlugin, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, PunchTweenOptions>
            where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
        {
            if (!storage.TryGet<TransformPunchTweenTypeKey<TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<TweenTargetTransform>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<TransformPunchTweenTypeKey<TTranslator>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetTransformShakeTweenArchetype<TValue, TPlugin, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, ShakeTweenOptions>
            where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
        {
            if (!storage.TryGet<TransformShakeTweenTypeKey<TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>(),
                    ComponentType.ReadWrite<TweenTargetTransform>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<TransformShakeTweenTypeKey<TTranslator>>(ref archetype);
            }
            return archetype;
        }
    }
}
#endif