using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using MagicTween.Core.Components;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MagicTween.Core
{
    [BurstCompile]
    internal struct ArchetypeStorage : IDisposable
    {
        readonly struct DelegateTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateNoAllocTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateVibrationTweenTypeKey<TValue, TOptions> { }
        readonly struct DelegateVibrationNoAllocTweenTypeKey<TValue, TOptions> { }
        readonly struct EntityTweenTypeKey<TValue, TOptions, TTranslator> { }
        readonly struct UnitTweenTypeKey { }
        readonly struct SequenceTypeKey { }

        [BurstCompile]
        readonly struct SharedTypeIndex<T>
        {
            static readonly SharedStatic<int> _index = SharedStatic<int>.GetOrCreate<T>();
            static readonly SharedStatic<bool> isCreated = SharedStatic<bool>.GetOrCreate<SharedTypeIndex<T>>();

            public static int Data
            {
                [BurstCompile]
                get
                {
                    if (!isCreated.Data)
                    {
                        _index.Data = _sharedTypeIndexOffset.Data;
                        _sharedTypeIndexOffset.Data++;
                        isCreated.Data = true;
                    }

                    return _index.Data;
                }
            }
        }

        static readonly SharedStatic<int> _sharedTypeIndexOffset = SharedStatic<int>.GetOrCreate<ArchetypeStorage>();

        NativeHashMap<int, EntityArchetype> cache;
        NativeArray<ComponentType> coreComponentTypes;

        public bool IsCreated => cache.IsCreated && coreComponentTypes.IsCreated;

        [BurstCompile]
        public static void Create(Allocator allocator, out ArchetypeStorage archetypeStorage)
        {
            archetypeStorage = new ArchetypeStorage
            {
                cache = new(32, allocator)
            };

            var list = new NativeList<ComponentType>(allocator)
            {
                ComponentType.ReadWrite<TweenStatus>(),
                ComponentType.ReadWrite<TweenPosition>(),
                ComponentType.ReadWrite<TweenCompletedLoops>(),
                ComponentType.ReadWrite<TweenProgress>(),

                ComponentType.ReadWrite<TweenParameterDuration>(),
                ComponentType.ReadWrite<TweenParameterDelay>(),
                ComponentType.ReadWrite<TweenParameterLoops>(),
                ComponentType.ReadWrite<TweenParameterLoopType>(),
                ComponentType.ReadWrite<TweenParameterPlaybackSpeed>(),
                ComponentType.ReadWrite<TweenParameterEase>(),
                ComponentType.ReadWrite<TweenParameterCustomEasingCurve>(),
                ComponentType.ReadWrite<TweenParameterAutoPlay>(),
                ComponentType.ReadWrite<TweenParameterAutoKill>(),
                ComponentType.ReadWrite<TweenParameterIgnoreTimeScale>(),
                ComponentType.ReadWrite<TweenParameterIsRelative>(),
                ComponentType.ReadWrite<TweenParameterInvertMode>(),

                ComponentType.ReadWrite<TweenIdInt>(),
                ComponentType.ReadWrite<TweenIdString>(),

                ComponentType.ReadWrite<TweenInvertFlag>(),
                ComponentType.ReadWrite<TweenStartedFlag>(),
                ComponentType.ReadWrite<TweenCallbackFlags>(),
                ComponentType.ReadWrite<TweenAccessorFlags>(),

                ComponentType.ReadWrite<TweenControllerReference>(),
                ComponentType.ReadWrite<TweenRootFlag>(),
                ComponentType.ReadWrite<TweenTranslationMode>()
            };

            archetypeStorage.coreComponentTypes = list.AsArray();
        }

        public void Dispose()
        {
            cache.Dispose();
            coreComponentTypes.Dispose();
        }

        [BurstCompile]
        public EntityArchetype GetDelegateTweenArchetype<TValue, TOptions>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var index = SharedTypeIndex<DelegateTweenTypeKey<TValue, TOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegateNoAllocTweenArchetype<TValue, TOptions>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var index = SharedTypeIndex<DelegateNoAllocTweenTypeKey<TValue, TOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetStringLambdaTweenArchetype(ref EntityManager entityManager)
        {
            var index = SharedTypeIndex<DelegateTweenTypeKey<string, StringTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenStartValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenEndValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenOptions<StringTweenOptions>>(),
                    ComponentType.ReadWrite<StringTweenCustomScrambleChars>(),
                    ComponentType.ReadWrite<TweenDelegates<string>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegatePunchTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<DelegateVibrationTweenTypeKey<TValue, PunchTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegatePunchNoAllocTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<DelegateVibrationNoAllocTweenTypeKey<TValue, PunchTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegateShakeTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<DelegateVibrationTweenTypeKey<TValue, ShakeTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegateShakeNoAllocTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<DelegateVibrationNoAllocTweenTypeKey<TValue, ShakeTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegatesNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>(),
                    ComponentType.ReadWrite<ShakeRandomState>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetDelegatePathTweenArchetype(ref EntityManager entityManager)
        {
            var index = SharedTypeIndex<DelegateTweenTypeKey<float3, PathTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<float3>>(),
                    ComponentType.ReadWrite<PathPoint>(),
                    ComponentType.ReadWrite<TweenOptions<PathTweenOptions>>(),
                    ComponentType.ReadWrite<TweenDelegates<float3>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetEntityTweenArchetype<TValue, TOptions, TComponent, TTranslator>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TComponent : unmanaged, IComponentData
            where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
        {
            var index = SharedTypeIndex<EntityTweenTypeKey<TValue, TOptions, TTranslator>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenTargetEntity>(),
                    ComponentType.ReadOnly<TTranslator>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetUnitTweenArchetype(ref EntityManager entityManager)
        {
            var index = SharedTypeIndex<UnitTweenTypeKey>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                archetype = entityManager.CreateArchetype(coreComponentTypes);
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetSequenceArchetype(ref EntityManager entityManager)
        {
            var index = SharedTypeIndex<SequenceTypeKey>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<SequenceState>(),
                    ComponentType.ReadWrite<SequenceEntitiesGroup>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }
    }
}