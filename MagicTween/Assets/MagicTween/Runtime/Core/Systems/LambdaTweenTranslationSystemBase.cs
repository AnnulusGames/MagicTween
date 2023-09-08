using System;
using Unity.Entities;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
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

        ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
        ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessor<TValue>> accessorTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessorUnsafe<TValue>> unsafeAccessorTypeHandle;

        [BurstCompile]
        protected override void OnCreate()
        {
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenPropertyAccessor<TValue>>()
                .Build();
            query2 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenPropertyAccessorUnsafe<TValue>>()
                .Build();

            callbackFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenCallbackFlags>(true);
            startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessor<TValue>>(true);
            unsafeAccessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessorUnsafe<TValue>>(true);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();
            callbackFlagsTypeHandle.Update(this);
            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            accessorTypeHandle.Update(this);
            unsafeAccessorTypeHandle.Update(this);

            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                callbackFlagsTypeHandle = callbackFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                accessorTypeHandle = accessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);

            var job2 = new SystemJob2()
            {
                entityManager = EntityManager,
                callbackFlagsTypeHandle = callbackFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                unsafeAccessorTypeHandle = unsafeAccessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job2, query2);
        }

        unsafe partial struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessor<TValue>> accessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var callbackFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref callbackFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref accessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var callbackPtr = callbackFlagsArrayPtr + i;
                        if ((callbackPtr->flags & CallbackFlags.OnKill) == CallbackFlags.OnKill) continue;

                        if ((callbackPtr->flags & CallbackFlags.OnStartUp) == CallbackFlags.OnStartUp)
                        {
                            if (accessor.getter != null) (startValueArrayPtr + i)->value = accessor.getter();
                        }
                        if ((callbackPtr->flags & (CallbackFlags.OnUpdate | CallbackFlags.OnComplete | CallbackFlags.OnRewind)) != 0)
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
            [ReadOnly] public ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessorUnsafe<TValue>> unsafeAccessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var callbackFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref callbackFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref unsafeAccessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var callbackPtr = callbackFlagsArrayPtr + i;
                        if ((callbackPtr->flags & CallbackFlags.OnKill) == CallbackFlags.OnKill) continue;

                        if ((callbackPtr->flags & CallbackFlags.OnStartUp) == CallbackFlags.OnStartUp)
                        {
                            if (accessor.getter == null) continue;
                            (startValueArrayPtr + i)->value = accessor.getter(accessor.target);
                        }
                        else if ((callbackPtr->flags & (CallbackFlags.OnUpdate | CallbackFlags.OnComplete | CallbackFlags.OnRewind)) != 0)
                        {
                            if (accessor.setter == null) continue;
                            accessor.setter(accessor.target, (valueArrayPtr + i)->value);
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