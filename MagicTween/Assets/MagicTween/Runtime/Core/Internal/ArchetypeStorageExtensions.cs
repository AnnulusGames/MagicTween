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
        readonly struct DelegateTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateNoAllocTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateVibrationTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateVibrationNoAllocTweenTypeKey<TValue, TOptions> { }
        readonly struct EntityTweenTypeKey<TValue, TOptions, TTranslator> { }
        readonly struct ObjectTweenTypeKey<TValue, TOptions, TTranslator> { }
        readonly struct UnitTweenTypeKey { }
        readonly struct SequenceTypeKey { }

        [BurstCompile]
        public static EntityArchetype GetDelegateTweenArchetype<TValue, TOptions>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            if (!storage.TryGet<DelegateTweenTypeKey<TValue, TOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<TValue, TOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateNoAllocTweenArchetype<TValue, TOptions>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            if (!storage.TryGet<DelegateNoAllocTweenTypeKey<TValue, TOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateNoAllocTweenTypeKey<TValue, TOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetStringLambdaTweenArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<DelegateTweenTypeKey<string, StringTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenStartValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenEndValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenOptions<StringTweenOptions>>(),
                    ComponentType.ReadWrite<StringTweenCustomScrambleChars>(),
                    ComponentType.ReadWrite<TweenDelegates<string>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<string, StringTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePunchTweenArchetype<TValue>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
        {
            if (!storage.TryGet<DelegateVibrationTweenTypeKey<TValue, PunchTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationTweenTypeKey<TValue, PunchTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePunchNoAllocTweenArchetype<TValue>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
        {
            if (!storage.TryGet<DelegateVibrationNoAllocTweenTypeKey<TValue, PunchTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationNoAllocTweenTypeKey<TValue, PunchTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateShakeTweenArchetype<TValue>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
        {
            if (!storage.TryGet<DelegateVibrationTweenTypeKey<TValue, ShakeTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationTweenTypeKey<TValue, ShakeTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegateShakeNoAllocTweenArchetype<TValue>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
        {
            if (!storage.TryGet<DelegateVibrationNoAllocTweenTypeKey<TValue, ShakeTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateVibrationNoAllocTweenTypeKey<TValue, ShakeTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetDelegatePathTweenArchetype(ref this ArchetypeStorage storage, ref EntityManager entityManager)
        {
            if (!storage.TryGet<DelegateTweenTypeKey<float3, PathTweenOptions>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<float3>>(),
                    ComponentType.ReadWrite<PathPoint>(),
                    ComponentType.ReadWrite<TweenOptions<PathTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<float3>>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<DelegateTweenTypeKey<float3, PathTweenOptions>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetEntityTweenArchetype<TValue, TOptions, TComponent, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TComponent : unmanaged, IComponentData
            where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
        {
            if (!storage.TryGet<EntityTweenTypeKey<TValue, TOptions, TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenTargetEntity>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<EntityTweenTypeKey<TValue, TOptions, TTranslator>>(ref archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public static EntityArchetype GetObjectTweenArchetype<TValue, TOptions, TObject, TTranslator>(ref this ArchetypeStorage storage, ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TObject : class
            where TTranslator : unmanaged, ITweenTranslatorManaged<TValue, TObject>
        {
            if (!storage.TryGet<ObjectTweenTypeKey<TValue, TOptions, TTranslator>>(out var archetype))
            {
                var types = new NativeList<ComponentType>(32, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenTargetObject>(),
                    ComponentType.ReadWrite<TweenTranslationModeData>(),
                    ComponentType.ReadWrite<TTranslator>()
                };
                storage.AddCoreComponentTypes(ref types);
                archetype = entityManager.CreateArchetype(types.AsArray());
                storage.Register<ObjectTweenTypeKey<TValue, TOptions, TTranslator>>(ref archetype);
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