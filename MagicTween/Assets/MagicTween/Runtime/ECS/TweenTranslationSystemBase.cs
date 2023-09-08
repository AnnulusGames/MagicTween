using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Collections;
using MagicTween.Core;

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
        ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
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
            callbackFlagsTypeHandle = GetComponentTypeHandle<TweenCallbackFlags>(true);
            targetComponentLookup = GetComponentLookup<TComponent>();
            entityTypeHandle = GetEntityTypeHandle();
            entityLookup = GetEntityStorageInfoLookup();

            query = GetEntityQuery(
                typeof(TTranslator),
                typeof(TweenStartValue<TValue>),
                typeof(TweenValue<TValue>),
                typeof(TweenCallbackFlags),
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
            callbackFlagsTypeHandle.Update(this);
            targetComponentLookup.Update(this);
            entityTypeHandle.Update(this);
            entityLookup.Update(this);

            var job = new SystemJob()
            {
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                translatorTypeHandle = translatorTypeHandle,
                optionsTypeHandle = optionsTypeHandle,
                callbackFlagsTypeHandle = callbackFlagsTypeHandle,
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
            [ReadOnly] public ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
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
                var callbackFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref callbackFlagsTypeHandle);
                var entitiesPtr = chunk.GetEntityDataPtrRO(entityTypeHandle);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translator = *(commandComponentPtr + i);
                    var targetEntity = translator.TargetEntity;
                    if (!entityLookup.Exists(targetEntity)) continue;

                    ref var target = ref targetComponentLookup.GetRefRW(targetEntity).ValueRW;

                    if ((optionsArrayPtr + i)->options == TweenTranslationOptions.To &&
                        ((callbackFlagsArrayPtr + i)->flags & CallbackFlags.OnStartUp) == CallbackFlags.OnStartUp)
                    {
                        (startValueArrayPtr + i)->value = translator.GetValue(ref target);
                    }
                    else if (((callbackFlagsArrayPtr + i)->flags & (CallbackFlags.OnUpdate | CallbackFlags.OnComplete | CallbackFlags.OnRewind)) != 0)
                    {
                        var value = (valueArrayPtr + i)->value;
                        translator.Apply(ref target, value);
                    }
                }
            }
        }
    }
}