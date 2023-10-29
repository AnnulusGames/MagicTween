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
        where TPlugin : unmanaged, ITweenPlugin<TValue>
        where TObject : class
        where TTranslator : class, ITweenTranslatorManaged<TValue, TObject>, new()
    {
        static readonly TTranslator translator = new();

        EntityQuery query;

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<ManagedTweenController<TValue, TPlugin, TObject, TTranslator>>();
            query = SystemAPI.QueryBuilder()
                .WithAll<TweenStartValue<TValue>, TweenValue<TValue>>()
                .WithAll<TweenTranslationMode, TweenAccessorFlags, TweenTargetObject>()
                .Build();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var job = new SystemJob()
            {
                startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>(),
                valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>(true),
                targetObjectTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenTargetObject>(),
                optionsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenTranslationMode>(true),
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
            [ReadOnly] public ComponentTypeHandle<TweenTranslationMode> optionsTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public EntityManager entityManager;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var startValueArrayPtr = chunk.GetComponentDataPtrRW(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRO(ref valueTypeHandle);
                var optionsArrayPtr = chunk.GetComponentDataPtrRO(ref optionsTypeHandle);
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var targetAccessor = chunk.GetManagedComponentAccessor(ref targetObjectTypeHandle, entityManager);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var target = (TObject)targetAccessor[i].target;

                    if ((optionsArrayPtr + i)->value == TweenTranslationOptions.To &&
                        ((accessorFlagsArrayPtr + i)->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
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