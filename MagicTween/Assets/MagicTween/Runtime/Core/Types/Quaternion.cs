using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<quaternion>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<quaternion>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<quaternion>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<quaternion>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorNoAlloc<quaternion>))]

namespace MagicTween.Core
{
    public readonly partial struct QuaternionTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<quaternion>> startRefRO;
        readonly RefRO<TweenEndValue<quaternion>> endRefRO;
        readonly RefRW<TweenValue<quaternion>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public quaternion startValue => startRefRO.ValueRO.value;
        public quaternion endValue => endRefRO.ValueRO.value;

        public quaternion currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct QuaternionTweenPlugin : ITweenPlugin<quaternion>
    {
        public quaternion Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out quaternion result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<quaternion>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<quaternion>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out result);
        }

        [BurstCompile]
        public static void EvaluateCore(in quaternion startValue, in quaternion endValue, float t, bool isRelative, bool isFrom, out quaternion result)
        {
            var resolvedEndValue = isRelative ? math.mul(startValue, endValue) : endValue;
            if (isFrom) result = math.slerp(resolvedEndValue, startValue, t);
            else result = math.slerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct QuaternionTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, QuaternionTweenAspect valueAspect)
            {
                QuaternionTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaQuaternionTweenTranslationSystem : LambdaTweenTranslationSystemBase<quaternion> { }
}