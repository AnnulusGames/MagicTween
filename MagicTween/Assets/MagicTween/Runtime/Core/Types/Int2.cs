using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<int2>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<int2>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<int2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<int2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<int2>))]

namespace MagicTween.Core
{
    public readonly partial struct Int2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<int2>> startRefRO;
        readonly RefRO<TweenEndValue<int2>> endRefRO;
        readonly RefRW<TweenValue<int2>> currentRefRW;
        readonly RefRO<TweenOptions<IntegerTweenOptions>> optionsRefRO;

        public int2 startValue => startRefRO.ValueRO.value;
        public int2 endValue => endRefRO.ValueRO.value;

        public int2 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }

        public RoundingMode roundingMode => optionsRefRO.ValueRO.value.roundingMode;
    }

    [BurstCompile]
    public readonly struct Int2TweenPlugin : ITweenPlugin<int2>
    {
        public int2 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out int2 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<int2>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<int2>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<IntegerTweenOptions>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, options.roundingMode, out result);
        }

        [BurstCompile]
        internal static void EvaluateCore(in int2 startValue, in int2 endValue, float t, bool isRelative, bool isFrom, RoundingMode roundingMode, out int2 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;

            float2 value;
            if (isFrom) value = math.lerp(resolvedEndValue, startValue, t);
            else value = math.lerp(startValue, resolvedEndValue, t);

            switch (roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    result = (int2)math.round(value);
                    break;
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    result = new int2(x, y);
                    break;
                case RoundingMode.ToZero:
                    result = (int2)math.trunc(value);
                    break;
                case RoundingMode.ToPositiveInfinity:
                    result = (int2)math.ceil(value);
                    break;
                case RoundingMode.ToNegativeInfinity:
                    result = (int2)math.floor(value);
                    break;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Int2TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Int2TweenAspect valueAspect)
            {
                Int2TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.roundingMode, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaInt2TweenTranslationSystem : LambdaTweenTranslationSystemBase<int2> { }
}