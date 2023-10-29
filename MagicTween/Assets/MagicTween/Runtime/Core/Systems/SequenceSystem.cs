using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core.Systems
{
    [UpdateInGroup(typeof(MagicTweenCoreSystemGroup))]
    [UpdateAfter(typeof(TweenSystem))]
    [BurstCompile]
    public partial class SequenceSystem : SystemBase
    {
        TweenCleanupSystem cleanupSystem;
        EntityQuery query;
        TweenAspect.Lookup tweenAspectLookUp;
        BufferLookup<SequenceEntitiesGroup> bufferLookup;
        EntityStorageInfoLookup storageInfoLookup;

        [BurstCompile]
        protected override void OnCreate()
        {
            cleanupSystem = World.GetExistingSystemManaged<TweenCleanupSystem>();
            query = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<SequenceEntitiesGroup, TweenRootFlag>()
                .Build();
            tweenAspectLookUp = new TweenAspect.Lookup(ref CheckedStateRef);
            bufferLookup = GetBufferLookup<SequenceEntitiesGroup>();
            storageInfoLookup = GetEntityStorageInfoLookup();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            tweenAspectLookUp.Update(ref CheckedStateRef);
            bufferLookup.Update(this);
            storageInfoLookup.Update(this);
            var job = new SystemJob()
            {
                tweenAspectLookUp = tweenAspectLookUp,
                bufferLookup = bufferLookup,
                parallelWriter = cleanupSystem.CreateBuffer(),
                storageInfoLookup = storageInfoLookup
            };
            job.ScheduleParallel(query);
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            [NativeDisableContainerSafetyRestriction] public TweenAspect.Lookup tweenAspectLookUp;
            [NativeDisableContainerSafetyRestriction] public BufferLookup<SequenceEntitiesGroup> bufferLookup;
            [WriteOnly] public NativeQueue<Entity>.ParallelWriter parallelWriter;
            [ReadOnly] public EntityStorageInfoLookup storageInfoLookup;

            public void Execute(TweenAspect aspect, ref DynamicBuffer<SequenceEntitiesGroup> buffer)
            {
                Update(aspect.status, ref aspect, ref buffer);
            }

            [BurstCompile]
            void Update(TweenStatusType rootStatus, ref TweenAspect aspect, ref DynamicBuffer<SequenceEntitiesGroup> sequenceEntitiesGroup)
            {
                for (int i = 0; i < sequenceEntitiesGroup.Length; i++)
                {
                    var item = sequenceEntitiesGroup[i];
                    var childEntity = item.entity;
                    if (!storageInfoLookup.Exists(childEntity)) continue;

                    var childSequencePosition = item.position;
                    var childAspect = tweenAspectLookUp[childEntity];

                    switch (rootStatus)
                    {
                        case TweenStatusType.Killed:
                            childAspect.Kill(ref parallelWriter);
                            break;
                        default:
                            childAspect.Update(aspect.delay + aspect.duration * aspect.progress - childSequencePosition, ref parallelWriter);
                            break;
                    }
                    
                    if (bufferLookup.TryGetBuffer(childEntity, out var childBuffer))
                    {
                        Update(rootStatus, ref childAspect, ref childBuffer);
                    }
                }
            }
        }
    }
}