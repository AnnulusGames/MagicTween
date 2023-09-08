using Unity.Mathematics;
using Unity.Burst;

namespace MagicTween
{
    [BurstCompile]
    public static class EaseUtility
    {
        const float PI = math.PI;

        public static float Evaluate(float t, Ease ease)
        {
            return ease switch
            {
                Ease.InSine => InSine(t),
                Ease.OutSine => OutSine(t),
                Ease.InOutSine => InOutSine(t),
                Ease.InQuad => InQuad(t),
                Ease.OutQuad => OutQuad(t),
                Ease.InOutQuad => InOutQuad(t),
                Ease.InCubic => InCubic(t),
                Ease.OutCubic => OutCubic(t),
                Ease.InOutCubic => InOutCubic(t),
                Ease.InQuart => InQuart(t),
                Ease.OutQuart => OutQuart(t),
                Ease.InOutQuart => InOutQuart(t),
                Ease.InQuint => InQuint(t),
                Ease.OutQuint => OutQuint(t),
                Ease.InOutQuint => InOutQuint(t),
                Ease.InExpo => InExpo(t),
                Ease.OutExpo => OutExpo(t),
                Ease.InOutExpo => InOutExpo(t),
                Ease.InCirc => InCirc(t),
                Ease.OutCirc => OutCirc(t),
                Ease.InOutCirc => InOutCirc(t),
                Ease.InElastic => InElastic(t),
                Ease.OutElastic => OutElastic(t),
                Ease.InOutElastic => InOutElastic(t),
                Ease.InBack => InBack(t),
                Ease.OutBack => OutBack(t),
                Ease.InOutBack => InOutBack(t),
                Ease.InBounce => InBounce(t),
                Ease.OutBounce => OutBounce(t),
                Ease.InOutBounce => InOutBounce(t),
                Ease.Custom => 1,
                _ => t,
            };
        }

        [BurstCompile]
        public static float InSine(float t)
        {
            return 1f - math.cos((t * PI) * 0.5f);
        }

        [BurstCompile]
        public static float OutSine(float t)
        {
            return math.sin((t * PI) * 0.5f);
        }

        [BurstCompile]
        public static float InOutSine(float t)
        {
            return -(math.cos(t * PI) - 1) * 0.5f;
        }

        [BurstCompile]
        public static float InQuad(float t)
        {
            return t * t;
        }

        [BurstCompile]
        public static float OutQuad(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }

        [BurstCompile]
        public static float InOutQuad(float t)
        {
            return t < 0.5f
                ? 2 * t * t
                : 1 - (math.pow(-2 * t + 2, 2) * 0.5f);
        }

        [BurstCompile]
        public static float InCubic(float t)
        {
            return t * t * t;
        }

        [BurstCompile]
        public static float OutCubic(float t)
        {
            return 1 - math.pow(1 - t, 3);
        }

        [BurstCompile]
        public static float InOutCubic(float t)
        {
            return t < 0.5f
                ? 4 * t * t * t
                : 1 - math.pow(-2 * t + 2, 3) * 0.5f;
        }

        [BurstCompile]
        public static float InQuart(float t)
        {
            return t * t * t * t;
        }

        [BurstCompile]
        public static float OutQuart(float t)
        {
            return 1 - math.pow(1 - t, 4);
        }

        [BurstCompile]
        public static float InOutQuart(float t)
        {
            return t < 0.5f
                ? 8 * t * t * t * t
                : 1 - math.pow(-2 * t + 2, 4) * 0.5f;
        }

        [BurstCompile]
        public static float InQuint(float t)
        {
            return t * t * t * t * t;
        }

        [BurstCompile]
        public static float OutQuint(float t)
        {
            return 1 - math.pow(1 - t, 5);
        }

        [BurstCompile]
        public static float InOutQuint(float t)
        {
            return t < 0.5f
                ? 16 * t * t * t * t * t
                : 1 - math.pow(-2 * t + 2, 5) * 0.5f;
        }

        [BurstCompile]
        public static float InExpo(float t)
        {
            return t == 0
                ? 0
                : math.pow(2, 10 * (t - 1));
        }

        [BurstCompile]
        public static float OutExpo(float t)
        {
            return t == 1
                ? 1
                : -math.pow(2, -10 * t) + 1;
        }

        [BurstCompile]
        public static float InOutExpo(float t)
        {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return t < 0.5f
                ? math.pow(2, 20 * t - 10) * 0.5f
                : (2 - math.pow(2, -20 * t + 10)) * 0.5f;
        }

        [BurstCompile]
        public static float InCirc(float t)
        {
            return 1 - math.sqrt(1 - math.pow(t, 2));
        }

        [BurstCompile]
        public static float OutCirc(float t)
        {
            return math.sqrt(1 - math.pow(t - 1, 2));
        }

        [BurstCompile]
        public static float InOutCirc(float t)
        {
            return t < 0.5f
                ? (1 - math.sqrt(1 - math.pow(2 * t, 2))) * 0.5f
                : (math.sqrt(1 - math.pow(-2 * t + 2, 2)) + 1) * 0.5f;
        }

        [BurstCompile]
        public static float InElastic(float t)
        {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return -math.sin(7.5f * PI * t) * math.pow(2, 10 * (t - 1));
        }

        [BurstCompile]
        public static float OutElastic(float t)
        {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return math.sin(-7.5f * PI * (t + 1)) * math.pow(2, -10 * t) + 1;
        }

        [BurstCompile]
        public static float InOutElastic(float t)
        {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return t < 0.5f
                ? 0.5f * math.sin(7.5f * PI * (2 * t)) * math.pow(2, 10 * (2 * t - 1))
                : 0.5f * (math.sin(-7.5f * PI * (2 * t - 1 + 1)) * math.pow(2, -10 * (2 * t - 1)) + 2);
        }

        [BurstCompile]
        public static float InBack(float t)
        {
            return t * t * t - t * math.sin(t * PI);
        }

        [BurstCompile]
        public static float OutBack(float t)
        {
            return 1 - (math.pow(1 - t, 3) - (1 - t) * math.sin((1 - t) * PI));
        }

        [BurstCompile]
        public static float InOutBack(float t)
        {
            if (t < 0.5f)
            {
                float f = 2 * t;
                return 0.5f * (f * f * f - f * math.sin(f * PI));
            }
            else
            {
                float f = 1 - (2 * t - 1);
                return 0.5f * (1 - (f * f * f - f * math.sin(f * PI))) + 0.5f;
            }
        }

        [BurstCompile]
        public static float InBounce(float t)
        {
            return 1 - Evaluate(1 - t, Ease.OutBounce);
        }

        [BurstCompile]
        public static float OutBounce(float t)
        {
            if (t < 1 / 2.75f)
            {
                return 7.5625f * t * t;
            }
            else if (t < 2 / 2.75f)
            {
                return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
            }
            else if (t < 2.5 / 2.75f)
            {
                return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
            }
            else
            {
                return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
            }
        }

        [BurstCompile]
        public static float InOutBounce(float t)
        {
            return t < 0.5f
                ? (1 - InBounce(1 - 2 * t)) * 0.5f
                : (1 + OutBounce(2 * t - 1)) * 0.5f;
        }
    }
}