using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class GraphicTweenExtensions
    {
        static readonly TweenGetter<Graphic, float4> colorGetter = self =>
        {
            var color = self.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Graphic, float4> colorSetter = (self, x) =>
        {
            self.color = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<Graphic, float> colorAlphaGetter = self => self.color.a;
        static readonly TweenSetter<Graphic, float> colorAlphaSetter = (self, x) =>
        {
            var color = self.color;
            color.a = x;
            self.color = color;
        };

        public static Tween<float4, NoOptions> TweenColor(this Graphic self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Graphic self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this Graphic self, float endValue, float duration)
        {
            return Tween.To(self, colorAlphaGetter, colorAlphaSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this Graphic self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, colorAlphaSetter, startValue, endValue, duration);
        }
    }
}