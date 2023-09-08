using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class ShadowTweenExtensions
    {
        static readonly TweenGetter<Shadow, float4> effectColorGetter = self =>
        {
            var color = self.effectColor;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Shadow, float4> effectColorSetter = (self, x) =>
        {
            self.effectColor = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<Shadow, float> effectColorAlphaGetter = self => self.effectColor.a;
        static readonly TweenSetter<Shadow, float> effectColorAlphaSetter = (self, x) =>
        {
            var color = self.effectColor;
            color.a = x;
            self.effectColor = color;
        };

        public static Tween<float4, NoOptions> TweenColor(this Shadow self, Color endValue, float duration)
        {
            return Tween.To(self, effectColorGetter, effectColorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Shadow self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, effectColorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this Shadow self, float endValue, float duration)
        {
            return Tween.To(self, effectColorAlphaGetter, effectColorAlphaSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this Shadow self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, effectColorAlphaSetter, startValue, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenDistance(this Shadow self, Vector2 endValue, float duration)
        {
            return Tween.To(self, self => self.effectDistance, (self, x) => self.effectDistance = x, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenDistance(this Shadow self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.effectDistance = x, startValue, endValue, duration);
        }
    }
}