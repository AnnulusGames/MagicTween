using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<float>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<float>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<float>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegates<float>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegatesNoAlloc<float>))]

namespace MagicTween.Core
{
    public readonly partial struct FloatTweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float>> startRefRO;
        readonly RefRO<TweenEndValue<float>> endRefRO;
        readonly RefRW<TweenValue<float>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public float startValue => startRefRO.ValueRO.value;
        public float endValue => endRefRO.ValueRO.value;

        public float currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct FloatTweenPlugin : ITweenPlugin<float>
    {
        public float Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
            => EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom);

        [BurstCompile]
        public static float EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<float>>(entity).value;
            return EvaluateCore(startValue, endValue, t, isRelative, isFrom);
        }

        [BurstCompile]
        internal static float EvaluateCore(float startValue, float endValue, float t, bool isRelative, bool isFrom)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) return math.lerp(resolvedEndValue, startValue, t);
            else return math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct FloatTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, FloatTweenAspect valueAspect)
            {
                valueAspect.currentValue = FloatTweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted);
            }
        }
    }

    public sealed partial class FloatTweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float> { }
}