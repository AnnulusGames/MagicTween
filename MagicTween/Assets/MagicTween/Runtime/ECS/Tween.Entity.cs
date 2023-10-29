using Unity.Mathematics;
using MagicTween.Core;
using UnityEntity = Unity.Entities.Entity;
using Unity.Entities;

namespace MagicTween
{
    partial struct Tween
    {
        public struct Entity
        {
            public static Tween<float, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, float endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float, TComponent>
            {
                return TweenFactory.ECS.CreateTo<float, NoOptions, FloatTweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float2, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, float2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float2, TComponent>
            {
                return TweenFactory.ECS.CreateTo<float2, NoOptions, Float2TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float3, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, float3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float3, TComponent>
            {
                return TweenFactory.ECS.CreateTo<float3, NoOptions, Float3TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float4, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, float4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float4, TComponent>
            {
                return TweenFactory.ECS.CreateTo<float4, NoOptions, Float4TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, double endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double, TComponent>
            {
                return TweenFactory.ECS.CreateTo<double, NoOptions, DoubleTweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double2, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, double2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double2, TComponent>
            {
                return TweenFactory.ECS.CreateTo<double2, NoOptions, Double2TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double3, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, double3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double3, TComponent>
            {
                return TweenFactory.ECS.CreateTo<double3, NoOptions, Double3TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<double4, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, double4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double4, TComponent>
            {
                return TweenFactory.ECS.CreateTo<double4, NoOptions, Double4TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int, IntegerTweenOptions> To<TComponent, TTranslator>(in UnityEntity entity, int endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int, TComponent>
            {
                return TweenFactory.ECS.CreateTo<int, IntegerTweenOptions, IntTweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int2, IntegerTweenOptions> To<TComponent, TTranslator>(in UnityEntity entity, int2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int2, TComponent>
            {
                return TweenFactory.ECS.CreateTo<int2, IntegerTweenOptions, Int2TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int3, IntegerTweenOptions> To<TComponent, TTranslator>(in UnityEntity entity, int3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int3, TComponent>
            {
                return TweenFactory.ECS.CreateTo<int3, IntegerTweenOptions, Int3TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<int4, IntegerTweenOptions> To<TComponent, TTranslator>(in UnityEntity entity, int4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int4, TComponent>
            {
                return TweenFactory.ECS.CreateTo<int4, IntegerTweenOptions, Int4TweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<long, IntegerTweenOptions> To<TComponent, TTranslator>(in UnityEntity entity, long endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<long, TComponent>
            {
                return TweenFactory.ECS.CreateTo<long, IntegerTweenOptions, LongTweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<quaternion, NoOptions> To<TComponent, TTranslator>(in UnityEntity entity, quaternion endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<quaternion, TComponent>
            {
                return TweenFactory.ECS.CreateTo<quaternion, NoOptions, QuaternionTweenPlugin, TComponent, TTranslator>(entity, endValue, duration);
            }

            public static Tween<float, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, float startValue, float endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<float, NoOptions, FloatTweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float2, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, float2 startValue, float2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float2, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<float2, NoOptions, Float2TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float3, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, float3 startValue, float3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float3, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<float3, NoOptions, Float3TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<float4, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, float4 startValue, float4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<float4, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<float4, NoOptions, Float4TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, double startValue, double endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<double, NoOptions, DoubleTweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double2, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, double2 startValue, double2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double2, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<double2, NoOptions, Double2TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double3, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, double3 startValue, double3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double3, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<double3, NoOptions, Double3TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<double4, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, double4 startValue, double4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<double4, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<double4, NoOptions, Double4TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int, IntegerTweenOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, int startValue, int endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<int, IntegerTweenOptions, IntTweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int2, IntegerTweenOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, int2 startValue, int2 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int2, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<int2, IntegerTweenOptions, Int2TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int3, IntegerTweenOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, int3 startValue, int3 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int3, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<int3, IntegerTweenOptions, Int3TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<int4, IntegerTweenOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, int4 startValue, int4 endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<int4, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<int4, IntegerTweenOptions, Int4TweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<long, IntegerTweenOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, long startValue, long endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<long, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<long, IntegerTweenOptions, LongTweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }

            public static Tween<quaternion, NoOptions> FromTo<TComponent, TTranslator>(in UnityEntity entity, quaternion startValue, quaternion endValue, float duration)
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<quaternion, TComponent>
            {
                return TweenFactory.ECS.CreateFromTo<quaternion, NoOptions, QuaternionTweenPlugin, TComponent, TTranslator>(entity, startValue, endValue, duration);
            }
        }
    }
}