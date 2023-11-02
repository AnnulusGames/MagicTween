using MagicTween.Core.Components;
using MagicTween.Plugins;
using Unity.Entities;

namespace MagicTween.Core
{
    public static class TweenControllerHelper
    {
        public static void Play(in Entity entity)
        {
            var canPlay = TweenHelper.TryPlay(entity, out var started);
            if (!canPlay) return;

            TweenHelper.TryCallOnStartAndOnPlay(entity, started);
        }

        public static void Pause(in Entity entity)
        {
            var canPause = TweenHelper.TryPause(entity);
            if (!canPause) return;

            TweenHelper.TryCallOnPause(entity);
        }

        public static void Kill(in Entity entity)
        {
            var canKill = TweenHelper.TryKill(entity);
            if (!canKill) return;

            TweenHelper.TryCallOnKill(entity);
        }

        public static void Complete(in Entity entity)
        {
            var canComplete = TweenHelper.TryComplete(entity);
            if (!canComplete) return;

            TweenHelper.TryCallOnComplete(entity);
        }

        public static void CompleteAndKill(in Entity entity)
        {
            var canCompleteAndKill = TweenHelper.TryCompleteAndKill(entity);
            if (!canCompleteAndKill) return;

            TweenHelper.TryCallOnCompleteAndOnKill(entity);
        }

        public static void Restart(in Entity entity)
        {
            if (!ECSCache.EntityManager.GetComponentData<TweenStartedFlag>(entity).value)
            {
                Play(entity);
                return;
            }

            var canRestart = TweenHelper.TryRestart(entity);
            if (!canRestart) return;
        }

        public static void Complete<TValue, TOptions, TPlugin, TController>(in TController controller, in Entity entity)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
            where TController : ITweenController<TValue>
        {
            var canComplete = TweenHelper.TryComplete<TValue, TOptions, TPlugin>(entity, out var currentValue);
            if (!canComplete) return;

            controller.SetValue(currentValue, entity);

            TweenHelper.TryCallOnComplete(entity);
        }

        public static void CompleteAndKill<TValue, TOptions, TPlugin, TController>(in TController controller, in Entity entity)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
            where TController : ITweenController<TValue>
        {
            var canCompleteAndKill = TweenHelper.TryCompleteAndKill<TValue, TOptions, TPlugin>(entity, out var currentValue);
            if (!canCompleteAndKill) return;

            controller.SetValue(currentValue, entity);

            TweenHelper.TryCallOnCompleteAndOnKill(entity);
        }

        public static void Restart<TValue, TOptions, TPlugin, TController>(in TController controller, in Entity entity)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
            where TController : ITweenController<TValue>
        {
            if (!ECSCache.EntityManager.GetComponentData<TweenStartedFlag>(entity).value)
            {
                Play(entity);
                return;
            }

            var canRestart = TweenHelper.TryRestart<TValue, TOptions, TPlugin>(entity, out var currentValue);
            if (!canRestart) return;

            controller.SetValue(currentValue, entity);
        }
    }
}