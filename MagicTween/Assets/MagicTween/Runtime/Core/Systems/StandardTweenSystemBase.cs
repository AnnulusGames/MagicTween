using MagicTween.Core.Components;
using MagicTween.Plugins;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace MagicTween.Core
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public abstract partial class StandardTweenSystemBase<TValue, TOptions, TPlugin> : SystemBase
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
        where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
    {
        EntityQuery query;

        [BurstCompile]
        protected override void OnCreate()
        {
            query = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenEndValue<TValue>, TweenOptions<TOptions>>()
                .Build();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var job = new SystemJob()
            {
                valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>(),
                startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>(true),
                endValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenEndValue<TValue>>(true),
                optionsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenOptions<TOptions>>(true),
                progressTypeHandle = SystemAPI.GetComponentTypeHandle<TweenProgress>(true),
                isInvertedTypeHandle = SystemAPI.GetComponentTypeHandle<TweenInvertFlag>(true),
                isRelativeTypeHandle = SystemAPI.GetComponentTypeHandle<TweenParameterIsRelative>(true),
            };
            Dependency = job.ScheduleParallel(query, Dependency);
        }

        [BurstCompile]
        unsafe struct SystemJob : IJobChunk
        {
            readonly TPlugin plugin;

            public ComponentTypeHandle<TweenValue<TValue>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenStartValue<TValue>> startValueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenEndValue<TValue>> endValueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenOptions<TOptions>> optionsTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenProgress> progressTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenInvertFlag> isInvertedTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenParameterIsRelative> isRelativeTypeHandle;

            [BurstCompile]
            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var endValueArrayPtr = chunk.GetComponentDataPtrRO(ref endValueTypeHandle);
                var optionsArrayPtr = chunk.GetComponentDataPtrRO(ref optionsTypeHandle);
                var progressArrayPtr = chunk.GetComponentDataPtrRO(ref progressTypeHandle);
                var isInvertedArrayPtr = chunk.GetComponentDataPtrRO(ref isInvertedTypeHandle);
                var isRelativeArrayPtr = chunk.GetComponentDataPtrRO(ref isRelativeTypeHandle);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var context = new TweenEvaluationContext(
                        (progressArrayPtr + i)->value,
                        (isRelativeArrayPtr + i)->value,
                        (isInvertedArrayPtr + i)->value
                    );

                    var currentValue = plugin.Evaluate(
                        (startValueArrayPtr + i)->value,
                        (endValueArrayPtr + i)->value,
                        (optionsArrayPtr + i)->value,
                        context
                    );

                    (valueArrayPtr + i)->value = currentValue;
                }
            }
        }
    }
}