using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;

[assembly: RegisterGenericComponentType(typeof(TweenValue<double2>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<double2>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<double2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<double2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<double2>))]

namespace MagicTween.Core
{
    public readonly partial struct Double2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<double2>> startRefRO;
        readonly RefRO<TweenEndValue<double2>> endRefRO;
        readonly RefRW<TweenValue<double2>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public double2 startValue => startRefRO.ValueRO.value;
        public double2 endValue => endRefRO.ValueRO.value;

        public double2 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Double2TweenPlugin : ITweenPlugin<double2>
    {
        public double2 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<double2>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<double2>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(in double2 startValue, in double2 endValue, float t, bool isRelative, bool isFrom, out double2 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Double2TweenSystem : ISystem
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
    public sealed partial class LambdaDouble2TweenTranslationSystem : LambdaTweenTranslationSystemBase<double2> { }
}
