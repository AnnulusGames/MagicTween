using Unity.Entities;

namespace MagicTween.Core
{
    public interface ITweenPlugin<TValue>
        where TValue : unmanaged
    {
        TValue Evaluate(in Entity entity, float t, bool isRelative, bool isFrom);
    }
}