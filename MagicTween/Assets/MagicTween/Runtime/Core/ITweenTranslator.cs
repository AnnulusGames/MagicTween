using Unity.Entities;

namespace MagicTween
{
    public interface ITweenTranslator<TValue, TComponent> : IComponentData
        where TValue : unmanaged
        where TComponent : unmanaged, IComponentData
    {
        TValue GetValue(ref TComponent component);
        void Apply(ref TComponent component, in TValue value);
    }
}