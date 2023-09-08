#if MAGICTWEEN_SUPPORT_POSTPROCESSING
using UnityEngine.Rendering.PostProcessing;
using MagicTween.Core;

namespace MagicTween
{
    public static class PostProcessVolumeTweenExtensions
    {
        public static Tween<float, NoOptions> TweenWeight(this PostProcessVolume self, float endValue, float duration)
        {
            return Tween.To(self, self => self.weight, (self, x) => self.weight = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWeight(this PostProcessVolume self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.weight = x, startValue, endValue, duration);
        }
    }
}
#endif