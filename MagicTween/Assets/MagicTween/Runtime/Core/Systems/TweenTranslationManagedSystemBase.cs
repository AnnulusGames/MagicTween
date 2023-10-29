using System;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    public abstract partial class TweenTranslationManagedSystemBase<TValue, TPlugin, TObject, TTranslator> : SystemBase
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
        where TObject : class
        where TTranslator : unmanaged, ITweenTranslatorManaged<TValue, TObject>
    {
        EntityQuery query;

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>();
            query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TweenStartValue<TValue>, TweenValue<TValue>>()
                .WithAll<TweenTranslationModeData, TweenAccessorFlags, TweenTargetObject, TTranslator>()
                .Build(this);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var job = new SystemJob()
            {
                startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>(),
                valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>(true),
                targetObjectTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenTargetObject>(),
                translationModeTypeHandle = SystemAPI.GetComponentTypeHandle<TweenTranslationModeData>(true),
                accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true),
                entityManager = EntityManager
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job, query);
        }

        unsafe struct SystemJob : IJobChunk
        {
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            public ComponentTypeHandle<TweenTargetObject> targetObjectTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenTranslationModeData> translationModeTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public EntityManager entityManager;

            readonly TTranslator translator;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var startValueArrayPtr = chunk.GetComponentDataPtrRW(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRO(ref valueTypeHandle);
                var translationModeArrayPtr = chunk.GetComponentDataPtrRO(ref translationModeTypeHandle);
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var targetAccessor = chunk.GetManagedComponentAccessor(ref targetObjectTypeHandle, entityManager);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var target = (TObject)targetAccessor[i].target;

                    if (((accessorFlagsArrayPtr + i)->flags & AccessorFlags.Getter) == AccessorFlags.Getter &&
                        (translationModeArrayPtr + i)->value == TweenTranslationMode.To)
                    {
                        try
                        {
                            (startValueArrayPtr + i)->value = translator.GetValue(target);
                        }
                        catch (Exception ex)
                        {
                            Debugger.LogExceptionInsideTween(ex);
                        }
                    }
                    else if (((accessorFlagsArrayPtr + i)->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                    {
                        try
                        {
                            var value = (valueArrayPtr + i)->value;
                            translator.Apply(target, value);
                        }
                        catch (Exception ex)
                        {
                            Debugger.LogExceptionInsideTween(ex);
                        }
                    }
                }
            }
        }
    }
}