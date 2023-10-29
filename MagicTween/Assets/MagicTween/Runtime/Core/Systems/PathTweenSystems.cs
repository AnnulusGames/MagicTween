using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using MagicTween.Plugins;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using MagicTween.Core.Aspects;

namespace MagicTween.Core.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PathTweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        unsafe partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, PathTweenAspect valueAspect)
            {
                PathTweenPlugin.EvaluateCore(valueAspect.points, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.Options, out var result);
                valueAspect.CurrentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    [BurstCompile]
    public partial class PathTweenDelegateTranslationSystem : SystemBase
    {
        EntityQuery query1;
        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        BufferTypeHandle<PathPoint> pointsTypeHandle;
        ComponentTypeHandle<TweenValue<float3>> valueTypeHandle;
        ComponentTypeHandle<TweenDelegates<float3>> accessorTypeHandle;

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<DelegateTweenController<float3, PathTweenPlugin>>();
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAspect<PathTweenAspect>()
                .WithAll<TweenDelegates<float3>>()
                .Build();
            accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            pointsTypeHandle = SystemAPI.GetBufferTypeHandle<PathPoint>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<float3>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenDelegates<float3>>(true);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();
            accessorFlagsTypeHandle.Update(this);
            pointsTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            accessorTypeHandle.Update(this);
            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                accessorFlagsTypeHandle = accessorFlagsTypeHandle,
                pointsTypeHandle = pointsTypeHandle,
                valueTypeHandle = valueTypeHandle,
                accessorTypeHandle = accessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);
        }

        unsafe partial struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
            public BufferTypeHandle<PathPoint> pointsTypeHandle;
            public ComponentTypeHandle<TweenValue<float3>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenDelegates<float3>> accessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var accessorFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var pointsBufferAccessor = chunk.GetBufferAccessor(ref pointsTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref accessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var flagsPtr = accessorFlagsArrayPtr + i;
                        if ((flagsPtr->flags & AccessorFlags.Getter) == AccessorFlags.Getter)
                        {
                            if (accessor.getter != null)
                            {
                                var buffer = pointsBufferAccessor[i];
                                buffer[0] = new PathPoint() { point = accessor.getter() };
                            }
                        }
                        if ((flagsPtr->flags & AccessorFlags.Setter) == AccessorFlags.Setter)
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
    }
}