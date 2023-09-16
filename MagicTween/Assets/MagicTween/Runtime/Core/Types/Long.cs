using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<long>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<long>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<long>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<long>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<long>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<IntegerTweenOptions>))]

namespace MagicTween.Core
{
    public readonly partial struct LongTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<long>> startRefRO;
        readonly RefRO<TweenEndValue<long>> endRefRO;
        readonly RefRW<TweenValue<long>> currentRefRW;
        readonly RefRO<TweenOptions<IntegerTweenOptions>> optionsRefRO;

        public long startValue => startRefRO.ValueRO.value;
        public long endValue => endRefRO.ValueRO.value;

        public long currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }

        public RoundingMode roundingMode => optionsRefRO.ValueRO.value.roundingMode;
    }

    [BurstCompile]
    public readonly struct LongTweenPlugin : ITweenPlugin<long>
    {
        public long Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
            => EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom);

        [BurstCompile]
        public static long EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<long>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<long>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<IntegerTweenOptions>>(entity).value;
            return EvaluateCore(startValue, endValue, t, isRelative, isFrom, options.roundingMode);
        }

        [BurstCompile]
        internal static long EvaluateCore(long startValue, long endValue, float t, bool isRelative, bool isFrom, RoundingMode roundingMode)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;

            float value;
            if (isFrom) value = math.lerp(resolvedEndValue, startValue, t);
            else value = math.lerp(startValue, resolvedEndValue, t);

            switch (roundingMode)
            {
                default:
                case RoundingMode.ToEven: return (long)math.round(value);
                case RoundingMode.AwayFromZero: return value >= 0f ? (long)math.ceil(value) : (long)math.floor(value);
                case RoundingMode.ToZero: return (long)math.trunc(value);
                case RoundingMode.ToPositiveInfinity: return (long)math.ceil(value);
                case RoundingMode.ToNegativeInfinity: return (long)math.floor(value);
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct LongTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, LongTweenAspect valueAspect)
            {
                valueAspect.currentValue = LongTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.roundingMode);
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaLongTweenTranslationSystem : LambdaTweenTranslationSystemBase<long> { }
}