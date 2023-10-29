using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<int>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<int>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<int>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegates<int>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegatesNoAlloc<int>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<IntegerTweenOptions>))]

namespace MagicTween.Core
{
    public readonly partial struct IntTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<int>> startRefRO;
        readonly RefRO<TweenEndValue<int>> endRefRO;
        readonly RefRW<TweenValue<int>> currentRefRW;
        readonly RefRO<TweenOptions<IntegerTweenOptions>> optionsRefRO;

        public int startValue => startRefRO.ValueRO.value;
        public int endValue => endRefRO.ValueRO.value;

        public int currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }

        public RoundingMode roundingMode => optionsRefRO.ValueRO.value.roundingMode;
    }

    public struct IntegerTweenOptions : ITweenOptions
    {
        public RoundingMode roundingMode;
    }

    [BurstCompile]
    public readonly struct IntTweenPlugin : ITweenPlugin<int>
    {
        public int Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
            => EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom);

        [BurstCompile]
        public int EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<int>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<int>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<IntegerTweenOptions>>(entity).value;
            return EvaluateCore(startValue, endValue, t, isRelative, isFrom, options.roundingMode);
        }

        [BurstCompile]
        internal static int EvaluateCore(int startValue, int endValue, float t, bool isRelative, bool isFrom, RoundingMode roundingMode)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;

            float value;
            if (isFrom) value = math.lerp(resolvedEndValue, startValue, t);
            else value = math.lerp(startValue, resolvedEndValue, t);

            switch (roundingMode)
            {
                default:
                case RoundingMode.ToEven: return (int)math.round(value);
                case RoundingMode.AwayFromZero: return value >= 0f ? (int)math.ceil(value) : (int)math.floor(value);
                case RoundingMode.ToZero: return (int)math.trunc(value);
                case RoundingMode.ToPositiveInfinity: return (int)math.ceil(value);
                case RoundingMode.ToNegativeInfinity: return (int)math.floor(value);
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct IntTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, IntTweenAspect valueAspect)
            {
                valueAspect.currentValue = IntTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.roundingMode);
            }
        }
    }

    public sealed partial class IntTweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<int> { }
}