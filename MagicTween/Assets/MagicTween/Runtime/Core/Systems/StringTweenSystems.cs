using Unity.Entities;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Plugins;
using MagicTween.Core.Components;
using MagicTween.Core.Aspects;
using MagicTween.Diagnostics;

namespace MagicTween.Core.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct StringTweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            foreach (var valueAspect in SystemAPI.Query<StringTweenAspect>())
            {
                if (valueAspect.StartValue.IsCreated) valueAspect.StartValue.Dispose();
                if (valueAspect.EndValue.IsCreated) valueAspect.EndValue.Dispose();
                if (valueAspect.CurrentValue.IsCreated) valueAspect.CurrentValue.Dispose();
                if (valueAspect.CustomScrambleChars.IsCreated) valueAspect.CustomScrambleChars.Dispose();
            }
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, StringTweenAspect valueAspect)
            {
                if (aspect.status == TweenStatusType.Killed)
                {
                    if (valueAspect.StartValue.IsCreated) valueAspect.StartValue.Dispose();
                    if (valueAspect.EndValue.IsCreated) valueAspect.EndValue.Dispose();
                    if (valueAspect.CurrentValue.IsCreated) valueAspect.CurrentValue.Dispose();
                    if (valueAspect.CustomScrambleChars.IsCreated) valueAspect.CustomScrambleChars.Dispose();
                    return;
                }

                if (!valueAspect.CurrentValue.IsCreated) return;

                StringTweenPlugin.EvaluateCore(
                    ref valueAspect.StartValue,
                    ref valueAspect.EndValue,
                    aspect.progress,
                    aspect.inverted,
                    valueAspect.RichTextEnabled,
                    valueAspect.ScrambleMode,
                    ref valueAspect.CustomScrambleChars,
                    out var text
                );
                valueAspect.CurrentValue.CopyFrom(text);
                text.Dispose();
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class StringDeletaTweenTranslationSystem : SystemBase
    {
        EntityQuery query1;
        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        ComponentTypeHandle<TweenStartValue<UnsafeText>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<UnsafeText>> valueTypeHandle;
        ComponentTypeHandle<TweenDelegates<string>> accessorTypeHandle;

        protected override void OnCreate()
        {
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAspect<StringTweenAspect>()
                .WithAll<TweenDelegates<string>>()
                .Build();
            accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<UnsafeText>>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<UnsafeText>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenDelegates<string>>(true);
        }

        protected override void OnUpdate()
        {
            CompleteDependency();
            accessorFlagsTypeHandle.Update(this);
            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            accessorTypeHandle.Update(this);
            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                accessorTypeHandle = accessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);
        }

        unsafe struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<UnsafeText>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<UnsafeText>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenDelegates<string>> accessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var callbackFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref accessorTypeHandle, entityManager);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var accessor = accessors[i];
                    if (accessor == null) continue;

                    var flagsPtr = callbackFlagsArrayPtr + i;
                    if ((flagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                    {
                        if (accessor.getter != null)
                        {
                            try
                            {
                                var ptr = startValueArrayPtr + i;
                                if (ptr->value.IsCreated) ptr->value.CopyFrom(accessor.getter());
                            }
                            catch (System.Exception ex)
                            {
                                Debugger.LogExceptionInsideTween(ex);
                            }
                        }
                    }
                    if ((flagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                    {
                        if (accessor.setter != null)
                        {
                            try
                            {
                                var ptr = valueArrayPtr + i;
                                if (ptr->value.IsCreated) accessor.setter(ptr->value.ConvertToString());
                            }
                            catch (System.Exception ex)
                            {
                                Debugger.LogExceptionInsideTween(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}