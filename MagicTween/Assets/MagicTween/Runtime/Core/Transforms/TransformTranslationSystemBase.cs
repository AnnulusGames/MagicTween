#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms.Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(TweenTransformTranslationSystemGroup))]
    public unsafe abstract partial class TweenTransformTranslationSystemBase<TValue, TOptions, TPlugin, TTranslator> : SystemBase
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
        where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
        where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
    {
        EntityQuery query;

        NativeArray<ComponentPtr<TweenValue<TValue>>> valuePtrArray;
        NativeArray<ComponentPtr<TweenStartValue<TValue>>> startValuePtrArray;
        NativeArray<ComponentPtr<TweenAccessorFlags>> accessorFlagsPtrArray;
        NativeArray<ComponentPtr<TweenTranslationModeData>> translationModePtrArray;
        NativeArray<UnsafeList<int>> indexLookup;

        const string NULL_ERROR_MESSAGE = "The object of type 'Transform' has been destroyed but you are still trying to access it.";

        [BurstCompile]
        static int GetEntityCount(ref EntityQuery query)
        {
            return query.CalculateEntityCount();
        }

        protected override void OnCreate()
        {
            TweenControllerContainer.Register<TransformTweenController<TValue, TOptions, TPlugin, TTranslator>>();

            query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TweenValue<TValue>, TweenStartValue<TValue>, TweenPluginTag<TPlugin>>()
                .WithAll<TweenAccessorFlags, TweenTargetTransform, TTranslator>()
                .Build(this);

            valuePtrArray = new(32, Allocator.Persistent);
            startValuePtrArray = new(32, Allocator.Persistent);
            accessorFlagsPtrArray = new(32, Allocator.Persistent);
            translationModePtrArray = new(32, Allocator.Persistent);
            indexLookup = new(32, Allocator.Persistent);
        }

        [BurstCompile]
        static void SetPtrs<TComponent>(TComponent* srcPtr, int length, ref NativeArray<ComponentPtr<TComponent>> dst, int dstIndex) where TComponent : unmanaged
        {
            for (int i = 0; i < length; i++) ((ComponentPtr<TComponent>*)dst.GetUnsafePtr() + dstIndex + i)->Ptr = srcPtr + i;
        }

        [BurstCompile]
        unsafe static void ClearLists(ref NativeArray<UnsafeList<int>> array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var list = array[i];
                if (list.IsCreated)
                {
                    list.Clear();
                    array[i] = list;
                }
            }
        }

        [BurstCompile]
        static void AddIndexLookup(ref NativeArray<UnsafeList<int>> indexLookup, int index, int value)
        {
            var indexArray = indexLookup[index];
            if (!indexArray.IsCreated)
            {
                indexArray = new UnsafeList<int>(4, Allocator.Persistent)
                {
                    value
                };
            }
            else
            {
                indexArray.Add(value);
            }
            indexLookup[index] = indexArray;
        }

        [BurstCompile]
        static void EnsureArray<T>(ref NativeArray<T> array, int capacity, NativeArrayOptions nativeArrayOptions = NativeArrayOptions.UninitializedMemory) where T : unmanaged
        {
            if (array.Length < capacity)
            {
                NativeArray<T> nativeArray = new(capacity, Allocator.Persistent, nativeArrayOptions);
                if (array.IsCreated)
                {
                    NativeArray<T>.Copy(array, nativeArray, array.Length);
                    array.Dispose();
                }

                array = nativeArray;
            }
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
            var translationModeHandle = SystemAPI.GetComponentTypeHandle<TweenTranslationModeData>(true);
            var targetTransformHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenTargetTransform>();

            var chunks = query.ToArchetypeChunkArray(Allocator.Temp);

            EnsureArray(ref valuePtrArray, entityCount);
            EnsureArray(ref startValuePtrArray, entityCount);
            EnsureArray(ref accessorFlagsPtrArray, entityCount);
            EnsureArray(ref translationModePtrArray, entityCount);
            EnsureArray(ref indexLookup, TransformManager.Count, NativeArrayOptions.ClearMemory);
            ClearLists(ref indexLookup);

            CompleteDependency();

            for (int chunkIndex = 0; chunkIndex < chunks.Length; chunkIndex++)
            {
                var chunkPtr = (ArchetypeChunk*)chunks.GetUnsafePtr() + chunkIndex;
                var chunkCount = chunkPtr->Count;

                var valuePtr = chunkPtr->GetComponentDataPtrRO(ref tweenValueHandle);
                var startValuePtr = chunkPtr->GetComponentDataPtrRW(ref tweenStartValueHandle);
                var accessorFlagsPtr = chunkPtr->GetComponentDataPtrRO(ref accessorFlagsTypeHandle);
                var translationModePtr = chunkPtr->GetComponentDataPtrRO(ref translationModeHandle);

                SetPtrs(valuePtr, chunkCount, ref valuePtrArray, arrayOffset);
                SetPtrs(startValuePtr, chunkCount, ref startValuePtrArray, arrayOffset);
                SetPtrs(accessorFlagsPtr, chunkCount, ref accessorFlagsPtrArray, arrayOffset);
                SetPtrs(translationModePtr, chunkCount, ref translationModePtrArray, arrayOffset);

                var targets = chunkPtr->GetManagedComponentAccessor(ref targetTransformHandle, EntityManager);

                for (int i = 0; i < chunkCount; i++)
                {
                    var target = targets[i];
#if UNITY_ASSERTIONS
                    if (target.target == null) Debugger.LogExceptionInsideTween(new MissingReferenceException(NULL_ERROR_MESSAGE));
#endif
                    var transformIndex = TransformManager.IndexOf(target.instanceId);
                    AddIndexLookup(ref indexLookup, transformIndex, arrayOffset);

                    arrayOffset++;
                }
            }

            var jobHandle = new SystemJob()
            {
                valuePtrArray = valuePtrArray,
                startValuePtrArray = startValuePtrArray,
                accessorFlagsPtrArray = accessorFlagsPtrArray,
                translationModePtrArray = translationModePtrArray,
                indexLookup = indexLookup
            }.Schedule(transformAccessArray);
            jobHandle.Complete();
        }

        protected override void OnDestroy()
        {
            valuePtrArray.Dispose();
            startValuePtrArray.Dispose();
            accessorFlagsPtrArray.Dispose();
            translationModePtrArray.Dispose();

            for (int i = 0; i < indexLookup.Length; i++)
            {
                var list = indexLookup[i];
                if (!list.IsCreated) list.Dispose();
            }
            indexLookup.Dispose();
        }

        [BurstCompile]
        public unsafe struct SystemJob : IJobParallelForTransform
        {
            readonly TTranslator translator;

            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenValue<TValue>>> valuePtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenStartValue<TValue>>> startValuePtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenAccessorFlags>> accessorFlagsPtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<ComponentPtr<TweenTranslationModeData>> translationModePtrArray;
            [NativeDisableContainerSafetyRestriction] public NativeArray<UnsafeList<int>> indexLookup;

            [BurstCompile]
            public void Execute(int index, TransformAccess transform)
            {
                if (!transform.isValid) return;
                if (indexLookup.Length <= index) return;

                var indexArray = indexLookup[index];
                if (!indexArray.IsCreated) return;

                for (int i = 0; i < indexArray.Length; i++)
                {
                    var componentIndex = indexArray[i];

                    var flags = accessorFlagsPtrArray[componentIndex].Ptr->flags;
                    var translationMode = translationModePtrArray[componentIndex].Ptr->value;
                    if (translationMode == TweenTranslationMode.To && (flags & AccessorFlags.Getter) == AccessorFlags.Getter) startValuePtrArray[componentIndex].Ptr->value = translator.GetValue(ref transform);
                    else if ((flags & AccessorFlags.Setter) == AccessorFlags.Setter) translator.Apply(ref transform, valuePtrArray[componentIndex].Ptr->value);
                }
            }
        }
    }
}
#endif