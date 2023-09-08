using MagicTween.Core;
using UnityEngine.UIElements;

namespace MagicTween
{
    public static class UIToolkitSliderTweenExtensions
    {
        public static Tween<float, NoOptions> TweenValue(this Slider self, float endValue, float duration)
        {
            return Tween.To(self, self => self.value, (self, x) => self.value = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenValue(this Slider self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.value = x, startValue, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenValue(this SliderInt self, int endValue, float duration)
        {
            return Tween.To(self, self => self.value, (self, x) => self.value = x, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenValue(this SliderInt self, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.value = x, startValue, endValue, duration);
        }
    }
}