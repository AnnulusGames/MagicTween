using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

[assembly: RegisterGenericComponentType(typeof(TweenOptions<PathTweenOptions>))]

namespace MagicTween.Core
{
    // TODO: Support for Unsafe Tween.To methods

    [InternalBufferCapacity(10)]
    public struct PathPoint : IBufferElementData
    {
        public float3 point;
    }

    public struct PathTweenOptions : ITweenOptions
    {
        public PathType pathType;
        public byte isClosed;
    }

    public readonly partial struct PathTweenAspect : IAspect
    {
        public readonly DynamicBuffer<PathPoint> points;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<TweenOptions<PathTweenOptions>> optionsRef;

        public float3 currentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public PathTweenOptions options => optionsRef.ValueRO.value;
    }

    [BurstCompile]
    public readonly struct PathTweenPlugin : ITweenPlugin<float3>
    {
        public float3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out float3 result)
        {
            var buffer = entityManager.GetBuffer<PathPoint>(entity);
            var options = entityManager.GetComponentData<TweenOptions<PathTweenOptions>>(entity).value;
            EvaluateCore(buffer, t, isRelative, isFrom, options, out result);
        }

        [BurstCompile]
        public unsafe static void EvaluateCore(in DynamicBuffer<PathPoint> points, float t, bool isRelative, bool isFrom, in PathTweenOptions options, out float3 result)
        {
            if (points.Length == 0)
            {
                result = default;
                return;
            }

            var length = points.Length + (options.isClosed == 1 ? 1 : 0);
            var pointList = new NativeArray<float3>(length, Allocator.Temp);

            UnsafeUtility.MemCpy((float3*)pointList.GetUnsafePtr(), points.GetUnsafePtr(), points.Length * sizeof(float3));

            if (isRelative)
            {
                for (int i = 1; i < points.Length; i++)
                {
                    pointList[i] += pointList[0];
                }
            }

            if (options.isClosed == 1) pointList[pointList.Length - 1] = pointList[0];

            if (isFrom) // reverse list
            {
                var halfLength = pointList.Length / 2;
                var i = 0;
                var j = pointList.Length - 1;

                for (; i < halfLength; i++, j--)
                {
                    (pointList[i], pointList[j]) = (pointList[j], pointList[i]);
                }
            }

            float3 currentValue = default;
            switch (options.pathType)
            {
                case PathType.Linear:
                    CurveUtils.Linear(in pointList, t, out currentValue);
                    break;
                case PathType.CatmullRom:
                    CurveUtils.CatmullRomSpline(in pointList, t, out currentValue);
                    break;
            }
            result = currentValue;

            pointList.Dispose();
        }
    }

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
                PathTweenPlugin.EvaluateCore(valueAspect.points, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.options, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public partial class LambdaPathTweenTranslationSystem : SystemBase
    {
        EntityQuery query1;
        EntityQuery query2;
        ComponentTypeHandle<TweenAccessorFlags> accessorFlagsTypeHandle;
        BufferTypeHandle<PathPoint> pointsTypeHandle;
        ComponentTypeHandle<TweenValue<float3>> valueTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessor<float3>> accessorTypeHandle;

        protected override void OnCreate()
        {
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAspect<PathTweenAspect>()
                .WithAll<TweenPropertyAccessor<float3>>()
                .Build();
            query2 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAspect<PathTweenAspect>()
                .Build();
            accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            pointsTypeHandle = SystemAPI.GetBufferTypeHandle<PathPoint>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<float3>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessor<float3>>(true);
        }

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
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessor<float3>> accessorTypeHandle;

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