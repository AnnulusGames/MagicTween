using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<double4>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<double4>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<double4>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<double4>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<double4>))]

namespace MagicTween.Core
{
    public readonly partial struct Double4TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<double4>> startRefRO;
        readonly RefRO<TweenEndValue<double4>> endRefRO;
        readonly RefRW<TweenValue<double4>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public double4 startValue => startRefRO.ValueRO.value;
        public double4 endValue => endRefRO.ValueRO.value;

        public double4 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Double4TweenPlugin : ITweenPlugin<double4>
    {
        public double4 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out double4 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<double4>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<double4>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out result);
        }

        [BurstCompile]
        public static void EvaluateCore(in double4 startValue, in double4 endValue, float t, bool isRelative, bool isFrom, out double4 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Double4TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, DoubleTweenAspect valueAspect)
            {
                DoubleTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaDouble4TweenTranslationSystem : LambdaTweenTranslationSystemBase<double4> { }
}