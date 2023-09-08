using Unity.Entities;

namespace MagicTween.Core
{
    public interface ITweenHandle
    {
        Entity GetEntity();
        Tween AsUnitTween();
    }
}