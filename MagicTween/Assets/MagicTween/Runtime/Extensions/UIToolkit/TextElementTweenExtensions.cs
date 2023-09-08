using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements;
using MagicTween.Core;

namespace MagicTween
{
    public static class TextElementTweenExtensions
    {
        public static Tween<UnsafeText, StringTweenOptions> TweenText(this TextElement self, string endValue, float duration)
        {
            return Tween.To(() => self.text, x => self.text = x, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> TweenText(this TextElement self, string startValue, string endValue, float duration)
        {
            return Tween.FromTo(x => self.text = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenFontSize(this TextElement self, float endValue, float duration)
        {
            return Tween.To(self, self => self.resolvedStyle.fontSize, (self, x) => self.style.fontSize = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenFontSize(this TextElement self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.style.fontSize = x, startValue, endValue, duration);
        }
    }
}