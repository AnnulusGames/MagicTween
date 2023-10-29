using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<float4>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<float4>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<float4>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegates<float4>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegatesNoAlloc<float4>))]

namespace MagicTween.Core
{
    public readonly partial struct Float4TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float4>> startRefRO;
        readonly RefRO<TweenEndValue<float4>> endRefRO;
        readonly RefRW<TweenValue<float4>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public float4 startValue => startRefRO.ValueRO.value;
        public float4 endValue => endRefRO.ValueRO.value;

        public float4 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Float4TweenPlugin : ITweenPlugin<float4>
    {
        public float4 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            EvaluateCore(ref TweenWorld.EntityManagerRef, entity, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref EntityManager entityManager, in Entity entity, float t, bool isRelative, bool isFrom, out float4 result)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<float4>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<float4>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out result);
        }

        [BurstCompile]
        public static void EvaluateCore(in float4 startValue, in float4 endValue, float t, bool isRelative, bool isFrom, out float4 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Float4TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Float4TweenAspect valueAspect)
            {
                Float4TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    public sealed partial class Float4TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float4> { }
}