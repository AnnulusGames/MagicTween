using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;

namespace MagicTween
{
    public static class VisualElementTweenExtensions
    {
        static readonly TweenGetter<VisualElement, float4> colorGetter = self =>
        {
            var color = self.resolvedStyle.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<VisualElement, float4> colorSetter = (self, x) =>
        {
            self.style.color = UnsafeUtility.As<float4, Color>(ref x);
        };
        static readonly TweenGetter<VisualElement, float> colorAlphaGetter = self => self.resolvedStyle.color.a;
        static readonly TweenSetter<VisualElement, float> colorAlphaSetter = (self, x) =>
        {
            var color = self.resolvedStyle.color;
            color.a = x;
            self.style.color = color;
        };

        public static Tween<float3, NoOptions> TweenPosition(this VisualElement self, Vector3 endValue, float duration)
        {
            return Tween.To(self, self => self.transform.position, (self, x) => self.transform.position = x, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenPosition(this VisualElement self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.transform.position = x, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this VisualElement self, Quaternion endValue, float duration)
        {
            return Tween.To(self, self => self.transform.rotation, (self, x) => self.transform.rotation = x, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this VisualElement self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.transform.rotation = x, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this VisualElement self, Vector3 endValue, float duration)
        {
            return Tween.To(self, self => self.transform.rotation.eulerAngles, (self, x) => self.transform.rotation = Quaternion.Euler(x), endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this VisualElement self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.transform.rotation = Quaternion.Euler(x), startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWidth(this VisualElement self, float endValue, float duration)
        {
            return Tween.To(self, self => self.style.width.value.value, (self, x) => self.style.width = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenWidth(this VisualElement self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.style.width = x, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenHeight(this VisualElement self, float endValue, float duration)
        {
            return Tween.To(self, self => self.resolvedStyle.height, (self, x) => self.style.height = x, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenHeight(this VisualElement self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.style.height = x, startValue, endValue, duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this VisualElement self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this VisualElement self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this VisualElement self, float endValue, float duration)
        {
            return Tween.To(self, colorAlphaGetter, colorAlphaSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenColorAlpha(this VisualElement self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, colorAlphaSetter, startValue, endValue, duration);
        }
    }
}