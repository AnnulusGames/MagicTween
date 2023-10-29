using Unity.Entities;

namespace MagicTween.Core.Controllers
{
    public sealed class UnitTweenController : ITweenController
    {
        public void Play(in Entity entity) => TweenControllerHelper.Play(entity);
        public void Pause(in Entity entity) => TweenControllerHelper.Pause(entity);
        public void Kill(in Entity entity) => TweenControllerHelper.Kill(entity);
        public void Complete(in Entity entity) => TweenControllerHelper.Complete(entity);
        public void CompleteAndKill(in Entity entity) => TweenControllerHelper.CompleteAndKill(entity);
        public void Restart(in Entity entity) => TweenControllerHelper.Restart(entity);
    }
}