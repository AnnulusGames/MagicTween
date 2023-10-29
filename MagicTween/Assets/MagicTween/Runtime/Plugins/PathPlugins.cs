using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween.Plugins
{
    // TODO: Support for NoAlloc Tween.To methods

    public struct PathTweenOptions : ITweenOptions
    {
        public PathType pathType;
        public byte isClosed;
    }

    [BurstCompile]
    public readonly struct PathTweenPlugin : ITweenPluginBase<float3>
    {
        public float3 Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var buffer = entityManager.GetBuffer<PathPoint>(entity);
            var options = entityManager.GetComponentData<TweenOptions<PathTweenOptions>>(entity).value;
            EvaluateCore(buffer, context.Progress, context.IsRelative, context.IsInverted, options, out var result);
            return result;
        }

        [BurstCompile]
        internal unsafe static void EvaluateCore(in DynamicBuffer<PathPoint> points, float t, bool isRelative, bool isFrom, in PathTweenOptions options, out float3 result)
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
}