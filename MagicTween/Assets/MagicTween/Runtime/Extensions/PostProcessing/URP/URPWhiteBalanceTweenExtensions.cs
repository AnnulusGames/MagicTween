#if MAGICTWEEN_SUPPORT_URP
using UnityEngine.Rendering.Universal;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class URPWhiteBalanceTweenExtensions
    {
        public static Tween<float, NoOptions> TweenTemperature(this WhiteBalance self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.temperature.value, (self, x) => self.temperature.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenTemperature(this WhiteBalance self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.temperature.Override(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenTint(this WhiteBalance self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.tint.value, (self, x) => self.tint.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenTint(this WhiteBalance self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.tint.Override(x), startValue, endValue, duration);
        }
    }
}
#endif