#if MAGICTWEEN_SUPPORT_URP
using MagicTween.Core;
using UnityEngine.Rendering;

namespace MagicTween
{
    public static class VolumeTweenExtensions
    {
        public static Tween<float, NoOptions> TweenWeight(this Volume self, float endValue, float duration)
        {
            return Tween.To(self, self => self.weight, (self, x) => self.weight = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWeight(this Volume self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.weight = x, startValue, endValue, duration);
        }
    }
}
#endif