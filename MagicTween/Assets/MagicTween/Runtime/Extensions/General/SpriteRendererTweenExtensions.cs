using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class SpriteRendererTweenExtensions
    {
        static readonly TweenGetter<SpriteRenderer, float4> colorGetter = self =>
        {
            var color = self.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<SpriteRenderer, float4> colorSetter = (self, x) =>
        {
            self.color = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<SpriteRenderer, float> alphaGetter = self => self.color.a;
        static readonly TweenSetter<SpriteRenderer, float> alphaSetter = (self, x) =>
        {
            var color = self.color;
            color.a = x;
            self.color = color;
        };

        public static Tween<float4, NoOptions> TweenColor(this SpriteRenderer self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this SpriteRenderer self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this SpriteRenderer self, float endValue, float duration)
        {
            return Tween.To(self, alphaGetter, alphaSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this SpriteRenderer self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, alphaSetter, startValue, endValue, duration);
        }
    }
}