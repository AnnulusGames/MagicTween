#if MAGICTWEEN_SUPPORT_URP
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class URPVignetteTweenExtensions
    {
        static readonly TweenGetter<Vignette, float4> colorGetter = self =>
        {
            var color = self.color.value;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Vignette, float4> colorSetter = (self, x) =>
        {
            self.color.Override(UnsafeUtility.As<float4, Color>(ref x));
        };

        public static Tween<float, NoOptions> TweenIntensity(this Vignette self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensity.value, (self, x) => self.intensity.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this Vignette self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensity.Override(x), startValue, endValue, duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Vignette self, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Vignette self, Color startValue, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }
    }
}
#endif