using UnityEngine;
using MagicTween.Core;

namespace MagicTween
{
    public static class AudioSourceTweenExtensions
    {
        public static Tween<float, NoOptions> TweenVolume(this AudioSource self, float endValue, float duration)
        {
            return Tween.To(self, self => self.volume, (self, x) => self.volume = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenVolume(this AudioSource self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.volume = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPitch(this AudioSource self, float endValue, float duration)
        {
            return Tween.To(self, self => self.pitch, (self, x) => self.pitch = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPitch(this AudioSource self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.pitch = x, startValue, endValue, duration);
        }
    }
}