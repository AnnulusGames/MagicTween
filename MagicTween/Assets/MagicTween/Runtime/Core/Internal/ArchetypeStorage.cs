using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    [BurstCompile]
    internal struct ArchetypeStorage : IDisposable
    {
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
        public NativeArray<ComponentType> coreComponentTypes;

        public bool IsCreated => cache.IsCreated && coreComponentTypes.IsCreated;

        [BurstCompile]
        public readonly void AddCoreComponentTypes(ref NativeList<ComponentType> list)
        {
            list.AddRange(coreComponentTypes);
        }

        [BurstCompile]
        public void Register<TKey>(ref EntityArchetype archetype)
        {
            var index = SharedTypeIndex<TKey>.Data;
            cache.TryAdd(index, archetype);
        }

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
                ComponentType.ReadWrite<TweenRootFlag>()
            };

            archetypeStorage.coreComponentTypes = list.AsArray();
        }

        public void Dispose()
        {
            cache.Dispose();
            coreComponentTypes.Dispose();
        }

        [BurstCompile]
        public bool TryGet<TKey>(out EntityArchetype archetype)
        {
            var index = SharedTypeIndex<TKey>.Data;
            return cache.TryGetValue(index, out archetype);
        }
    }
}