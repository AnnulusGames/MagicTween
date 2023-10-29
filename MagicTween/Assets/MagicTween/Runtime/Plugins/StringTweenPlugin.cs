using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween.Plugins
{
    // TODO: Support for NoAlloc Tween.To methods

    public struct StringTweenOptions : ITweenOptions
    {
        public ScrambleMode scrambleMode;
        public bool richTextEnabled;
    }
    
    [BurstCompile]
    public readonly struct StringTweenPlugin : ITweenPluginBase<UnsafeText>
    {
        // Evaluate() returns new NaviteText(Allocator.Temp)
        public UnsafeText Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<UnsafeText>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<UnsafeText>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<StringTweenOptions>>(entity).value;
            var customChars = entityManager.GetComponentData<StringTweenCustomScrambleChars>(entity).customChars;
            EvaluateCore(ref startValue, ref endValue, context.Progress, context.IsInverted, options.richTextEnabled, options.scrambleMode, ref customChars, out var result);
            return result;
        }

        [BurstCompile]
        public static void EvaluateCore(ref UnsafeText startValue, ref UnsafeText endValue, float t, bool isFrom, bool richTextEnabled, ScrambleMode scrambleMode, ref UnsafeText customChars, out UnsafeText result)
        {
            if (isFrom) result = StringUtils.CreateTweenedText(ref endValue, ref startValue, t, scrambleMode, richTextEnabled, ref customChars, Allocator.Temp);
            else result = StringUtils.CreateTweenedText(ref startValue, ref endValue, t, scrambleMode, richTextEnabled, ref customChars, Allocator.Temp);
        }
    }
}