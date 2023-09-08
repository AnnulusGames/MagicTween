using Unity.Entities;

namespace MagicTween.Core
{
    public interface ITweenController
    {
        void Play(in Entity entity);
        void Pause(in Entity entity);
        void Restart(in Entity entity);
        void Complete(in Entity entity);
        void Kill(in Entity entity);
        void CompleteAndKill(in Entity entity);
    }
}