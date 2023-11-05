#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
using Unity.Burst;
using Unity.Collections;
using UnityEngine.Jobs;

namespace MagicTween.Core.Transforms
{
    [BurstCompile]
    internal static class TransformManager
    {
        static TransformAccessArray transformAccessArray;
        static readonly SharedStatic<NativeHashMap<int, int>> instanceIdToArrayIndex = SharedStatic<NativeHashMap<int, int>>.GetOrCreate<SharedIdToIndexKey>();
        readonly struct SharedIdToIndexKey { }

        public static bool IsCreated { get; private set; }

        public static ref TransformAccessArray GetAccessArrayRef()
        {
            return ref transformAccessArray;
        }

        public static int Count
        {
            get => transformAccessArray.length;
        }

        public static void Initialize()
        {
            transformAccessArray = new TransformAccessArray(32);
            instanceIdToArrayIndex.Data = new NativeHashMap<int, int>(32, Allocator.Persistent);
            IsCreated = true;
        }

        public static void Dispose()
        {
            if (!IsCreated) return;
            if (transformAccessArray.isCreated) transformAccessArray.Dispose();
            if (instanceIdToArrayIndex.Data.IsCreated) instanceIdToArrayIndex.Data.Dispose();

            IsCreated = false;
        }

        public static void Register(TweenTargetTransform target)
        {
            if (target.isRegistered) return;
            target.isRegistered = true;

            var instanceId = target.instanceId;
            if (IsCreated && !instanceIdToArrayIndex.Data.ContainsKey(instanceId))
            {
                var index = transformAccessArray.length;
                transformAccessArray.Add(target.target);
                instanceIdToArrayIndex.Data.Add(instanceId, index);
            }
        }

        public static void Unregister(TweenTargetTransform target)
        {
            if (!target.isRegistered) return;
            target.isRegistered = false;

            if (IsCreated && instanceIdToArrayIndex.Data.TryGetValue(target.instanceId, out var index))
            {
                if (transformAccessArray.length == 1)
                {
                    instanceIdToArrayIndex.Data.Remove(target.instanceId);
                    transformAccessArray.RemoveAtSwapBack(index);
                }
                else
                {
                    var lastIndex = transformAccessArray.length - 1;
                    var lastInstanceId = transformAccessArray[lastIndex].GetInstanceID();

                    instanceIdToArrayIndex.Data.Remove(target.instanceId);
                    transformAccessArray.RemoveAtSwapBack(index);

                    instanceIdToArrayIndex.Data[lastInstanceId] = index;
                }
            }
        }

        [BurstCompile]
        public static int IndexOf(int instanceId)
        {
            return instanceIdToArrayIndex.Data[instanceId];
        }
    }
}
#endif