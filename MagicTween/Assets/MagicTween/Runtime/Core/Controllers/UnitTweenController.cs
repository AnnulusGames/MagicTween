using Unity.Entities;

namespace MagicTween.Core
{
    public sealed class UnitTweenController : ITweenController
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
            var canComplete = TweenHelper.TryComplete(entity);
            if (!canComplete) return;

            TweenHelper.TryCallOnComplete(entity);
        }

        public void CompleteAndKill(in Entity entity)
        {
            var canCompleteAndKill = TweenHelper.TryCompleteAndKill(entity);
            if (!canCompleteAndKill) return;

            TweenHelper.TryCallOnCompleteAndOnKill(entity);
        }

        public void Restart(in Entity entity)
        {
            if (!TweenWorld.EntityManager.GetComponentData<TweenStartedFlag>(entity).started)
            {
                Play(entity);
                return;
            }

            var canRestart = TweenHelper.TryRestart(entity);
            if (!canRestart) return;
        }
    }
}