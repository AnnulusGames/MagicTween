using Unity.Mathematics;
using Unity.Assertions;
using MagicTween.Core;
using MagicTween.Plugins;

namespace MagicTween
{
    partial struct Tween
    {
        public static Tween<float, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, float> getter, TweenSetter<TObject, float> setter, in float endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, float, NoOptions, FloatTweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<float2, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, float2> getter, TweenSetter<TObject, float2> setter, in float2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, float2, NoOptions, Float2TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<float3, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, float3> getter, TweenSetter<TObject, float3> setter, in float3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, float3, NoOptions, Float3TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<float4, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, float4> getter, TweenSetter<TObject, float4> setter, in float4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, float4, NoOptions, Float4TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<double, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, double> getter, TweenSetter<TObject, double> setter, in double endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, double, NoOptions, DoubleTweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<double2, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, double2> getter, TweenSetter<TObject, double2> setter, in double2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, double2, NoOptions, Double2TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<double3, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, double3> getter, TweenSetter<TObject, double3> setter, in double3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, double3, NoOptions, Double3TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<double4, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, double4> getter, TweenSetter<TObject, double4> setter, in double4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, double4, NoOptions, Double4TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<int, IntegerTweenOptions> To<TObject>(TObject target, TweenGetter<TObject, int> getter, TweenSetter<TObject, int> setter, in int endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, int, IntegerTweenOptions, IntTweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<int2, IntegerTweenOptions> To<TObject>(TObject target, TweenGetter<TObject, int2> getter, TweenSetter<TObject, int2> setter, in int2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, int2, IntegerTweenOptions, Int2TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<int3, IntegerTweenOptions> To<TObject>(TObject target, TweenGetter<TObject, int3> getter, TweenSetter<TObject, int3> setter, in int3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, int3, IntegerTweenOptions, Int3TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<int4, IntegerTweenOptions> To<TObject>(TObject target, TweenGetter<TObject, int4> getter, TweenSetter<TObject, int4> setter, in int4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, int4, IntegerTweenOptions, Int4TweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<long, IntegerTweenOptions> To<TObject>(TObject target, TweenGetter<TObject, long> getter, TweenSetter<TObject, long> setter, in long endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, long, IntegerTweenOptions, LongTweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> To<TObject>(TObject target, TweenGetter<TObject, quaternion> getter, TweenSetter<TObject, quaternion> setter, in quaternion endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateToTweenNoAlloc<TObject, quaternion, NoOptions, QuaternionTweenPlugin>(target, getter, setter, endValue, duration);
        }

        public static Tween<float, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, float> setter, in float startValue, in float endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, float, NoOptions, FloatTweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<float2, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, float2> setter, in float2 startValue, in float2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, float2, NoOptions, Float2TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<float3, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, float3> setter, in float3 startValue, in float3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, float3, NoOptions, Float3TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<float4, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, float4> setter, in float4 startValue, in float4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, float4, NoOptions, Float4TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<double, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, double> setter, in double startValue, in double endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, double, NoOptions, DoubleTweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<double2, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, double2> setter, in double2 startValue, in double2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, double2, NoOptions, Double2TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<double3, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, double3> setter, in double3 startValue, in double3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, double3, NoOptions, Double3TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<double4, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, double4> setter, in double4 startValue, in double4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, double4, NoOptions, Double4TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<int, IntegerTweenOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, int> setter, in int startValue, in int endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, int, IntegerTweenOptions, IntTweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<int2, IntegerTweenOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, int2> setter, in int2 startValue, in int2 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, int2, IntegerTweenOptions, Int2TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<int3, IntegerTweenOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, int3> setter, in int3 startValue, in int3 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, int3, IntegerTweenOptions, Int3TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<int4, IntegerTweenOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, int4> setter, in int4 startValue, in int4 endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, int4, IntegerTweenOptions, Int4TweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<quaternion, NoOptions> FromTo<TObject>(TObject target, TweenSetter<TObject, quaternion> setter, in quaternion startValue, in quaternion endValue, float duration)
            where TObject : class
        {
            Assert.IsNotNull(target);
            return TweenFactory.CreateFromToTweenNoAlloc<TObject, quaternion, NoOptions, QuaternionTweenPlugin>(target, startValue, endValue, duration, setter);
        }

        public static Tween<float, PunchTweenOptions> Punch<TObject>(TObject target, TweenGetter<TObject, float> getter, TweenSetter<TObject, float> setter, in float strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreatePunchTweenNoAlloc<TObject, float, PunchTweenPlugin>(target, getter, setter, strength, duration);
        }

        public static Tween<float2, PunchTweenOptions> Punch<TObject>(TObject target, TweenGetter<TObject, float2> getter, TweenSetter<TObject, float2> setter, in float2 strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreatePunchTweenNoAlloc<TObject, float2, Punch2TweenPlugin>(target, getter, setter, strength, duration);
        }

        public static Tween<float3, PunchTweenOptions> Punch<TObject>(TObject target, TweenGetter<TObject, float3> getter, TweenSetter<TObject, float3> setter, in float3 strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreatePunchTweenNoAlloc<TObject, float3, Punch3TweenPlugin>(target, getter, setter, strength, duration);
        }

        public static Tween<float, ShakeTweenOptions> Shake<TObject>(TObject target, TweenGetter<TObject, float> getter, TweenSetter<TObject, float> setter, in float strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreateShakeTweenNoAlloc<TObject, float, ShakeTweenPlugin>(target, getter, setter, strength, duration);
        }

        public static Tween<float2, ShakeTweenOptions> Shake<TObject>(TObject target, TweenGetter<TObject, float2> getter, TweenSetter<TObject, float2> setter, in float2 strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreateShakeTweenNoAlloc<TObject, float2, Shake2TweenPlugin>(target, getter, setter, strength, duration);
        }

        public static Tween<float3, ShakeTweenOptions> Shake<TObject>(TObject target, TweenGetter<TObject, float3> getter, TweenSetter<TObject, float3> setter, in float3 strength, float duration)
            where TObject : class
        {
            return TweenFactory.CreateShakeTweenNoAlloc<TObject, float3, Shake3TweenPlugin>(target, getter, setter, strength, duration);
        }
    }
}