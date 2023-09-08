using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class LineRendererTweenExtensions
    {
        static readonly TweenGetter<LineRenderer, float4> startColorGetter = self =>
        {
            var color = self.startColor;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<LineRenderer, float4> startColorSetter = (self, x) =>
        {
            self.startColor = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<LineRenderer, float4> endColorGetter = self =>
        {
            var color = self.endColor;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<LineRenderer, float4> endColorSetter = (self, x) =>
        {
            self.endColor = UnsafeUtility.As<float4, Color>(ref x);
        };

        public static Tween<float4, NoOptions> TweenStartColor(this LineRenderer self, Color endValue, float duration)
        {
            return Tween.To(self, startColorGetter, startColorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenStartColor(this LineRenderer self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, startColorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenEndColor(this LineRenderer self, Color endValue, float duration)
        {
            return Tween.To(self, endColorGetter, endColorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenEndColor(this LineRenderer self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, endColorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenStartWidth(this LineRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.startWidth, (self, x) => self.startWidth = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenStartWidth(this LineRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.startWidth = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEndWidth(this LineRenderer self, float endValue, float duration)
        {
            return Tween.To(self, self => self.endWidth, (self, x) => self.endWidth = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEndWidth(this LineRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.endWidth = x, startValue, endValue, duration);
        }
    }
}