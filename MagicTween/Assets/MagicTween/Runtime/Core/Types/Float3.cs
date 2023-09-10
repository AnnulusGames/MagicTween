using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenValue<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessorUnsafe<float3>))]

namespace MagicTween.Core
{
    public readonly partial struct Float3TweenAspect : IAspect
    {
        readonly RefRO<TweenStartValue<float3>> startRefRO;
        readonly RefRO<TweenEndValue<float3>> endRefRO;
        readonly RefRW<TweenValue<float3>> currentRefRW;

#pragma warning disable CS0414
        readonly RefRO<TweenOptions<NoOptions>> optionsRef;
#pragma warning restore CS0414

        public float3 startValue => startRefRO.ValueRO.value;
        public float3 endValue => endRefRO.ValueRO.value;

        public float3 currentValue
        {
            get => currentRefRW.ValueRO.value;
            set => currentRefRW.ValueRW.value = value;
        }
    }

    [BurstCompile]
    public readonly struct Float3TweenPlugin : ITweenPlugin<float3>
    {
        public float3 Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<float3>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<float3>>(entity).value;
            EvaluateCore(startValue, endValue, t, isRelative, isFrom, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(in float3 startValue, in float3 endValue, float t, bool isRelative, bool isFrom, out float3 result)
        {
            var resolvedEndValue = isRelative ? startValue + endValue : endValue;
            if (isFrom) result = math.lerp(resolvedEndValue, startValue, t);
            else result = math.lerp(startValue, resolvedEndValue, t);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Float3TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Float3TweenAspect valueAspect)
            {
                Float3TweenPlugin.EvaluateCore(valueAspect.startValue, valueAspect.endValue, aspect.progress, aspect.isRelative, aspect.inverted, out var result);
                valueAspect.currentValue = result;
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class LambdaFloat3TweenTranslationSystem : LambdaTweenTranslationSystemBase<float3> { }
}