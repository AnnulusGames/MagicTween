using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<double>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<double>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<double>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<double>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<double>))]

namespace MagicTween.Core
{
    public readonly partial struct DoubleTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<double>> startRefRO;
        readonly RefRO<TweenEndValue<double>> endRefRO;
        readonly RefRW<TweenValue<double>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public double startValue => startRefRO.ValueRO.value;
        public double endValue => endRefRO.ValueRO.value;

        public double currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct DoubleTweenPlugin : ITweenPlugin<double>
    {
        public double Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<double>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<double>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(in double startValue, in double endValue, float t, bool isRelative, bool isFrom, out double result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct DoubleTweenSystem : ISystem
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
    public sealed partial class LambdaDoubleTweenTranslationSystem : LambdaTweenTranslationSystemBase<double> { }
}