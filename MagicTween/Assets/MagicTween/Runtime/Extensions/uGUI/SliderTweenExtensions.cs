using MagicTween.Core;
using UnityEngine.UI;

namespace MagicTween
{
    public static class SliderTweenExtensions
    {
        public static Tween<float, NoOptions> TweenValue(this Slider self, float endValue, float duration)
        {
            return Tween.To(self, self => self.value, (self, x) => self.value = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenValue(this Slider self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.value = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenMinValue(this Slider self, float endValue, float duration)
        {
            return Tween.To(self, self => self.minValue, (self, x) => self.minValue = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenMinValue(this Slider self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.minValue = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenMaxValue(this Slider self, float endValue, float duration)
        {
            return Tween.To(self, self => self.maxValue, (self, x) => self.maxValue = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenMaxValue(this Slider self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.maxValue = x, startValue, endValue, duration);
        }
    }
}