#if MAGICTWEEN_SUPPORT_POSTPROCESSING
using UnityEngine.Rendering.PostProcessing;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class BuiltinLensDistortionTweenExtensions
    {
        public static Tween<float, NoOptions> TweenIntensity(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensity.value, (self, x) => self.intensity.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensity.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensityX(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensityX.value, (self, x) => self.intensityX.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensityX(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensityX.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensityY(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensityY.value, (self, x) => self.intensityY.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensityY(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensityY.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenScale(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.scale.value, (self, x) => self.scale.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenScale(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.scale.Override(x), startValue, endValue, duration);
        }
    }
}
#endif