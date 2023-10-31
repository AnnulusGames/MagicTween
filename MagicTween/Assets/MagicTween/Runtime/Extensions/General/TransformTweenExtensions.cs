using UnityEngine;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Plugins;
using MagicTween.Core.Transforms;

namespace MagicTween
{
    public static class TransformTweenExtensions
    {
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
