using System;
using Unity.Entities;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using MagicTween.Core.Components;
using MagicTween.Core.Controllers;
using MagicTween.Diagnostics;

namespace MagicTween.Core.Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public abstract partial class TweenDelegateTranslationSystemBase<TValue, TOptions, TPlugin> : SystemBase
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
        where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
    {
        EntityQuery query1;
        EntityQuery query2;

        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
        ComponentTypeHandle<TweenDelegates<TValue>> delegatesTypeHandle;
        ComponentTypeHandle<TweenDelegatesNoAlloc<TValue>> unsafedelegatesTypeHandle;

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<DelegateTweenController<TValue, TOptions, TPlugin>>();
            TweenControllerContainer.Register<NoAllocDelegateTweenController<TValue, TOptions, TPlugin>>();

            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenDelegates<TValue>, TweenOptions<TOptions>, TweenPluginTag<TPlugin>>()
                .Build();
            query2 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenDelegatesNoAlloc<TValue>, TweenOptions<TOptions>, TweenPluginTag<TPlugin>>()
                .Build();

            accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>();
            delegatesTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenDelegates<TValue>>(true);
            unsafedelegatesTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenDelegatesNoAlloc<TValue>>(true);
        }


        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();
            accessorFlagsTypeHandle.Update(this);
            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            delegatesTypeHandle.Update(this);
            unsafedelegatesTypeHandle.Update(this);

            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                delegatesTypeHandle = delegatesTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);

            var job2 = new SystemJob2()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                unsafedelegatesTypeHandle = unsafedelegatesTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job2, query2);
        }

        unsafe partial struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenDelegates<TValue>> delegatesTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var delegatess = chunk.GetManagedComponentAccessor(ref delegatesTypeHandle, entityManager);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var delegates = delegatess[i];
                    if (delegates == null) continue;

                    var accessorFlagsPtr = accessorFlagsArrayPtr + i;

                    if ((accessorFlagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                    {
                        try
                        {
                            if (delegates.getter != null) (startValueArrayPtr + i)->value = delegates.getter();
                        }
                        catch (Exception ex)
                        {
                            Debugger.LogExceptionInsideTween(ex);
                        }
                    }
                    if ((accessorFlagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                    {
                        try
                        {
                            delegates.setter?.Invoke((valueArrayPtr + i)->value);
                        }
                        catch (Exception ex)
                        {
                            Debugger.LogExceptionInsideTween(ex);
                        }
                    }
                }
            }
        }

        unsafe partial struct SystemJob2 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenDelegatesNoAlloc<TValue>> unsafedelegatesTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var delegatess = chunk.GetManagedComponentAccessor(ref unsafedelegatesTypeHandle, entityManager);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var delegates = delegatess[i];
                    if (delegates == null) continue;

                    var accessorFlagsPtr = accessorFlagsArrayPtr + i;

                    if ((accessorFlagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                    {
                        try
                        {
                            if (delegates.getter != null) (startValueArrayPtr + i)->value = delegates.getter(delegates.target);
                        }
                        catch (Exception ex)
                        {
                            Debugger.LogExceptionInsideTween(ex);
                        }
                    }
                    if ((accessorFlagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
                    {
                        try
                        {
                            delegates.setter?.Invoke(delegates.target, (valueArrayPtr + i)->value);
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