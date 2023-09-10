using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenCoreSystemGroup))]
    public partial class TweenSystem : SystemBase
    {
        TweenCleanupSystem cleanupSystem;
        EntityQuery query;

        [BurstCompile]
        protected override void OnCreate()
        {
            cleanupSystem = World.GetExistingSystemManaged<TweenCleanupSystem>();
            query = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenRootFlag>()
                .Build();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();
            var job = new SystemJob()
            {
                deltaTime = UnityEngine.Time.deltaTime,
                unscaledDeltaTime = UnityEngine.Time.unscaledDeltaTime,
                parallelWriter = cleanupSystem.CreateBuffer()
            };
            job.ScheduleParallel(query);
        }

        [BurstCompile]
        protected override void OnDestroy()
        {
            foreach (var easing in SystemAPI.Query<RefRW<TweenEasing>>())
            {
                if (easing.ValueRW.customCurve.IsCreated) easing.ValueRW.customCurve.Dispose();
            }
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            [ReadOnly] public float deltaTime;
            [ReadOnly] public float unscaledDeltaTime;
            [WriteOnly] public NativeQueue<Entity>.ParallelWriter parallelWriter;

            public void Execute(TweenAspect aspect)
            {
                var position = aspect.position + (aspect.ignoreTimeScale ? unscaledDeltaTime : deltaTime) * aspect.playbackSpeed;
                aspect.Update(position, ref parallelWriter);
            }
        }
    }
}