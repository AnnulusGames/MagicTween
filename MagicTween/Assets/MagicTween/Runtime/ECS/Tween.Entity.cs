using Unity.Mathematics;
using MagicTween.Core;
using UnityEntity = Unity.Entities.Entity;

namespace MagicTween
{
    partial struct Tween
    {
        public struct Entity
        {
            public static Tween<float, NoOptions> To<TTranslator>(in UnityEntity entity, float endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float>
            {
                return TweenFactory.ECS.CreateTo<float, NoOptions, FloatTweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float2, NoOptions> To<TTranslator>(in UnityEntity entity, float2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float2>
            {
                return TweenFactory.ECS.CreateTo<float2, NoOptions, Float2TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float3, NoOptions> To<TTranslator>(in UnityEntity entity, float3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float3>
            {
                return TweenFactory.ECS.CreateTo<float3, NoOptions, Float3TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float4, NoOptions> To<TTranslator>(in UnityEntity entity, float4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float4>
            {
                return TweenFactory.ECS.CreateTo<float4, NoOptions, Float4TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double, NoOptions> To<TTranslator>(in UnityEntity entity, double endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double>
            {
                return TweenFactory.ECS.CreateTo<double, NoOptions, DoubleTweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double2, NoOptions> To<TTranslator>(in UnityEntity entity, double2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double2>
            {
                return TweenFactory.ECS.CreateTo<double2, NoOptions, Double2TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double3, NoOptions> To<TTranslator>(in UnityEntity entity, double3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double3>
            {
                return TweenFactory.ECS.CreateTo<double3, NoOptions, Double3TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double4, NoOptions> To<TTranslator>(in UnityEntity entity, double4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double4>
            {
                return TweenFactory.ECS.CreateTo<double4, NoOptions, Double4TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int, IntegerTweenOptions> To<TTranslator>(in UnityEntity entity, int endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int>
            {
                return TweenFactory.ECS.CreateTo<int, IntegerTweenOptions, IntTweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int2, IntegerTweenOptions> To<TTranslator>(in UnityEntity entity, int2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int2>
            {
                return TweenFactory.ECS.CreateTo<int2, IntegerTweenOptions, Int2TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int3, IntegerTweenOptions> To<TTranslator>(in UnityEntity entity, int3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int3>
            {
                return TweenFactory.ECS.CreateTo<int3, IntegerTweenOptions, Int3TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int4, IntegerTweenOptions> To<TTranslator>(in UnityEntity entity, int4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int4>
            {
                return TweenFactory.ECS.CreateTo<int4, IntegerTweenOptions, Int4TweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<long, IntegerTweenOptions> To<TTranslator>(in UnityEntity entity, long endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<long>
            {
                return TweenFactory.ECS.CreateTo<long, IntegerTweenOptions, LongTweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<quaternion, NoOptions> To<TTranslator>(in UnityEntity entity, quaternion endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<quaternion>
            {
                return TweenFactory.ECS.CreateTo<quaternion, NoOptions, QuaternionTweenPlugin, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float, NoOptions> FromTo<TTranslator>(in UnityEntity entity, float startValue, float endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float>
            {
                return TweenFactory.ECS.CreateFromTo<float, NoOptions, FloatTweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float2, NoOptions> FromTo<TTranslator>(in UnityEntity entity, float2 startValue, float2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float2>
            {
                return TweenFactory.ECS.CreateFromTo<float2, NoOptions, Float2TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float3, NoOptions> FromTo<TTranslator>(in UnityEntity entity, float3 startValue, float3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float3>
            {
                return TweenFactory.ECS.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float4, NoOptions> FromTo<TTranslator>(in UnityEntity entity, float4 startValue, float4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<float4>
            {
                return TweenFactory.ECS.CreateFromTo<float4, NoOptions, Float4TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double, NoOptions> FromTo<TTranslator>(in UnityEntity entity, double startValue, double endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double>
            {
                return TweenFactory.ECS.CreateFromTo<double, NoOptions, DoubleTweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double2, NoOptions> FromTo<TTranslator>(in UnityEntity entity, double2 startValue, double2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double2>
            {
                return TweenFactory.ECS.CreateFromTo<double2, NoOptions, Double2TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double3, NoOptions> FromTo<TTranslator>(in UnityEntity entity, double3 startValue, double3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double3>
            {
                return TweenFactory.ECS.CreateFromTo<double3, NoOptions, Double3TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double4, NoOptions> FromTo<TTranslator>(in UnityEntity entity, double4 startValue, double4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<double4>
            {
                return TweenFactory.ECS.CreateFromTo<double4, NoOptions, Double4TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int, IntegerTweenOptions> FromTo<TTranslator>(in UnityEntity entity, int startValue, int endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int>
            {
                return TweenFactory.ECS.CreateFromTo<int, IntegerTweenOptions, IntTweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int2, IntegerTweenOptions> FromTo<TTranslator>(in UnityEntity entity, int2 startValue, int2 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int2>
            {
                return TweenFactory.ECS.CreateFromTo<int2, IntegerTweenOptions, Int2TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int3, IntegerTweenOptions> FromTo<TTranslator>(in UnityEntity entity, int3 startValue, int3 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int3>
            {
                return TweenFactory.ECS.CreateFromTo<int3, IntegerTweenOptions, Int3TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int4, IntegerTweenOptions> FromTo<TTranslator>(in UnityEntity entity, int4 startValue, int4 endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<int4>
            {
                return TweenFactory.ECS.CreateFromTo<int4, IntegerTweenOptions, Int4TweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<long, IntegerTweenOptions> FromTo<TTranslator>(in UnityEntity entity, long startValue, long endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<long>
            {
                return TweenFactory.ECS.CreateFromTo<long, IntegerTweenOptions, LongTweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<quaternion, NoOptions> FromTo<TTranslator>(in UnityEntity entity, quaternion startValue, quaternion endValue, float duration)
                where TTranslator : unmanaged, ITweenTranslatorBase<quaternion>
            {
                return TweenFactory.ECS.CreateFromTo<quaternion, NoOptions, QuaternionTweenPlugin, TTranslator>(entity, startValue, endValue, duration);
            }
        }
    }
}