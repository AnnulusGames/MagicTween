using MagicTween.Core.Components;
using Unity.Entities;

namespace MagicTween
{
    public interface ITweenPlugin<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
    {
        TValue Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context);
    }

    public interface ICustomTweenPlugin<TValue, TOptions> : ITweenPlugin<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
    {
        TValue ITweenPlugin<TValue, TOptions>.Evaluate(in Entity entity, ref EntityManager entityManager, in TweenEvaluationContext context)
        {
            var startValue = entityManager.GetComponentData<TweenStartValue<TValue>>(entity).value;
            var endValue = entityManager.GetComponentData<TweenEndValue<TValue>>(entity).value;
            var options = entityManager.GetComponentData<TweenOptions<TOptions>>(entity).value;
            return Evaluate(startValue, endValue, options, context);
        }

        TValue Evaluate(in TValue startValue, in TValue endValue, in TOptions options, in TweenEvaluationContext context);
    }

    public readonly struct TweenEvaluationContext
    {
        public readonly float Progress;
        public readonly bool IsRelative;
        public readonly bool IsInverted;

        internal TweenEvaluationContext(float progress, bool isRelative, bool isInverted)
        {
            this.Progress = progress;
            this.IsRelative = isRelative;
            this.IsInverted = isInverted;
        }
    }
}