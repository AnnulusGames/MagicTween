using MagicTween.Core;
using UnityEngine.UIElements;

namespace MagicTween
{
    public static class ProgressBarExtensions
    {
        public static Tween<float, NoOptions> TweenValue(this AbstractProgressBar self, float endValue, float duration)
        {
            return Tween.To(self, self => self.value, (self, x) => self.value = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenValue(this AbstractProgressBar self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.value = x, startValue, endValue, duration);
        }
    }
}