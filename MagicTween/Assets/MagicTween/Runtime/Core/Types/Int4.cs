using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<int4>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<int4>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<int4>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegatesNoAlloc<int4>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegates<int4>))]

namespace MagicTween.Core
{
    public readonly partial struct Int4TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<int4>> startRefRO;
        readonly RefRO<TweenEndValue<int4>> endRefRO;
        readonly RefRW<TweenValue<int4>> currentRefRW;
        readonly RefRO<TweenOptions<IntegerTweenOptions>> optionsRefRO;

        public int4 startValue => startRefRO.ValueRO.value;
        public int4 endValue => endRefRO.ValueRO.value;

        public int4 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }

        public RoundingMode roundingMode => optionsRefRO.ValueRO.value.roundingMode;
    }

    [BurstCompile]
    public readonly struct Int4TweenPlugin : ITweenPlugin<int4>
    {
        public int4 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out int4 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<int4>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<int4>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<IntegerTweenOptions>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, options.roundingMode, out result);
        }

        [BurstCompile]
        internal static void EvaluateCore(in int4 startValue, in int4 endValue, float t, bool isRelative, bool isFrom, RoundingMode roundingMode, out int4 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;

            float4 value;
            if (isFrom) value = math.lerp(resolvedEndValue, startValue, t);
            else value = math.lerp(startValue, resolvedEndValue, t);

            switch (roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    result = (int4)math.round(value);
                    break;
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    var z = value.z >= 0f ? (int)math.ceil(value.z) : (int)math.floor(value.z);
                    var w = value.w >= 0f ? (int)math.ceil(value.w) : (int)math.floor(value.w);
                    result = new int4(x, y, z, w);
                    break;
                case RoundingMode.ToZero:
                    result = (int4)math.trunc(value);
                    break;
                case RoundingMode.ToPositiveInfinity:
                    result = (int4)math.ceil(value);
                    break;
                case RoundingMode.ToNegativeInfinity:
                    result = (int4)math.floor(value);
                    break;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Int4TweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, Int4TweenAspect valueAspect)
            {
                Int4TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.roundingMode, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    public sealed partial class Int4TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<int4> { }
}