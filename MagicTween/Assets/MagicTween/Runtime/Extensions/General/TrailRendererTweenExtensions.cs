using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class TrailRendererTweenExtensions
    {
        static readonly TweenGetter<TrailRenderer, float4> startColorGetter = self =>
        {
            var color = self.startColor;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<TrailRenderer, float4> startColorSetter = (self, x) =>
        {
            self.startColor = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<TrailRenderer, float4> endColorGetter = self =>
        {
            var color = self.endColor;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<TrailRenderer, float4> endColorSetter = (self, x) =>
        {
            self.endColor = UnsafeUtility.As<float4, Color>(ref x);
        };

        public static Tween<float4, NoOptions> TweenStartColor(this TrailRenderer self, Color endValue, float duration)
        {
            return Tween.To(self, startColorGetter, startColorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenStartColor(this TrailRenderer self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, startColorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenEndColor(this TrailRenderer self, Color endValue, float duration)
        {
            return Tween.To(self, endColorGetter, endColorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenEndColor(this TrailRenderer self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, endColorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenStartWidth(this TrailRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.startWidth, (self, x) => self.startWidth = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenStartWidth(this TrailRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.startWidth = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEndWidth(this TrailRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.endWidth, (self, x) => self.endWidth = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEndWidth(this TrailRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.endWidth = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenTime(this TrailRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.time, (self, x) => self.time = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenTime(this TrailRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.time = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWidthMultiplier(this TrailRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.widthMultiplier, (self, x) => self.widthMultiplier = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWidthMultiplier(this TrailRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.widthMultiplier = x, startValue, endValue, duration);
        }
    }
}