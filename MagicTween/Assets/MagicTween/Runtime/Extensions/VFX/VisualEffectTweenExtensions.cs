#if MAGICTWEEN_SUPPORT_VFX
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
using MagicTween.Core;

namespace MagicTween
{
    public static class VisualEffectTweenExtensions
    {
        public static Tween<float, NoOptions> TweenFloat(this VisualEffect self, int nameID, float endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetFloat(nameID),
                (self, x) => self.SetFloat(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this VisualEffect self, int nameID, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetFloat(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this VisualEffect self, string name, float endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetFloat(name),
                (self, x) => self.SetFloat(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float, NoOptions> TweenFloat(this VisualEffect self, string name, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetFloat(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this VisualEffect self, int nameID, int endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetInt(nameID),
                (self, x) => self.SetInt(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this VisualEffect self, int nameID, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetInt(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this VisualEffect self, string name, int endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetInt(name),
                (self, x) => self.SetInt(name, x),
                endValue,
                duration
            );
        }

        public static Tween<int, IntegerTweenOptions> TweenInt(this VisualEffect self, string name, int startValue, int endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetInt(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenVector2(this VisualEffect self, int nameID, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector2(nameID),
                (self, x) => self.SetVector2(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenVector2(this VisualEffect self, int nameID, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector2(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float2, NoOptions> TweenVector2(this VisualEffect self, string name, Vector2 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector2(name),
                (self, x) => self.SetVector2(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float3, NoOptions> TweenVector3(this VisualEffect self, int nameID, Vector3 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector3(nameID),
                (self, x) => self.SetVector3(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float3, NoOptions> TweenVector3(this VisualEffect self, int nameID, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector3(nameID),
                (self, x) => self.SetVector3(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float3, NoOptions> TweenVector3(this VisualEffect self, string name, Vector3 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector3(name),
                (self, x) => self.SetVector3(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float3, NoOptions> TweenVector3(this VisualEffect self, string name, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector3(name, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector4(this VisualEffect self, int nameID, Vector4 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector4(nameID),
                (self, x) => self.SetVector4(nameID, x),
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector4(this VisualEffect self, int nameID, Vector4 startValue, Vector4 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector4(nameID, x),
                startValue,
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector4(this VisualEffect self, string name, Vector4 endValue, float duration)
        {
            return Tween.To(
                self,
                self => self.GetVector4(name),
                (self, x) => self.SetVector4(name, x),
                endValue,
                duration
            );
        }

        public static Tween<float4, NoOptions> TweenVector4(this VisualEffect self, string name, Vector4 startValue, Vector4 endValue, float duration)
        {
            return Tween.FromTo(
                self,
                (self, x) => self.SetVector4(name, x),
                startValue,
                endValue,
                duration
            );
        }
    }
}
#endif