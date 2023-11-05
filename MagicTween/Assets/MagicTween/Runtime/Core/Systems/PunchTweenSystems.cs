using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using MagicTween.Plugins;
using MagicTween.Core.Aspects;

namespace MagicTween.Core.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PunchTweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, PunchTweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.Strength, valueAspect.Options.frequency, valueAspect.Options.dampingRatio, aspect.progress, out var result);
                valueAspect.CurrentValue = valueAspect.StartValue + result;
            }
        }
    }


    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Punch2TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Punch2TweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.Strength, valueAspect.Options.frequency, valueAspect.Options.dampingRatio, aspect.progress, out var result);
                valueAspect.CurrentValue = valueAspect.StartValue + result;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct Punch3TweenSystem : ISystem
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
            public void Execute(TweenAspect aspect, Punch3TweenAspect valueAspect)
            {
                VibrationUtils.EvaluateStrength(valueAspect.Strength, valueAspect.Options.frequency, valueAspect.Options.dampingRatio, aspect.progress, out var result);
                valueAspect.CurrentValue = valueAspect.StartValue + result;
            }
        }
    }

    public sealed partial class PunchTweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin> { }
    public sealed partial class Punch2TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float2, PunchTweenOptions, Punch2TweenPlugin> { }
    public sealed partial class Punch3TweenDelegateTranslationSystem : TweenDelegateTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin> { }
}