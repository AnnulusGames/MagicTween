#if MAGICTWEEN_SUPPORT_URP
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class URPBloomTweenExtensions
    {
        static readonly TweenGetter<Bloom, float4> tintGetter = self =>
        {
            var color = self.tint.value;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Bloom, float4> tintSetter = (self, x) =>
        {
            self.tint.Override(UnsafeUtility.As<float4, Color>(ref x));
        };

        public static Tween<float, NoOptions> TweenIntensity(this Bloom self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensity.value, (self, x) => self.intensity.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this Bloom self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensity.Override(x), startValue, endValue, duration);
        }

        public static Tween<float4, NoOptions> TweenTint(this Bloom self, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, tintGetter, tintSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenTint(this Bloom self, Color startValue, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, tintSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }
    }
}
#endif