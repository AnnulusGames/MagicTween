using UnityEngine;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Plugins;
#if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
using MagicTween.Core.Transforms;
#endif

namespace MagicTween
{
    public static class TransformTweenExtensions
    {
#if MAGICTWEEN_DISABLE_TRANSFORM_JOBS
        static readonly TweenGetter<Transform, float3> positionGetter = self => self.position;
        static readonly TweenSetter<Transform, float3> positionSetter = (self, x) => self.position = x;
        static readonly TweenGetter<Transform, float> positionXGetter = self => self.position.x;
        static readonly TweenGetter<Transform, float> positionYGetter = self => self.position.y;
        static readonly TweenGetter<Transform, float> positionZGetter = self => self.position.z;
        static readonly TweenSetter<Transform, float> positionXSetter = (self, x) =>
        {
            var p = self.position;
            p.x = x;
            self.position = p;
        };
        static readonly TweenSetter<Transform, float> positionYSetter = (self, y) =>
        {
            var p = self.position;
            p.y = y;
            self.position = p;
        };
        static readonly TweenSetter<Transform, float> positionZSetter = (self, z) =>
        {
            var p = self.position;
            p.z = z;
            self.position = p;
        };
        static readonly TweenGetter<Transform, float3> localPositionGetter = self => self.localPosition;
        static readonly TweenSetter<Transform, float3> localPositionSetter = (self, x) => self.localPosition = x;
        static readonly TweenGetter<Transform, float> localPositionXGetter = self => self.localPosition.x;
        static readonly TweenGetter<Transform, float> localPositionYGetter = self => self.localPosition.y;
        static readonly TweenGetter<Transform, float> localPositionZGetter = self => self.localPosition.z;
        static readonly TweenSetter<Transform, float> localPositionXSetter = (self, x) =>
        {
            var p = self.localPosition;
            p.x = x;
            self.localPosition = p;
        };
        static readonly TweenSetter<Transform, float> localPositionYSetter = (self, y) =>
        {
            var p = self.localPosition;
            p.y = y;
            self.localPosition = p;
        };
        static readonly TweenSetter<Transform, float> localPositionZSetter = (self, z) =>
        {
            var p = self.localPosition;
            p.z = z;
            self.localPosition = p;
        };
        static readonly TweenGetter<Transform, quaternion> rotationGetter = self => self.rotation;
        static readonly TweenSetter<Transform, quaternion> rotationSetter = (self, x) => self.rotation = x;
        static readonly TweenGetter<Transform, quaternion> localRotationGetter = self => self.localRotation;
        static readonly TweenSetter<Transform, quaternion> localRotationSetter = (self, x) => self.localRotation = x;
        static readonly TweenGetter<Transform, float3> eulerAnglesGetter = self => self.eulerAngles;
        static readonly TweenSetter<Transform, float3> eulerAnglesSetter = (self, x) => self.eulerAngles = x;
        static readonly TweenGetter<Transform, float> eulerAnglesXGetter = self => self.eulerAngles.x;
        static readonly TweenGetter<Transform, float> eulerAnglesYGetter = self => self.eulerAngles.y;
        static readonly TweenGetter<Transform, float> eulerAnglesZGetter = self => self.eulerAngles.z;
        static readonly TweenSetter<Transform, float> eulerAnglesXSetter = (self, x) =>
        {
            var p = self.eulerAngles;
            p.x = x;
            self.eulerAngles = p;
        };
        static readonly TweenSetter<Transform, float> eulerAnglesYSetter = (self, y) =>
        {
            var p = self.eulerAngles;
            p.y = y;
            self.eulerAngles = p;
        };
        static readonly TweenSetter<Transform, float> eulerAnglesZSetter = (self, z) =>
        {
            var p = self.eulerAngles;
            p.z = z;
            self.eulerAngles = p;
        };
        static readonly TweenGetter<Transform, float3> localEulerAnglesGetter = self => self.localEulerAngles;
        static readonly TweenSetter<Transform, float3> localEulerAnglesSetter = (self, x) => self.localEulerAngles = x;
        static readonly TweenGetter<Transform, float> localEulerAnglesXGetter = self => self.localEulerAngles.x;
        static readonly TweenGetter<Transform, float> localEulerAnglesYGetter = self => self.localEulerAngles.y;
        static readonly TweenGetter<Transform, float> localEulerAnglesZGetter = self => self.localEulerAngles.z;
        static readonly TweenSetter<Transform, float> localEulerAnglesXSetter = (self, x) =>
        {
            var p = self.localEulerAngles;
            p.x = x;
            self.localEulerAngles = p;
        };
        static readonly TweenSetter<Transform, float> localEulerAnglesYSetter = (self, y) =>
        {
            var p = self.localEulerAngles;
            p.y = y;
            self.localEulerAngles = p;
        };
        static readonly TweenSetter<Transform, float> localEulerAnglesZSetter = (self, z) =>
        {
            var p = self.localEulerAngles;
            p.z = z;
            self.localEulerAngles = p;
        };
        static readonly TweenGetter<Transform, float3> localScaleGetter = self => self.localScale;
        static readonly TweenSetter<Transform, float3> localScaleSetter = (self, x) => self.localScale = x;
        static readonly TweenGetter<Transform, float> localScaleXGetter = self => self.localScale.x;
        static readonly TweenGetter<Transform, float> localScaleYGetter = self => self.localScale.y;
        static readonly TweenGetter<Transform, float> localScaleZGetter = self => self.localScale.z;
        static readonly TweenSetter<Transform, float> localScaleXSetter = (self, x) =>
        {
            var p = self.localScale;
            p.x = x;
            self.localScale = p;
        };
        static readonly TweenSetter<Transform, float> localScaleYSetter = (self, y) =>
        {
            var p = self.localScale;
            p.y = y;
            self.localScale = p;
        };
        static readonly TweenSetter<Transform, float> localScaleZSetter = (self, z) =>
        {
            var p = self.localScale;
            p.z = z;
            self.localScale = p;
        };

