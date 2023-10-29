using System;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using Unity.Assertions;
using MagicTween.Plugins;

namespace MagicTween
{
    partial struct Tween
    {
        public static Tween<float, NoOptions> To(TweenGetter<float> getter, TweenSetter<float> setter, in float endValue, float duration)
        {
            return TweenFactory.CreateToTween<float, NoOptions, FloatTweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<float2, NoOptions> To(TweenGetter<float2> getter, TweenSetter<float2> setter, in float2 endValue, float duration)
        {
            return TweenFactory.CreateToTween<float2, NoOptions, Float2TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<float3, NoOptions> To(TweenGetter<float3> getter, TweenSetter<float3> setter, in float3 endValue, float duration)
        {
            return TweenFactory.CreateToTween<float3, NoOptions, Float3TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<float4, NoOptions> To(TweenGetter<float4> getter, TweenSetter<float4> setter, in float4 endValue, float duration)
        {
            return TweenFactory.CreateToTween<float4, NoOptions, Float4TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<double, NoOptions> To(TweenGetter<double> getter, TweenSetter<double> setter, in double endValue, float duration)
        {
            return TweenFactory.CreateToTween<double, NoOptions, DoubleTweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<double2, NoOptions> To(TweenGetter<double2> getter, TweenSetter<double2> setter, in double2 endValue, float duration)
        {
            return TweenFactory.CreateToTween<double2, NoOptions, Double2TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<double3, NoOptions> To(TweenGetter<double3> getter, TweenSetter<double3> setter, in double3 endValue, float duration)
        {
            return TweenFactory.CreateToTween<double3, NoOptions, Double3TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<double4, NoOptions> To(TweenGetter<double4> getter, TweenSetter<double4> setter, in double4 endValue, float duration)
        {
            return TweenFactory.CreateToTween<double4, NoOptions, Double4TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> To(TweenGetter<int> getter, TweenSetter<int> setter, in int endValue, float duration)
        {
            return TweenFactory.CreateToTween<int, IntegerTweenOptions, IntTweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<int2, IntegerTweenOptions> To(TweenGetter<int2> getter, TweenSetter<int2> setter, in int2 endValue, float duration)
        {
            return TweenFactory.CreateToTween<int2, IntegerTweenOptions, Int2TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<int3, IntegerTweenOptions> To(TweenGetter<int3> getter, TweenSetter<int3> setter, in int3 endValue, float duration)
        {
            return TweenFactory.CreateToTween<int3, IntegerTweenOptions, Int3TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<int4, IntegerTweenOptions> To(TweenGetter<int4> getter, TweenSetter<int4> setter, in int4 endValue, float duration)
        {
            return TweenFactory.CreateToTween<int4, IntegerTweenOptions, Int4TweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<long, IntegerTweenOptions> To(TweenGetter<long> getter, TweenSetter<long> setter, in long endValue, float duration)
        {
            return TweenFactory.CreateToTween<long, IntegerTweenOptions, LongTweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> To(TweenGetter<quaternion> getter, TweenSetter<quaternion> setter, in quaternion endValue, float duration)
        {
            return TweenFactory.CreateToTween<quaternion, NoOptions, QuaternionTweenPlugin>(getter, setter, endValue, duration);
        }

        public static Tween<UnsafeText, StringTweenOptions> To(TweenGetter<string> getter, TweenSetter<string> setter, string endValue, float duration)
        {
            return TweenFactory.CreateStringToTween(getter, setter, endValue, duration);
        }

        public static Tween<float, NoOptions> FromTo(TweenSetter<float> setter, in float startValue, in float endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<float, NoOptions, FloatTweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<float2, NoOptions> FromTo(TweenSetter<float2> setter, in float2 startValue, in float2 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<float2, NoOptions, Float2TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<float3, NoOptions> FromTo(TweenSetter<float3> setter, in float3 startValue, in float3 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<float3, NoOptions, Float3TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<float4, NoOptions> FromTo(TweenSetter<float4> setter, in float4 startValue, in float4 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<float4, NoOptions, Float4TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<double, NoOptions> FromTo(TweenSetter<double> setter, in double startValue, in double endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<double, NoOptions, DoubleTweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<double2, NoOptions> FromTo(TweenSetter<double2> setter, in double2 startValue, in double2 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<double2, NoOptions, Double2TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<double3, NoOptions> FromTo(TweenSetter<double3> setter, in double3 startValue, in double3 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<double3, NoOptions, Double3TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<double4, NoOptions> FromTo(TweenSetter<double4> setter, in double4 startValue, in double4 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<double4, NoOptions, Double4TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<int, IntegerTweenOptions> FromTo(TweenSetter<int> setter, in int startValue, in int endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<int, IntegerTweenOptions, IntTweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<int2, IntegerTweenOptions> FromTo(TweenSetter<int2> setter, in int2 startValue, in int2 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<int2, IntegerTweenOptions, Int2TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<int3, IntegerTweenOptions> FromTo(TweenSetter<int3> setter, in int3 startValue, in int3 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<int3, IntegerTweenOptions, Int3TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<int4, IntegerTweenOptions> FromTo(TweenSetter<int4> setter, in int4 startValue, in int4 endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<int4, IntegerTweenOptions, Int4TweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<long, IntegerTweenOptions> FromTo(TweenSetter<long> setter, in long startValue, in long endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<long, IntegerTweenOptions, LongTweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<quaternion, NoOptions> FromTo(TweenSetter<quaternion> setter, in quaternion startValue, in quaternion endValue, float duration)
        {
            return TweenFactory.CreateFromToTween<quaternion, NoOptions, QuaternionTweenPlugin>(startValue, endValue, duration, setter);
        }

        public static Tween<UnsafeText, StringTweenOptions> FromTo(TweenSetter<string> setter, string startValue, string endValue, float duration)
        {
            return TweenFactory.CreateStringFromToTween(setter, startValue, endValue, duration);
        }

        public static Tween<float, PunchTweenOptions> Punch(TweenGetter<float> getter, TweenSetter<float> setter, in float strength, float duration)
        {
            return TweenFactory.CreatePunchTween<float, PunchTweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float2, PunchTweenOptions> Punch(TweenGetter<float2> getter, TweenSetter<float2> setter, in float2 strength, float duration)
        {
            return TweenFactory.CreatePunchTween<float2, Punch2TweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> Punch(TweenGetter<float3> getter, TweenSetter<float3> setter, in float3 strength, float duration)
        {
            return TweenFactory.CreatePunchTween<float3, Punch3TweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> Shake(TweenGetter<float> getter, TweenSetter<float> setter, in float strength, float duration)
        {
            return TweenFactory.CreateShakeTween<float, ShakeTweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float2, ShakeTweenOptions> Shake(TweenGetter<float2> getter, TweenSetter<float2> setter, in float2 strength, float duration)
        {
            return TweenFactory.CreateShakeTween<float2, Shake2TweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> Shake(TweenGetter<float3> getter, TweenSetter<float3> setter, in float3 strength, float duration)
        {
            return TweenFactory.CreateShakeTween<float3, Shake3TweenPlugin>(getter, setter, strength, duration);
        }

        public static Tween<float3, PathTweenOptions> Path(TweenGetter<float3> getter, TweenSetter<float3> setter, float3[] points, float duration)
        {
            return TweenFactory.CreatePathTween(getter, setter, points, duration);
        }

        public static Tween<float3, PathTweenOptions> Path(TweenGetter<float3> getter, TweenSetter<float3> setter, Vector3[] points, float duration)
        {
            return TweenFactory.CreatePathTween(getter, setter, UnsafeUtility.As<Vector3[], float3[]>(ref points), duration);
        }

        public static Tween DelayedCall(float delay, Action callback)
        {
            Assert.IsTrue(delay >= 0f, "'delay' must be 0 or higher.");
            return TweenFactory.CreateUnitTween(delay).OnStepComplete(callback).OnComplete(callback);
        }

        public static Tween Empty(float duration)
        {
            return TweenFactory.CreateUnitTween(duration);
        }
    }
}