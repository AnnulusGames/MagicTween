using System;
using Unity.Entities;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween.Core
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    public abstract partial class LambdaTweenTranslationSystemBase<TValue> : SystemBase
        where TValue : unmanaged
    {
        EntityQuery query1;
        EntityQuery query2;

        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessor<TValue>> accessorTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessorNoAlloc<TValue>> unsafeAccessorTypeHandle;

        [BurstCompile]
        protected override void OnCreate()
        {
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenPropertyAccessor<TValue>>()
                .Build();
            query2 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenPropertyAccessorNoAlloc<TValue>>()
                .Build();

            accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessor<TValue>>(true);
            unsafeAccessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessorNoAlloc<TValue>>(true);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();
            accessorFlagsTypeHandle.Update(this);
            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            accessorTypeHandle.Update(this);
            unsafeAccessorTypeHandle.Update(this);

            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                accessorTypeHandle = accessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);

            var job2 = new SystemJob2()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                unsafeAccessorTypeHandle = unsafeAccessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job2, query2);
        }

        unsafe partial struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessor<TValue>> accessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref accessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var accessorFlagsPtr = accessorFlagsArrayPtr + i;

                        if ((accessorFlagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                        {
                            if (accessor.getter != null) (startValueArrayPtr + i)->value = accessor.getter();
                        }
                        if ((accessorFlagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                        {
                            accessor.setter?.Invoke((valueArrayPtr + i)->value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debugger.LogExceptionInsideTween(ex);
                }
            }
        }

        unsafe partial struct SystemJob2 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessorNoAlloc<TValue>> unsafeAccessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref unsafeAccessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var accessorFlagsPtr = accessorFlagsArrayPtr + i;

                        if ((accessorFlagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                        {
                            if (accessor.getter != null) (startValueArrayPtr + i)->value = accessor.getter(accessor.target);
                        }
                        if ((accessorFlagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                        {
                            accessor.setter?.Invoke(accessor.target, (valueArrayPtr + i)->value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debugger.LogExceptionInsideTween(ex);
                }
            }
        }
    }
}