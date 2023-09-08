using MagicTween.Core;
using UnityEngine.UI;

namespace MagicTween
{
    public static class ImageTweenExtensions
    {
        public static Tween<float, NoOptions> TweenFillAmount(this Image self, float endValue, float duration)
        {
            return Tween.To(self, self => self.fillAmount, (self, x) => self.fillAmount = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenFillAmount(this Image self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.fillAmount = x, startValue, endValue, duration);
        }
    }
}