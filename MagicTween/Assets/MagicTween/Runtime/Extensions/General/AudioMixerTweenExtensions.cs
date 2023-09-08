using MagicTween.Core;
using UnityEngine.Audio;

namespace MagicTween
{
    public static class AudioMixerTweenExtensions
    {
        public static Tween<float, NoOptions> TweenFloat(this AudioMixer self, string name, float endValue, float duration)
        {
            return Tween.To(
                self,
                self =>
                {
                    self.GetFloat(name, out var x);
                    return x;
                },
                (self, x) =>
                {
                    self.SetFloat(name, x);
                },
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this AudioMixer self, string name, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) =>
                {
                    self.SetFloat(name, x);
                },
                startValue,
                endValue,
                duration
            );
        }
    }
}