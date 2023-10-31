using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using MagicTween.Plugins;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms
{
    static class RegisterTransformControllersEntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            TweenControllerContainer.Register<TransformTweenController<float3, Float3TweenPlugin, TransformPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Float3TweenPlugin, TransformLocalPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Float3TweenPlugin, TransformEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Float3TweenPlugin, TransformLocalEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Float3TweenPlugin, TransformLocalScaleTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalScaleXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalScaleYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, FloatTweenPlugin, TransformLocalScaleZTranslator>>();

            TweenControllerContainer.Register<TransformTweenController<float3, Punch3TweenPlugin, TransformPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Punch3TweenPlugin, TransformLocalPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Punch3TweenPlugin, TransformEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Punch3TweenPlugin, TransformLocalEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Punch3TweenPlugin, TransformLocalScaleTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalScaleXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalScaleYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, PunchTweenPlugin, TransformLocalScaleZTranslator>>();

            TweenControllerContainer.Register<TransformTweenController<float3, Shake3TweenPlugin, TransformPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Shake3TweenPlugin, TransformLocalPositionTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalPositionXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalPositionYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalPositionZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Shake3TweenPlugin, TransformEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Shake3TweenPlugin, TransformLocalEulerAnglesTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalEulerAnglesXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalEulerAnglesYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalEulerAnglesZTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float3, Shake3TweenPlugin, TransformLocalScaleTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalScaleXTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalScaleYTranslator>>();
            TweenControllerContainer.Register<TransformTweenController<float, ShakeTweenPlugin, TransformLocalScaleZTranslator>>();
        }
    }    

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