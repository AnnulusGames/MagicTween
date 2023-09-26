using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Collections;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween
{
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public abstract partial class TweenTranslationSystemBase<TValue, TComponent, TTranslator> : SystemBase
        where TValue : unmanaged
        where TComponent : unmanaged, IComponentData
        where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
    {
        ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
        ComponentTypeHandle<TTranslator> translatorTypeHandle;
        ComponentTypeHandle<TweenTranslationOptionsData> optionsTypeHandle;
        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        ComponentLookup<TComponent> targetComponentLookup;
        EntityTypeHandle entityTypeHandle;
        EntityStorageInfoLookup entityLookup;

        EntityQuery query;
        EndSimulationEntityCommandBufferSystem commandBufferSystem;

        protected override void OnCreate()
        {
            startValueTypeHandle = GetComponentTypeHandle<TweenStartValue<TValue>>();
            valueTypeHandle = GetComponentTypeHandle<TweenValue<TValue>>(true);
            translatorTypeHandle = GetComponentTypeHandle<TTranslator>();
            optionsTypeHandle = GetComponentTypeHandle<TweenTranslationOptionsData>(true);
            accessorFlagsTypeHandle = GetComponentTypeHandle<TweenAccessorFlags>(true);
            targetComponentLookup = GetComponentLookup<TComponent>();
            entityTypeHandle = GetEntityTypeHandle();
            entityLookup = GetEntityStorageInfoLookup();

            query = GetEntityQuery(
                typeof(TTranslator),
                typeof(TweenStartValue<TValue>),
                typeof(TweenValue<TValue>),
                typeof(TweenAccessorFlags),
                typeof(TweenTranslationOptionsData)
            );
            commandBufferSystem = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            CompleteDependency();

            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            translatorTypeHandle.Update(this);
            optionsTypeHandle.Update(this);
            accessorFlagsTypeHandle.Update(this);
            targetComponentLookup.Update(this);
            entityTypeHandle.Update(this);
            entityLookup.Update(this);

            var job = new SystemJob()
            {
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                translatorTypeHandle = translatorTypeHandle,
                optionsTypeHandle = optionsTypeHandle,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                targetComponentLookup = targetComponentLookup,
                entityTypeHandle = entityTypeHandle,
                entityLookup = entityLookup,
                parallelWriter = commandBufferSystem.CreateCommandBuffer().AsParallelWriter()
            };
            Dependency = job.ScheduleParallel(query, Dependency);
            commandBufferSystem.AddJobHandleForProducer(Dependency);
        }

        [BurstCompile]
        unsafe struct SystemJob : IJobChunk
        {
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            public ComponentTypeHandle<TTranslator> translatorTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenTranslationOptionsData> optionsTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            [NativeDisableParallelForRestriction] public ComponentLookup<TComponent> targetComponentLookup;
            [ReadOnly] public EntityTypeHandle entityTypeHandle;
            [ReadOnly] public EntityStorageInfoLookup entityLookup;
            public EntityCommandBuffer.ParallelWriter parallelWriter;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var startValueArrayPtr = chunk.GetComponentDataPtrRW(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRO(ref valueTypeHandle);
                var commandComponentPtr = chunk.GetComponentDataPtrRW(ref translatorTypeHandle);
                var optionsArrayPtr = chunk.GetComponentDataPtrRO(ref optionsTypeHandle);
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var entitiesPtr = chunk.GetEntityDataPtrRO(entityTypeHandle);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translator = *(commandComponentPtr + i);
                    var targetEntity = translator.TargetEntity;
                    if (!entityLookup.Exists(targetEntity)) continue;
                    if (!targetComponentLookup.HasComponent(targetEntity)) continue;

                    ref var target = ref targetComponentLookup.GetRefRW(targetEntity).ValueRW;

                    if ((optionsArrayPtr + i)->value == TweenTranslationOptions.To &&
                        ((accessorFlagsArrayPtr + i)->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                    {
                        (startValueArrayPtr + i)->value = translator.GetValue(ref target);
                    }
                    else if (((accessorFlagsArrayPtr + i)->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                    {
                        var value = (valueArrayPtr + i)->value;
                        translator.Apply(ref target, value);
                    }
                }
            }
        }
    }
}