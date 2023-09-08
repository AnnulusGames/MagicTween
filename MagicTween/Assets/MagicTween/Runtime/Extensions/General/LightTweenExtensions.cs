using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using MagicTween.Core;

namespace MagicTween
{
    public static class LightTweenExtensions
    {
        static readonly TweenGetter<Light, float4> colorGetter = self =>
        {
            var color = self.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Light, float4> colorSetter = (self, x) =>
        {
            self.color = UnsafeUtility.As<float4, Color>(ref x);
        };

        public static Tween<float4, NoOptions> TweenColor(this Light self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }
        public static Tween<float4, NoOptions> TweenColor(this Light self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this Light self, float endValue, float duration)
        {
            return Tween.To(self, self => self.intensity, (self, x) => self.intensity = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this Light self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.intensity = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenShadowStrength(this Light self, float endValue, float duration)
        {
            return Tween.To(self, self => self.shadowStrength, (self, x) => self.shadowStrength = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenShadowStrength(this Light self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.shadowStrength = x, startValue, endValue, duration);
        }
    }
}