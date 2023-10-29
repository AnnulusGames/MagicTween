using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Collections;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Core.Systems;

namespace MagicTween
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public abstract partial class TweenTranslationSystemBase<TValue, TPlugin, TComponent, TTranslator> : SystemBase
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
        where TComponent : unmanaged, IComponentData
        where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
    {
        ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
        ComponentTypeHandle<TweenTargetEntity> targetEntityTypeHandle;
        ComponentTypeHandle<TweenTranslationModeData> optionsTypeHandle;
        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        ComponentLookup<TComponent> targetComponentLookup;
        EntityTypeHandle entityTypeHandle;
        EntityStorageInfoLookup entityLookup;

        EntityQuery query;

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>();

            startValueTypeHandle = GetComponentTypeHandle<TweenStartValue<TValue>>();
            valueTypeHandle = GetComponentTypeHandle<TweenValue<TValue>>(true);
            targetEntityTypeHandle = GetComponentTypeHandle<TweenTargetEntity>();
            optionsTypeHandle = GetComponentTypeHandle<TweenTranslationModeData>(true);
            accessorFlagsTypeHandle = GetComponentTypeHandle<TweenAccessorFlags>(true);
            targetComponentLookup = GetComponentLookup<TComponent>();
            entityTypeHandle = GetEntityTypeHandle();
            entityLookup = GetEntityStorageInfoLookup();

            query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TweenTargetEntity, TweenStartValue<TValue>, TweenAccessorFlags, TweenTranslationModeData, TTranslator>()
                .Build(this);
        }

        protected override void OnUpdate()
        {
            CompleteDependency();

            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            targetEntityTypeHandle.Update(this);
            optionsTypeHandle.Update(this);
            accessorFlagsTypeHandle.Update(this);
            targetComponentLookup.Update(this);
            entityTypeHandle.Update(this);
            entityLookup.Update(this);

            var job = new SystemJob()
            {
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                targetEntityTypeHandle = targetEntityTypeHandle,
                optionsTypeHandle = optionsTypeHandle,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                targetComponentLookup = targetComponentLookup,
                entityTypeHandle = entityTypeHandle,
                entityLookup = entityLookup,
            };
            Dependency = job.ScheduleParallel(query, Dependency);
        }

        [BurstCompile]
        unsafe struct SystemJob : IJobChunk
        {
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            public ComponentTypeHandle<TweenTargetEntity> targetEntityTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenTranslationModeData> optionsTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            [NativeDisableParallelForRestriction] public ComponentLookup<TComponent> targetComponentLookup;
            [ReadOnly] public EntityTypeHandle entityTypeHandle;
            [ReadOnly] public EntityStorageInfoLookup entityLookup;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var startValueArrayPtr = chunk.GetComponentDataPtrRW(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRO(ref valueTypeHandle);
                var targetEntityArrayPtr = chunk.GetComponentDataPtrRW(ref targetEntityTypeHandle);
                var optionsArrayPtr = chunk.GetComponentDataPtrRO(ref optionsTypeHandle);
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var entitiesPtr = chunk.GetEntityDataPtrRO(entityTypeHandle);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translator = default(TTranslator);
                    var targetEntity = (targetEntityArrayPtr + i)->target;
                    if (!entityLookup.Exists(targetEntity)) continue;
                    if (!targetComponentLookup.HasComponent(targetEntity)) continue;

                    ref var target = ref targetComponentLookup.GetRefRW(targetEntity).ValueRW;

                    if ((optionsArrayPtr + i)->value == TweenTranslationMode.To &&
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