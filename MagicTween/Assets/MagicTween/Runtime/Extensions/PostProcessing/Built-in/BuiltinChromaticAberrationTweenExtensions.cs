#if MAGICTWEEN_SUPPORT_POSTPROCESSING
using UnityEngine.Rendering.PostProcessing;
using MagicTween.Diagnostics;
using MagicTween.Core;

namespace MagicTween
{
    public static class BuiltinChromaticAberrationTweenExtensions
    {
        public static Tween<float, NoOptions> TweenIntensity(this ChromaticAberration self, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.To(self, self => self.intensity.value, (self, x) => self.intensity.Override(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenIntensity(this ChromaticAberration self, float startValue, float endValue, float duration)
        {
            Debugger.LogWarningIfPostProcessingInactive(self);
            return Tween.FromTo(self, (self, x) => self.intensity.Override(x), startValue, endValue, duration);
        }
    }
}
#endif