using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(TweenTransformTranslationSystemGroup))]
    public unsafe abstract partial class TweenTransformTranslationSystemBase<TValue, TPlugin, TTranslator> : SystemBase
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPluginBase<TValue>
        where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
    {
        EntityQuery query;

        const string NULL_ERROR_MESSAGE = "The object of type 'Transform' has been destroyed but you are still trying to access it.";

        [BurstCompile]
        static int GetEntityCount(ref EntityQuery query)
        {
            return query.CalculateEntityCount();
        }

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<TransformTweenController<TValue, TPlugin, TTranslator>>();

            query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>>()
                .WithAll<TweenAccessorFlags, TweenTargetTransform, TTranslator>()
                .Build(this);
        }

        [BurstCompile]
        static void SetPtrs<TComponent>(TComponent* srcPtr, int length, ref NativeArray<ComponentPtr<TComponent>> dst, int dstIndex) where TComponent : unmanaged
        {
            for (int i = 0; i < length; i++) ((ComponentPtr<TComponent>*)dst.GetUnsafePtr() + dstIndex + i)->Ptr = srcPtr + i;
        }

        protected override void OnUpdate()
        {
            if (!TransformManager.IsCreated) return;

            var entityCount = GetEntityCount(ref query);
            if (entityCount == 0) return;

            ref var transformAccessArray = ref TransformManager.GetAccessArrayRef();

            var arrayOffset = 0;

            var tweenValueHandle = SystemAPI.GetComponentTypeHandle<TweenValue<TValue>>(true);
            var tweenStartValueHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<TValue>>();
            var accessorFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenAccessorFlags>(true);
            var targetTransformHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenTargetTransform>();

            var chunks = query.ToArchetypeChunkArray(Allocator.Temp);

            var valuePtrArray = new NativeArray<ComponentPtr<TweenValue<TValue>>>(entityCount, Allocator.TempJob);
            var startValuePtrArray = new NativeArray<ComponentPtr<TweenStartValue<TValue>>>(entityCount, Allocator.TempJob);
            var accessorFlagsPtrArray = new NativeArray<ComponentPtr<TweenAccessorFlags>>(entityCount, Allocator.TempJob);
            var indexLookup = new NativeHashMap<int, int>(entityCount, Allocator.TempJob);

            CompleteDependency();

            for (int chunkIndex = 0; chunkIndex < chunks.Length; chunkIndex++)
            {
                var chunkPtr = (ArchetypeChunk*)chunks.GetUnsafePtr() + chunkIndex;
                var chunkCount = chunkPtr->Count;

                var valuePtr = chunkPtr->GetComponentDataPtrRO(ref tweenValueHandle);
                var startValuePtr = chunkPtr->GetComponentDataPtrRW(ref tweenStartValueHandle);
                var accessorFlagsPtr = chunkPtr->GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var targets = chunkPtr->GetManagedComponentAccessor(ref targetTransformHandle, EntityManager);

                SetPtrs(valuePtr, chunkCount, ref valuePtrArray, arrayOffset);
                SetPtrs(startValuePtr, chunkCount, ref startValuePtrArray, arrayOffset);
                SetPtrs(accessorFlagsPtr, chunkCount, ref accessorFlagsPtrArray, arrayOffset);

                for (int i = 0; i < chunkCount; i++)
                {
                    var target = targets[i];
#if UNITY_ASSERTIONS
                    if (target.target == null) Debugger.LogExceptionInsideTween(new MissingReferenceException(NULL_ERROR_MESSAGE));
#endif
                    var transformIndex = TransformManager.IndexOf(target.instanceId);
                    indexLookup[transformIndex] = arrayOffset;

                    arrayOffset++;
                }
            }

            var jobHandle = new SystemJob()
            {
                valuePtrArray = valuePtrArray,
                startValuePtrArray = startValuePtrArray,
                accessorFlagsPtrArray = accessorFlagsPtrArray,
                indexLookup = indexLookup
            }.Schedule(transformAccessArray);
            jobHandle.Complete();

            valuePtrArray.Dispose();
            startValuePtrArray.Dispose();
            accessorFlagsPtrArray.Dispose();
            indexLookup.Dispose();
        }

        [BurstCompile]
        public unsafe struct SystemJob : IJobParallelForTransform
        {
            readonly TTranslator translator;

            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenValue<TValue>>> valuePtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenStartValue<TValue>>> startValuePtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenAccessorFlags>> accessorFlagsPtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeHashMap<int, int> indexLookup;

            [BurstCompile]
            public void Execute(int index, TransformAccess transform)
            {
                if (!transform.isValid) return;
                if (!indexLookup.TryGetValue(index, out var componentIndex)) return;

                var flags = accessorFlagsPtrArray[componentIndex].Ptr->flags;
                if ((flags & AccessorFlags.Getter) == AccessorFlags.Getter) startValuePtrArray[componentIndex].Ptr->value = translator.GetValue(ref transform);
                if ((flags & AccessorFlags.Setter) == AccessorFlags.Setter) translator.Apply(ref transform, valuePtrArray[componentIndex].Ptr->value);
            }
        }
    }
}