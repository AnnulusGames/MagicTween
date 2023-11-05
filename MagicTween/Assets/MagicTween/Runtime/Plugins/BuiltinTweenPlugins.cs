using Unity.Burst;
using Unity.Mathematics;

namespace MagicTween.Plugins
{
    public struct IntegerTweenOptions : ITweenOptions
    {
        public RoundingMode roundingMode;
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct DoubleTweenPlugin : ICustomTweenPlugin<double, NoOptions>
    {
        [BurstCompile]
        public double Evaluate(in double startValue, in double endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Double2TweenPlugin : ICustomTweenPlugin<double2, NoOptions>
    {
        [BurstCompile]
        public double2 Evaluate(in double2 startValue, in double2 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Double3TweenPlugin : ICustomTweenPlugin<double3, NoOptions>
    {
        [BurstCompile]
        public double3 Evaluate(in double3 startValue, in double3 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Double4TweenPlugin : ICustomTweenPlugin<double4, NoOptions>
    {
        [BurstCompile]
        public double4 Evaluate(in double4 startValue, in double4 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct FloatTweenPlugin : ICustomTweenPlugin<float, NoOptions>
    {
        [BurstCompile]
        public float Evaluate(in float startValue, in float endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Float2TweenPlugin : ICustomTweenPlugin<float2, NoOptions>
    {
        [BurstCompile]
        public float2 Evaluate(in float2 startValue, in float2 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Float3TweenPlugin : ICustomTweenPlugin<float3, NoOptions>
    {
        [BurstCompile]
        public float3 Evaluate(in float3 startValue, in float3 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Float4TweenPlugin : ICustomTweenPlugin<float4, NoOptions>
    {
        [BurstCompile]
        public float4 Evaluate(in float4 startValue, in float4 endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;
            if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
            else return math.lerp(startValue, resolvedEndValue, context.Progress);
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct IntTweenPlugin : ICustomTweenPlugin<int, IntegerTweenOptions>
    {
        [BurstCompile]
        public int Evaluate(in int startValue, in int endValue, in IntegerTweenOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

            float value;
            if (context.IsInverted) value = math.lerp(resolvedEndValue, startValue, context.Progress);
            else value = math.lerp(startValue, resolvedEndValue, context.Progress);

            return options.roundingMode switch
            {
                RoundingMode.AwayFromZero => value >= 0f ? (int)math.ceil(value) : (int)math.floor(value),
                RoundingMode.ToZero => (int)math.trunc(value),
                RoundingMode.ToPositiveInfinity => (int)math.ceil(value),
                RoundingMode.ToNegativeInfinity => (int)math.floor(value),
                _ => (int)math.round(value),
            };
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Int2TweenPlugin : ICustomTweenPlugin<int2, IntegerTweenOptions>
    {
        [BurstCompile]
        public int2 Evaluate(in int2 startValue, in int2 endValue, in IntegerTweenOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

            float2 value;
            if (context.IsInverted) value = math.lerp(resolvedEndValue, startValue, context.Progress);
            else value = math.lerp(startValue, resolvedEndValue, context.Progress);

            switch (options.roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    return (int2)math.round(value);
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    return new int2(x, y);
                case RoundingMode.ToZero:
                    return (int2)math.trunc(value);
                case RoundingMode.ToPositiveInfinity:
                    return (int2)math.ceil(value);
                case RoundingMode.ToNegativeInfinity:
                    return (int2)math.floor(value);
            }
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Int3TweenPlugin : ICustomTweenPlugin<int3, IntegerTweenOptions>
    {
        [BurstCompile]
        public int3 Evaluate(in int3 startValue, in int3 endValue, in IntegerTweenOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

            float3 value;
            if (context.IsInverted) value = math.lerp(resolvedEndValue, startValue, context.Progress);
            else value = math.lerp(startValue, resolvedEndValue, context.Progress);

            switch (options.roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    return (int3)math.round(value);
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    var z = value.z >= 0f ? (int)math.ceil(value.z) : (int)math.floor(value.z);
                    return new int3(x, y, z);
                case RoundingMode.ToZero:
                    return (int3)math.trunc(value);
                case RoundingMode.ToPositiveInfinity:
                    return (int3)math.ceil(value);
                case RoundingMode.ToNegativeInfinity:
                    return (int3)math.floor(value);
            }
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct Int4TweenPlugin : ICustomTweenPlugin<int4, IntegerTweenOptions>
    {
        [BurstCompile]
        public int4 Evaluate(in int4 startValue, in int4 endValue, in IntegerTweenOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

            float4 value;
            if (context.IsInverted) value = math.lerp(resolvedEndValue, startValue, context.Progress);
            else value = math.lerp(startValue, resolvedEndValue, context.Progress);

            switch (options.roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    return (int4)math.round(value);
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    var z = value.z >= 0f ? (int)math.ceil(value.z) : (int)math.floor(value.z);
                    var w = value.w >= 0f ? (int)math.ceil(value.w) : (int)math.floor(value.w);
                    return new int4(x, y, z, w);
                case RoundingMode.ToZero:
                    return (int4)math.trunc(value);
                case RoundingMode.ToPositiveInfinity:
                    return (int4)math.ceil(value);
                case RoundingMode.ToNegativeInfinity:
                    return (int4)math.floor(value);
            }
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct LongTweenPlugin : ICustomTweenPlugin<long, IntegerTweenOptions>
    {
        [BurstCompile]
        public long Evaluate(in long startValue, in long endValue, in IntegerTweenOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

            float value;
            if (context.IsInverted) value = math.lerp(resolvedEndValue, startValue, context.Progress);
            else value = math.lerp(startValue, resolvedEndValue, context.Progress);

            return options.roundingMode switch
            {
                RoundingMode.AwayFromZero => value >= 0f ? (long)math.ceil(value) : (long)math.floor(value),
                RoundingMode.ToZero => (long)math.trunc(value),
                RoundingMode.ToPositiveInfinity => (long)math.ceil(value),
                RoundingMode.ToNegativeInfinity => (long)math.floor(value),
                _ => (long)math.round(value),
            };
        }
    }

    [BurstCompile]
    [TweenPlugin]
    public readonly struct QuaternionTweenPlugin : ICustomTweenPlugin<quaternion, NoOptions>
    {
        [BurstCompile]
        public quaternion Evaluate(in quaternion startValue, in quaternion endValue, in NoOptions options, in TweenEvaluationContext context)
        {
            var resolvedEndValue = context.IsRelative ? math.mul(startValue, endValue) : endValue;
            if (context.IsInverted) return math.slerp(resolvedEndValue, startValue, context.Progress);
            else return math.slerp(startValue, resolvedEndValue, context.Progress);
        }
    }
}