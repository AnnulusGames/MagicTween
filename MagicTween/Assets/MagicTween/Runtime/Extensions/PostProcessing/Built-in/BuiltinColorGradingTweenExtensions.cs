#if MAGICTWEEN_SUPPORT_POSTPROCESSING
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class BuiltinColorGradingTweenExtensions
    {
        static readonly TweenGetter<ColorGrading, float4> colorFilterGetter = self =>
        {
            var color = self.colorFilter.value;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<ColorGrading, float4> colorFilterSetter = (self, x) =>
        {
            self.colorFilter.Override(UnsafeUtility.As<float4, Color>(ref x));
        };

        public static Tween<float, NoOptions> TweenPostExposure(this ColorGrading self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.postExposure.value, (self, x) => self.postExposure.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPostExposure(this ColorGrading self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.postExposure.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenContrast(this ColorGrading self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.contrast.value, (self, x) => self.contrast.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenContrast(this ColorGrading self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.contrast.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenHueShift(this ColorGrading self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.hueShift.value, (self, x) => self.hueShift.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenHueShift(this ColorGrading self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.hueShift.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSaturation(this ColorGrading self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.saturation.value, (self, x) => self.saturation.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSaturation(this ColorGrading self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.saturation.Override(x), startValue, endValue, duration);
        }

        public static Tween<float4, NoOptions> TweenColorFilter(this ColorGrading self, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, colorFilterGetter, colorFilterSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColorFilter(this ColorGrading self, Color startValue, Color endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, colorFilterSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }
    }
}
#endif