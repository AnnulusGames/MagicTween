#if MAGICTWEEN_SUPPORT_URP
using UnityEngine.Rendering.Universal;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class URPLensDistortionTweenExtensions
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

        public static Tween<float, NoOptions> TweenXMultiplier(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.xMultiplier.value, (self, x) => self.xMultiplier.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenXMultiplier(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.xMultiplier.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenYMultiplier(this LensDistortion self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.yMultiplier.value, (self, x) => self.yMultiplier.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenYMultiplier(this LensDistortion self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.yMultiplier.Override(x), startValue, endValue, duration);
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