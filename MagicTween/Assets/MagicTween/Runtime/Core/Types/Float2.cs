using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<float2>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<float2>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<float2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<float2>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<float2>))]

namespace MagicTween.Core
{
    public readonly partial struct Float2TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float2>> startRefRO;
        readonly RefRO<TweenEndValue<float2>> endRefRO;
        readonly RefRW<TweenValue<float2>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public float2 startValue => startRefRO.ValueRO.value;
        public float2 endValue => endRefRO.ValueRO.value;

        public float2 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Float2TweenPlugin : ITweenPlugin<float2>
    {
        public float2 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<float2>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<float2>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(in float2 startValue, in float2 endValue, float t, bool isRelative, bool isFrom, out float2 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Float2TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Float2TweenAspect valueAspect)
            {
                Float2TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaFloat2TweenTranslationSystem : LambdaTweenTranslationSystemBase<float2> { }
}