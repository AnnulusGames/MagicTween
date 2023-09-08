using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

namespace MagicTween.Core
{
    [UpdateInGroup(typeof(MagicTweenCleanupSystemGroup))]
    [BurstCompile]
    public sealed partial class TweenCleanupSystem : SystemBase
    {
        NativeQueue<Entity> queue;
        const int maxDestroyCount = 20000;

        [BurstCompile]
        protected override void OnCreate()
        {
            queue = new NativeQueue<Entity>(Allocator.Persistent);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            CompleteDependency();

            var list = new NativeList<Entity>(math.min(queue.Count, maxDestroyCount), Allocator.Temp);
            for (int i = 0; i < maxDestroyCount; i++)
            {
                if (!queue.TryDequeue(out var entity)) break;
                if (!SystemAPI.Exists(entity)) continue;
                list.Add(entity);
            }
            EntityManager.DestroyEntity(list.AsArray());
            list.Dispose();
        }

        [BurstCompile]
        protected override void OnDestroy()
        {
            queue.Dispose();
        }

        public void Enqueue(in Entity entity)
        {
            queue.Enqueue(entity);
        }

        public NativeQueue<Entity>.ParallelWriter CreateBuffer()
        {
            return queue.AsParallelWriter();
        }

        [BurstCompile]
        public void ClearQueue()
        {
            queue.Clear();
        }
    }
}