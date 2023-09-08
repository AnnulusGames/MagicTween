using Unity.Burst;
using Unity.Mathematics;

namespace MagicTween.Core
{
    [BurstCompile]
    internal static class VibrationUtils
    {
        [BurstCompile]
        public static void EvaluateStrength(in float strength, in int frequency, in float dampingRatio, in float t, out float result)
        {
            if (t == 1f || t == 0f)
            {
                result = 0f;
                return;
            }
            float angularFrequency = (frequency - 0.5f) * math.PI;
            float dampingFactor = dampingRatio * frequency / (2f * math.PI);
            result = strength * math.pow(math.E, -dampingFactor * t) * math.cos(angularFrequency * t);
        }

        [BurstCompile]
        public static void EvaluateStrength(in float2 strength, in int frequency, in float dampingRatio, in float t, out float2 result)
        {
            if (t == 1f || t == 0f)
            {
                result = 0f;
                return;
            }
            float angularFrequency = (frequency - 0.5f) * math.PI;
            float dampingFactor = dampingRatio * frequency / (2f * math.PI);
            result = strength * math.pow(math.E, -dampingFactor * t) * math.cos(angularFrequency * t);
        }

        [BurstCompile]
        public static void EvaluateStrength(in float3 strength, in int frequency, in float dampingRatio, in float t, out float3 result)
        {
            if (t == 1f || t == 0f)
            {
                result = 0f;
                return;
            }
            float angularFrequency = (frequency - 0.5f) * math.PI;
            float dampingFactor = dampingRatio * frequency / (2f * math.PI);
            result = strength * math.pow(math.E, -dampingFactor * t) * math.cos(angularFrequency * t);
        }
    }
}