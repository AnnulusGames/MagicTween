using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Plugins;

namespace MagicTween.Core.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct ShakeTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, ShakeTweenAspect valueAspect)
            {
                ShakeTweenPlugin.EvaluateCore(valueAspect.StartValue, valueAspect.Options, valueAspect.Strength, aspect.progress, ref valueAspect.Random.random, out var result);
                valueAspect.CurrentValue = result;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Shake2TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Shake2TweenAspect valueAspect)
            {
                Shake2TweenPlugin.EvaluateCore(valueAspect.StartValue, valueAspect.Options, valueAspect.Strength, aspect.progress, ref valueAspect.Random.random, out var result);
                valueAspect.CurrentValue = result;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Shake3TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Shake3TweenAspect valueAspect)
            {
                Shake3TweenPlugin.EvaluateCore(valueAspect.StartValue, valueAspect.Options, valueAspect.Strength, aspect.progress, ref valueAspect.Random.random, out var result);
                valueAspect.CurrentValue = result;
            }
        }
    }

    public sealed partial class ShakeTweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin> { }
    public sealed partial class Shake2TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float2, ShakeTweenOptions, Shake2TweenPlugin> { }
    public sealed partial class Shake3TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin> { }
}