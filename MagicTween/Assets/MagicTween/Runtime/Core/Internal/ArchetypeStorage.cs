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
        readonly struct LambdaTweenTypeKey<TValue, TOptions> { }
        readonly struct UnsafeLambdaTweenTypeKey<TValue, TOptions> { }
        readonly struct VibrationTweenTypeKey<TValue, TOptions> { }
        readonly struct UnsafeVibrationTweenTypeKey<TValue, TOptions> { }
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
            };

            archetypeStorage.coreComponentTypes = list.AsArray();
        }

        public void Dispose()
        {
            cache.Dispose();
            coreComponentTypes.Dispose();
        }

        [BurstCompile]
        public EntityArchetype GetLambdaTweenArchetype<TValue, TOptions>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var index = SharedTypeIndex<LambdaTweenTypeKey<TValue, TOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessor<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetNoAllocLambdaTweenArchetype<TValue, TOptions>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var index = SharedTypeIndex<UnsafeLambdaTweenTypeKey<TValue, TOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenEndValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<TOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessorNoAlloc<TValue>>()
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
            var index = SharedTypeIndex<LambdaTweenTypeKey<string, StringTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenStartValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenEndValue<UnsafeText>>(),
                    ComponentType.ReadWrite<TweenOptions<StringTweenOptions>>(),
                    ComponentType.ReadWrite<StringTweenCustomScrambleChars>(),
                    ComponentType.ReadWrite<TweenPropertyAccessor<string>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetPunchLambdaTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<VibrationTweenTypeKey<TValue, PunchTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessor<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetNoAllocPunchLambdaTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<UnsafeVibrationTweenTypeKey<TValue, PunchTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<PunchTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessorNoAlloc<TValue>>(),
                    ComponentType.ReadWrite<VibrationStrength<TValue>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetShakeLambdaTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<VibrationTweenTypeKey<TValue, ShakeTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessor<TValue>>(),
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
        public EntityArchetype GetNoAllocShakeLambdaTweenArchetype<TValue>(ref EntityManager entityManager)
            where TValue : unmanaged
        {
            var index = SharedTypeIndex<UnsafeVibrationTweenTypeKey<TValue, ShakeTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<TValue>>(),
                    ComponentType.ReadWrite<TweenStartValue<TValue>>(),
                    ComponentType.ReadWrite<TweenOptions<ShakeTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessorNoAlloc<TValue>>(),
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
        public EntityArchetype GetPathLambdaTweenArchetype(ref EntityManager entityManager)
        {
            var index = SharedTypeIndex<LambdaTweenTypeKey<float3, PathTweenOptions>>.Data;
            if (!cache.TryGetValue(index, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    ComponentType.ReadWrite<TweenValue<float3>>(),
                    ComponentType.ReadWrite<PathPoint>(),
                    ComponentType.ReadWrite<TweenOptions<PathTweenOptions>>(),
                    ComponentType.ReadWrite<TweenPropertyAccessor<float3>>()
                };
                types.AddRange(coreComponentTypes);
                archetype = entityManager.CreateArchetype(types.AsArray());
                cache.Add(index, archetype);
            }
            return archetype;
        }

        [BurstCompile]
        public EntityArchetype GetEntityTweenArchetype<TValue, TOptions, TTranslator>(ref EntityManager entityManager)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
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
                    ComponentType.ReadWrite<TTranslator>(),
                    ComponentType.ReadWrite<TweenTranslationOptionsData>()
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