        public static Tween<float3, NoOptions> TweenPosition(this Transform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, positionGetter, positionSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenPosition(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, positionSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, positionXGetter, positionXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, positionYGetter, positionYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, positionZGetter, positionZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionZSetter, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalPosition(this Transform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, localPositionGetter, localPositionSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalPosition(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, localPositionSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionX(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localPositionXGetter, localPositionXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionX(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localPositionXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionY(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localPositionYGetter, localPositionYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionY(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localPositionYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionZ(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localPositionZGetter, localPositionZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionZ(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localPositionZSetter, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Transform self, Quaternion endValue, float duration)
        {
            return Tween.To(self, rotationGetter, rotationSetter, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Transform self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return Tween.FromTo(self, rotationSetter, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenLocalRotation(this Transform self, Quaternion endValue, float duration)
        {
            return Tween.To(self, localRotationGetter, localRotationSetter, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenLocalRotation(this Transform self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return Tween.FromTo(self, localRotationSetter, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this Transform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, eulerAnglesGetter, eulerAnglesSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, eulerAnglesSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesX(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, eulerAnglesXGetter, eulerAnglesXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesX(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, eulerAnglesXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesY(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, eulerAnglesYGetter, eulerAnglesYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesY(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, eulerAnglesYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesZ(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, eulerAnglesZGetter, eulerAnglesZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesZ(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, eulerAnglesZSetter, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalEulerAngles(this Transform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, localEulerAnglesGetter, localEulerAnglesSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalEulerAngles(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, localEulerAnglesSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesX(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localEulerAnglesXGetter, localEulerAnglesXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesX(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localEulerAnglesXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesY(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localEulerAnglesYGetter, localEulerAnglesYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesY(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localEulerAnglesYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesZ(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localEulerAnglesZGetter, localEulerAnglesZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesZ(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localEulerAnglesZSetter, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalScale(this Transform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, localScaleGetter, localScaleSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalScale(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, localScaleSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleX(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localScaleXGetter, localScaleXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleX(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localScaleXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleY(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localScaleYGetter, localScaleYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleY(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localScaleYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleZ(this Transform self, float endValue, float duration)
        {
            return Tween.To(self, localScaleZGetter, localScaleZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleZ(this Transform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, localScaleZSetter, startValue, endValue, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchPosition(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Punch(self, positionGetter, positionSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, positionXGetter, positionXSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionY(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, positionYGetter, positionYSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionZ(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, positionZGetter, positionZSetter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalPosition(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Punch(self, localPositionGetter, localPositionSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localPositionXGetter, localPositionXSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionY(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localPositionYGetter, localPositionYSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionZ(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localPositionZGetter, localPositionZSetter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Punch(self, eulerAnglesGetter, eulerAnglesSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, eulerAnglesXGetter, eulerAnglesXSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesY(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, eulerAnglesYGetter, eulerAnglesYSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, eulerAnglesZGetter, eulerAnglesZSetter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Punch(self, localEulerAnglesGetter, localEulerAnglesSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localEulerAnglesXGetter, localEulerAnglesXSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesY(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localEulerAnglesYGetter, localEulerAnglesYSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localEulerAnglesZGetter, localEulerAnglesZSetter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalScale(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Punch(self, localScaleGetter, localScaleSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localScaleXGetter, localScaleXSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleY(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localScaleYGetter, localScaleYSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleZ(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localScaleZGetter, localScaleZSetter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakePosition(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Shake(self, positionGetter, positionSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionX(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, positionXGetter, positionXSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionY(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, positionYGetter, positionYSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionZ(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, positionZGetter, positionZSetter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalPosition(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Shake(self, localPositionGetter, localPositionSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionX(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localPositionXGetter, localPositionXSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionY(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localPositionYGetter, localPositionYSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionZ(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localPositionZGetter, localPositionZSetter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Shake(self, eulerAnglesGetter, eulerAnglesSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesX(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, eulerAnglesXGetter, eulerAnglesXSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesY(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, eulerAnglesYGetter, eulerAnglesYSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, eulerAnglesZGetter, eulerAnglesZSetter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Shake(self, localEulerAnglesGetter, localEulerAnglesSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesX(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localEulerAnglesXGetter, localEulerAnglesXSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesY(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localEulerAnglesYGetter, localEulerAnglesYSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localEulerAnglesZGetter, localEulerAnglesZSetter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalScale(this Transform self, Vector3 strength, float duration)
        {
            return Tween.Shake(self, localScaleGetter, localScaleSetter, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> ShakeLocalScaleX(this Transform self, float strength, float duration)
        {
            return Tween.Punch(self, localScaleXGetter, localScaleXSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalScaleY(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localScaleYGetter, localScaleYSetter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalScaleZ(this Transform self, float strength, float duration)
        {
            return Tween.Shake(self, localScaleZGetter, localScaleZSetter, strength, duration);
        }

#else

        public static Tween<float3, NoOptions> TweenPosition(this Transform self, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float3, NoOptions, Float3TweenPlugin, TransformPositionTranslator>(self, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenPosition(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TransformPositionTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformPositionXTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformPositionXTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformPositionYTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformPositionYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformPositionZTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformPositionZTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalPosition(this Transform self, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float3, NoOptions, Float3TweenPlugin, TransformLocalPositionTranslator>(self, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalPosition(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TransformLocalPositionTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionX(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionXTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionX(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionXTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionY(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionYTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionY(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionZ(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionZTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalPositionZ(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalPositionZTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Transform self, Quaternion endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<quaternion, NoOptions, QuaternionTweenPlugin, TransformRotationTranslator>(self, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Transform self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<quaternion, NoOptions, QuaternionTweenPlugin, TransformRotationTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenLocalRotation(this Transform self, Quaternion endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<quaternion, NoOptions, QuaternionTweenPlugin, TransformLocalRotationTranslator>(self, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenLocalRotation(this Transform self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<quaternion, NoOptions, QuaternionTweenPlugin, TransformLocalRotationTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this Transform self, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float3, NoOptions, Float3TweenPlugin, TransformEulerAnglesTranslator>(self, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenEulerAngles(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TransformEulerAnglesTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesX(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesXTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesX(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesXTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesY(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesYTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesY(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesZ(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesZTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenEulerAnglesZ(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalEulerAngles(this Transform self, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float3, NoOptions, Float3TweenPlugin, TransformLocalEulerAnglesTranslator>(self, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalEulerAngles(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TransformLocalEulerAnglesTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesX(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesXTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesX(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesXTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesY(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesYTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesY(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesZ(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesZTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalEulerAnglesZ(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesZTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalScale(this Transform self, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float3, NoOptions, Float3TweenPlugin, TransformLocalScaleTranslator>(self, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenLocalScale(this Transform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TransformLocalScaleTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleX(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleXTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleX(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleXTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleY(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleYTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleY(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleYTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleZ(this Transform self, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleZTranslator>(self, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenLocalScaleZ(this Transform self, float startValue, float endValue, float duration)
        {
            return TweenFactory.Transforms.CreateFromTo<float, NoOptions, FloatTweenPlugin, TransformLocalScaleZTranslator>(self, startValue, endValue, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchPosition(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float3, Punch3TweenPlugin, TransformPositionTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformPositionXTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformPositionYTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchPositionZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformPositionZTranslator>(self, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalPosition(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float3, Punch3TweenPlugin, TransformLocalPositionTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalPositionXTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalPositionYTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalPositionZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalPositionZTranslator>(self, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float3, Punch3TweenPlugin, TransformEulerAnglesTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformEulerAnglesXTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformEulerAnglesYTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformEulerAnglesZTranslator>(self, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float3, Punch3TweenPlugin, TransformLocalEulerAnglesTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalEulerAnglesXTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalEulerAnglesYTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalEulerAnglesZTranslator>(self, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> PunchLocalScale(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float3, Punch3TweenPlugin, TransformLocalScaleTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalScaleXTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalScaleYTranslator>(self, strength, duration);
        }

        public static Tween<float, PunchTweenOptions> PunchLocalScaleZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreatePunch<float, PunchTweenPlugin, TransformLocalScaleZTranslator>(self, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakePosition(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float3, Shake3TweenPlugin, TransformPositionTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformPositionXTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformPositionYTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakePositionZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformPositionZTranslator>(self, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalPosition(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float3, Shake3TweenPlugin, TransformLocalPositionTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalPositionXTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalPositionYTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalPositionZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalPositionZTranslator>(self, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float3, Shake3TweenPlugin, TransformEulerAnglesTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformEulerAnglesXTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformEulerAnglesYTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformEulerAnglesZTranslator>(self, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalEulerAngles(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float3, Shake3TweenPlugin, TransformLocalEulerAnglesTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalEulerAnglesXTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalEulerAnglesYTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalEulerAnglesZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalEulerAnglesZTranslator>(self, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> ShakeLocalScale(this Transform self, Vector3 strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float3, Shake3TweenPlugin, TransformLocalScaleTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalScaleX(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalScaleXTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalScaleY(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalScaleYTranslator>(self, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> ShakeLocalScaleZ(this Transform self, float strength, float duration)
        {
            return TweenFactory.Transforms.CreateShake<float, ShakeTweenPlugin, TransformLocalScaleZTranslator>(self, strength, duration);
        }

#endif

        public static Tween<float3, PathTweenOptions> TweenPath(this Transform self, float3[] points, float duration)
        {
            return Tween.Path(() => self.position, x => self.position = x, points, duration);
        }

        public static Tween<float3, PathTweenOptions> TweenPath(this Transform self, Vector3[] points, float duration)
        {
            return Tween.Path(() => self.position, x => self.position = x, points, duration);
        }

        public static Tween<float3, PathTweenOptions> TweenLocalPath(this Transform self, float3[] points, float duration)
        {
            return Tween.Path(() => self.localPosition, x => self.localPosition = x, points, duration);
        }

        public static Tween<float3, PathTweenOptions> TweenLocalPath(this Transform self, Vector3[] points, float duration)
        {
            return Tween.Path(() => self.localPosition, x => self.localPosition = x, points, duration);
        }
    }
}
