using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class TextTweenExtensions
    {
        public static Tween<int, IntegerTweenOptions> TweenFontSize(this Text self, int endValue, float duration)
        {
            return Tween.To(self, self => self.fontSize, (self, x) => self.fontSize = x, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenFontSize(this Text self, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.fontSize = x, startValue, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> TweenText(this Text self, string endValue, float duration)
        {
            return Tween.To(() => self.text, x => self.text = x, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> TweenText(this Text self, string startValue, string endValue, float duration)
        {
            return Tween.FromTo(x => self.text = x, startValue, endValue, duration);
        }
    }
}
