using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;
using MagicTween.Plugins;

namespace MagicTween.Core
{
    [BurstCompile]
    internal static class ArchetypeStorageExtensions
    {
        readonly struct DelegateTweenTypeKey<TValue, TOptions, TPlugin> { }
        readonly struct DelegateNoAllocTweenTypeKey<TValue, TOptions, TPlugin> { }
        readonly struct DelegateVibrationTweenTypeKey<TValue, TOptions, TPlugin> { }
        readonly struct DelegateVibrationNoAllocTweenTypeKey<TValue, TOptions, TPlugin> { }
        readonly struct EntityTweenTypeKey<TValue, TOptions, TPlugin, TTranslator> { }
        readonly struct ObjectTweenTypeKey<TValue, TOptions, TPlugin, TTranslator> { }
        readonly struct UnitTweenTypeKey { }
        readonly struct SequenceTypeKey { }

        [BurstCompile]
        public static EntityArchetype GetDelegateTweenArchetype<TValue, TOptions, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        {
            if (!storage.TryGet<DelegateTweenTypeKey<TValue, TOptions, TPlugin>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<TValue, TOptions, TPlugin>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateNoAllocTweenArchetype<TValue, TOptions, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        {
            if (!storage.TryGet<DelegateNoAllocTweenTypeKey<TValue, TOptions, TPlugin>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateNoAllocTweenTypeKey<TValue, TOptions, TPlugin>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetStringLambdaTweenArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<DelegateTweenTypeKey<string, StringTweenOptions, StringTweenPlugin>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenStartValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenEndValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenOptions<StringTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<StringTweenPlugin>>(),
                    ComponentType.ReadWrite<StringTweenCustomScrambleChars>(),
                    ComponentType.ReadWrite<TweenDelegates<string>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<string, StringTweenOptions, StringTweenPlugin>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePunchTweenArchetype<TValue, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, PunchTweenOptions>
        {
            if (!storage.TryGet<DelegateVibrationTweenTypeKey<TValue, PunchTweenOptions, TPlugin>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationTweenTypeKey<TValue, PunchTweenOptions, TPlugin>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePunchNoAllocTweenArchetype<TValue, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, PunchTweenOptions>
        {
            if (!storage.TryGet<DelegateVibrationNoAllocTweenTypeKey<TValue, TPlugin, PunchTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationNoAllocTweenTypeKey<TValue, TPlugin, PunchTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateShakeTweenArchetype<TValue, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, ShakeTweenOptions>
        {
            if (!storage.TryGet<DelegateVibrationTweenTypeKey<TValue, TPlugin, ShakeTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationTweenTypeKey<TValue, TPlugin, ShakeTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateShakeNoAllocTweenArchetype<TValue, TPlugin>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue, ShakeTweenOptions>
        {
            if (!storage.TryGet<DelegateVibrationNoAllocTweenTypeKey<TValue, TPlugin, ShakeTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationNoAllocTweenTypeKey<TValue, TPlugin, ShakeTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePathTweenArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<DelegateTweenTypeKey<float3, PathTweenOptions, PathTweenPlugin>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<float3>>(),
                    ComponentType.ReadWrite<PathPoint>(),
                    ComponentType.ReadWrite<TweenOptions<PathTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<PathTweenPlugin>>(),
                    ComponentType.ReadWrite<TweenDelegates<float3>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<float3, PathTweenOptions, PathTweenPlugin>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetEntityTweenArchetype<TValue, TOptions, TPlugin, TComponent, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
            where TComponent : unmanaged, IComponentData
            where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
        {
            if (!storage.TryGet<EntityTweenTypeKey<TValue, TOptions, TPlugin, TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPluginTag<TPlugin>>(),
                    ComponentType.ReadWrite<TweenTargetEntity>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<EntityTweenTypeKey<TValue, TOptions, TPlugin, TTranslator>>(ref archetype);
            }
            return archetype;
        }
        
        [BurstCompile]
        public static EntityArchetype GetUnitTweenArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<UnitTweenTypeKey>(out var archetype))
            {
                archetype = entityManager.CreateArchetype(storage.coreComponentTypes);
                storage.Register<UnitTweenTypeKey>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetSequenceArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<SequenceTypeKey>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<SequenceState>(),
                    ComponentType.ReadWrite<SequenceEntitiesGroup>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<SequenceTypeKey>(ref archetype);
            }
            return archetype;
        }
    }
}