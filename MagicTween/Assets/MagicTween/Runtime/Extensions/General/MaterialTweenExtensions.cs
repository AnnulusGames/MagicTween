using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Plugins;

namespace MagicTween
{
    public static class MaterialTweenExtensions
    {
        static readonly TweenGetter<Material, float4> colorGetter = self =>
        {
            var color = self.color;
            return UnsafeUtility.As<Color, float4>(ref color);
        };
        static readonly TweenSetter<Material, float4> colorSetter = (self, x) =>
        {
            self.color = UnsafeUtility.As<float4, Color>(ref x);
        };

        public static Tween<float4, NoOptions> TweenColor(this Material self, Color endValue, float duration)
        {
            return Tween.To(self, colorGetter, colorSetter, UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Material self, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(self, colorSetter, UnsafeUtility.As<Color, float4>(ref startValue), UnsafeUtility.As<Color, float4>(ref endValue), duration);
        }

        public static Tween<float4, NoOptions> TweenColor(this Material self, int nameID, Color endValue, float duration)
        {
            return Tween.To(
                self,
                self =>
                {
                    var color = self.GetColor(nameID);
                    return UnsafeUtility.As<Color, float4>(ref color);
                },
                (self, x) =>
                {
                    self.SetColor(nameID, self.color = UnsafeUtility.As<float4, Color>(ref x));
                },
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenColor(this Material self, int nameID, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) =>
                {
                    self.SetColor(nameID, self.color = UnsafeUtility.As<float4, Color>(ref x));
                },
                UnsafeUtility.As<Color, float4>(ref startValue),
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenColor(this Material self, string name, Color endValue, float duration)
        {
            return Tween.To(
                self,
                self =>
                {
                    var color = self.GetColor(name);
                    return UnsafeUtility.As<Color, float4>(ref color);
                },
                (self, x) =>
                {
                    self.SetColor(name, self.color = UnsafeUtility.As<float4, Color>(ref x));
                },
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenColor(this Material self, string name, Color startValue, Color endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) =>
                {
                    self.SetColor(name, self.color = UnsafeUtility.As<float4, Color>(ref x));
                },
                UnsafeUtility.As<Color, float4>(ref startValue),
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenMainTextureOffset(this Material self, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.mainTextureOffset,
                (self, x) => self.mainTextureOffset = x,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenMainTextureOffset(this Material self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.mainTextureOffset = x,
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenMainTextureScale(this Material self, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.mainTextureScale,
                (self, x) => self.mainTextureScale = x,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenMainTextureScale(this Material self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.mainTextureScale = x,
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this Material self, int nameID, float endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetFloat(nameID),
                (self, x) => self.SetFloat(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this Material self, int nameID, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetFloat(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this Material self, string name, float endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetFloat(name),
                (self, x) => self.SetFloat(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this Material self, string name, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetFloat(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this Material self, int nameID, int endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetInteger(nameID),
                (self, x) => self.SetInteger(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this Material self, int nameID, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetInteger(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this Material self, string name, int endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetInteger(name),
                (self, x) => self.SetInteger(name, x),
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this Material self, string name, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetInteger(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector(this Material self, int nameID, Vector4 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector(nameID),
                (self, x) => self.SetVector(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector(this Material self, int nameID, Vector4 startValue, Vector4 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector(this Material self, string name, Vector4 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector(name),
                (self, x) => self.SetVector(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector(this Material self, string name, Vector4 startValue, Vector4 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureOffset(this Material self, int nameID, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetTextureOffset(nameID),
                (self, x) => self.SetTextureOffset(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureOffset(this Material self, int nameID, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetTextureOffset(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureOffset(this Material self, string name, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetTextureOffset(name),
                (self, x) => self.SetTextureOffset(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureOffset(this Material self, string name, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetTextureOffset(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureScale(this Material self, int nameID, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetTextureScale(nameID),
                (self, x) => self.SetTextureScale(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureScale(this Material self, int nameID, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetTextureScale(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureScale(this Material self, string name, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetTextureScale(name),
                (self, x) => self.SetTextureScale(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenTextureScale(this Material self, string name, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetTextureScale(name, x),
                startValue,
                endValue,
                duration
            );
        }

    }
}