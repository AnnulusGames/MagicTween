using Unity.Entities;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    using static TweenWorld;

    public abstract class TweenControllerBase<TValue, TPlugin> : ITweenController
        where TValue : unmanaged
        where TPlugin : unmanaged, ITweenPlugin<TValue>
    {
        public void Play(in Entity entity)
        {
            var canPlay = TweenHelper.TryPlay(entity, out var started);
            if (!canPlay) return;

            TweenHelper.TryCallOnStartAndOnPlay(entity, started);
        }

        public void Pause(in Entity entity)
        {
            var canPause = TweenHelper.TryPause(entity);
            if (!canPause) return;

            TweenHelper.TryCallOnPause(entity);
        }

        public void Kill(in Entity entity)
        {
            var canKill = TweenHelper.TryKill(entity);
            if (!canKill) return;

            TweenHelper.TryCallOnKill(entity);
        }

        public void Complete(in Entity entity)
        {
            var canComplete = TweenHelper.TryComplete<TValue, TPlugin>(entity, out var currentValue);
            if (!canComplete) return;

            SetValue(currentValue, entity);

            TweenHelper.TryCallOnComplete(entity);
        }

        public void CompleteAndKill(in Entity entity)
        {
            var canCompleteAndKill = TweenHelper.TryCompleteAndKill<TValue, TPlugin>(entity, out var currentValue);
            if (!canCompleteAndKill) return;

            SetValue(currentValue, entity);

            TweenHelper.TryCallOnCompleteAndOnKill(entity);
        }

        public void Restart(in Entity entity)
        {
            if (!EntityManager.GetComponentData<TweenStartedFlag>(entity).value)
            {
                Play(entity);
                return;
            }

            var canRestart = TweenHelper.TryRestart<TValue, TPlugin>(entity, out var currentValue);
            if (!canRestart) return;

            SetValue(currentValue, entity);
        }

        protected abstract void SetValue(TValue currentValue, in Entity entity);
    }
}