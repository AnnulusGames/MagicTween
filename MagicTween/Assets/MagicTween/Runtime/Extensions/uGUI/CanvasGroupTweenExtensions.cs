using UnityEngine;
using MagicTween.Core;

namespace MagicTween
{
    public static class CanvasGroupTweenExtensions
    {
        public static Tween<float, NoOptions> TweenAlpha(this CanvasGroup self, float endValue, float duration)
        {
            return Tween.To(self, self => self.alpha, (self, x) => self.alpha = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAlpha(this CanvasGroup self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.alpha = x, startValue, endValue, duration);
        }
    }
}