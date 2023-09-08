using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;

[assembly: RegisterGenericComponentType(typeof(TweenValue<double3>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<double3>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<double3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<double3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<double3>))]

namespace MagicTween.Core
{
    public readonly partial struct Double3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<double3>> startRefRO;
        readonly RefRO<TweenEndValue<double3>> endRefRO;
        readonly RefRW<TweenValue<double3>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public double3 startValue => startRefRO.ValueRO.value;
        public double3 endValue => endRefRO.ValueRO.value;

        public double3 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Double3TweenPlugin : ITweenPlugin<double3>
    {
        public double3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<double3>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<double3>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(in double3 startValue, in double3 endValue, float t, bool isRelative, bool isFrom, out double3 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Double3TweenSystem : ISystem
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
    public sealed partial class LambdaDouble3TweenTranslationSystem : LambdaTweenTranslationSystemBase<double3> { }
}