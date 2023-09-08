using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;

namespace MagicTween.Core
{
    [BurstCompile]
    internal static class CurveUtils
    {
        [BurstCompile]
        public static void CatmullRomSpline(in NativeArray<float3> points, float t, out float3 result)
        {
            int l = points.Length;

            if (l == 0)
            {
                result = default;
                return;
            }
            else if (l == 1)
            {
                result = points[0];
                return;
            }

            float progress = (l - 1) * t;
            int i = (int)math.floor(progress);
            float weight = progress - i;

            if (MathUtils.Approximately(weight, 0f) && i >= l - 1)
            {
                i = l - 2;
                weight = 1;
            }

            float3 p0 = points[i];
            float3 p1 = points[i + 1];

            float3 v0;
            if (i > 0)
            {
                v0 = 0.5f * (points[i + 1] - points[i - 1]);
            }
            else
            {
                v0 = points[i + 1] - points[i];
            }

            float3 v1;
            if (i < l - 2)
            {
                v1 = 0.5f * (points[i + 2] - points[i]);
            }
            else
            {
                v1 = points[i + 1] - points[i];
            }

            HermiteCurve(p0, p1, v0, v1, weight, out result);
        }

        [BurstCompile]
        public static void HermiteCurve(in float3 p0, in float3 p1, in float3 v0, in float3 v1, float t, out float3 result)
        {
            float3 c0 = 2f * p0 + -2f * p1 + v0 + v1;
            float3 c1 = -3f * p0 + 3f * p1 + -2f * v0 - v1;
            float3 c2 = v0;
            float3 c3 = p0;

            result = t * t * t * c0 +
                t * t * c1 +
                t * c2 +
                c3;
        }

        [BurstCompile]
        public static void Linear(in NativeArray<float3> points, float t, out float3 result)
        {
            int l = points.Length;

            if (l == 0)
            {
                result = default;
                return;
            }
            else if (l == 1)
            {
                result = points[0];
                return;
            }

            float progress = (l - 1) * t;
            int i = (int)math.floor(progress);
            float weight = progress - i;

            if (MathUtils.Approximately(weight, 0f) && i >= l - 1)
            {
                i = l - 2;
                weight = 1;
            }

            result = math.lerp(points[i], points[i + 1], weight);
        }
    }
}