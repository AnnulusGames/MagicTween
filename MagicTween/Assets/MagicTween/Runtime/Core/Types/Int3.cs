using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<int3>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<int3>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<int3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorNoAlloc<int3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<int3>))]

namespace MagicTween.Core
{
    public readonly partial struct Int3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<int3>> startRefRO;
        readonly RefRO<TweenEndValue<int3>> endRefRO;
        readonly RefRW<TweenValue<int3>> currentRefRW;
        readonly RefRO<TweenOptions<IntegerTweenOptions>> optionsRefRO;

        public int3 startValue => startRefRO.ValueRO.value;
        public int3 endValue => endRefRO.ValueRO.value;

        public int3 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }

        public RoundingMode roundingMode => optionsRefRO.ValueRO.value.roundingMode;
    }

    [BurstCompile]
    public readonly struct Int3TweenPlugin : ITweenPlugin<int3>
    {
        public int3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out int3 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<int3>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<int3>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<IntegerTweenOptions>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, options.roundingMode, out result);
        }

        [BurstCompile]
        internal static void EvaluateCore(in int3 startValue, in int3 endValue, float t, bool isRelative, bool isFrom, RoundingMode roundingMode, out int3 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;

            float3 value;
            if (isFrom) value = math.lerp(resolvedEndValue, startValue, t);
            else value = math.lerp(startValue, resolvedEndValue, t);

            switch (roundingMode)
            {
                default:
                case RoundingMode.ToEven:
                    result = (int3)math.round(value);
                    break;
                case RoundingMode.AwayFromZero:
                    var x = value.x >= 0f ? (int)math.ceil(value.x) : (int)math.floor(value.x);
                    var y = value.y >= 0f ? (int)math.ceil(value.y) : (int)math.floor(value.y);
                    var z = value.z >= 0f ? (int)math.ceil(value.z) : (int)math.floor(value.z);
                    result = new int3(x, y, z);
                    break;
                case RoundingMode.ToZero:
                    result = (int3)math.trunc(value);
                    break;
                case RoundingMode.ToPositiveInfinity:
                    result = (int3)math.ceil(value);
                    break;
                case RoundingMode.ToNegativeInfinity:
                    result = (int3)math.floor(value);
                    break;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Int3TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Int3TweenAspect valueAspect)
            {
                Int3TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, valueAspect.roundingMode, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaInt3TweenTranslationSystem : LambdaTweenTranslationSystemBase<int3> { }
}