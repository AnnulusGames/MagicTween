using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Core.Systems;
using MagicTween.Plugins;
using TMPro;

namespace MagicTween
{
    public static class TextMeshProTweenExtensions
    {
        static readonly TweenGetter<TMP_Text, float4> colorGetter = self =>
        {
            var color = self.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<TMP_Text, float4> colorSetter = (self, x) =>
        {
            self.color = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<TMP_Text, float> colorAlphaGetter = self => self.color.a;
        static readonly TweenSetter<TMP_Text, float> colorAlphaSetter = (self, x) =>
        {
            var color = self.color;
            color.a = x;
            self.color = color;
        };

        public static Tween<float4, NoOptions> TweenColor(this TMP_Text self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this TMP_Text self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenAlpha(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, colorAlphaGetter, colorAlphaSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAlpha(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, colorAlphaSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenFontSize(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, self => self.fontSize, (self, x) => self.fontSize = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenFontSize(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.fontSize = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenParagraphSpacing(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, self => self.paragraphSpacing, (self, x) => self.paragraphSpacing = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenParagraphSpacing(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.paragraphSpacing = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLineSpacing(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, self => self.lineSpacing, (self, x) => self.lineSpacing = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLineSpacing(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.lineSpacing = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWordSpacing(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, self => self.wordSpacing, (self, x) => self.wordSpacing = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWordSpacing(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.wordSpacing = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenCharacterSpacing(this TMP_Text self, float endValue, float duration)
        {
            return Tween.To(self, self => self.characterSpacing, (self, x) => self.characterSpacing = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenCharacterSpacing(this TMP_Text self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.characterSpacing = x, startValue, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleCharacters(this TMP_Text self, int endValue, float duration)
        {
            return Tween.To(self, self => self.maxVisibleCharacters, (self, x) => self.maxVisibleCharacters = x, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleCharacters(this TMP_Text self, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.maxVisibleCharacters = x, startValue, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleLines(this TMP_Text self, int endValue, float duration)
        {
            return Tween.To(self, self => self.maxVisibleLines, (self, x) => self.maxVisibleLines = x, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleLines(this TMP_Text self, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.maxVisibleLines = x, startValue, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleWords(this TMP_Text self, int endValue, float duration)
        {
            return Tween.To(self, self => self.maxVisibleWords, (self, x) => self.maxVisibleWords = x, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> TweenMaxVisibleWords(this TMP_Text self, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.maxVisibleWords = x, startValue, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> TweenText(this TMP_Text self, string endValue, float duration)
        {
            return Tween.To(() => self.text, x => self.text = x, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> TweenText(this TMP_Text self, string startValue, string endValue, float duration)
        {
            return Tween.FromTo(x => self.text = x, startValue, endValue, duration);
        }

        static TMPTweenAnimatorUpdateSystem system;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            system = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TMPTweenAnimator GetTMPTweenAnimator(this TMP_Text self)
        {
            if (system == null) system = TweenWorld.World.GetOrCreateSystemManaged<TMPTweenAnimatorUpdateSystem>();
            return system.GetAnimator(self);
        }

        public static Tween<float4, NoOptions> TweenCharColor(this TMP_Text self, int index, Color endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharColor(index, endValue, duration);
        }

        public static Tween<float4, NoOptions> TweenCharColor(this TMP_Text self, int index, Color startValue, Color endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharColor(index, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenCharColorAlpha(this TMP_Text self, int index, float endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharColorAlpha(index, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenCharColorAlpha(this TMP_Text self, int index, float startValue, float endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharColorAlpha(index, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharOffset(this TMP_Text self, int index, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharOffset(index, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharOffset(this TMP_Text self, int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharOffset(index, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharEulerAngles(this TMP_Text self, int index, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharEulerAngles(index, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharEulerAngles(this TMP_Text self, int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharEulerAngles(index, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenCharRotation(this TMP_Text self, int index, Quaternion endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharRotation(index, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenCharRotation(this TMP_Text self, int index, Quaternion startValue, Quaternion endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharRotation(index, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharScale(this TMP_Text self, int index, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharScale(index, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenCharScale(this TMP_Text self, int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            return GetTMPTweenAnimator(self).TweenCharScale(index, startValue, endValue, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchCharOffset(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).PunchCharOffset(index, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchCharEulerAngles(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).PunchCharEulerAngles(index, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchCharScale(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).PunchCharScale(index, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeCharOffset(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).ShakeCharOffset(index, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeCharEulerAngles(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).ShakeCharEulerAngles(index, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeCharScale(this TMP_Text self, int index, Vector3 strength, float duration)
        {
            return GetTMPTweenAnimator(self).ShakeCharScale(index, strength, duration);
        }

        public static void ResetCharTweens(this TMP_Text self)
        {
            GetTMPTweenAnimator(self).Reset();
        }

        public static int GetCharCount(this TMP_Text self)
        {
            return GetTMPTweenAnimator(self).GetCharCount();
        }
    }
}