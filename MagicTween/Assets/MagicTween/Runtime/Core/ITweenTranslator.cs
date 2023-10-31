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

    internal interface ITweenTranslatorManaged<TValue, TObject> : IComponentData
        where TObject : class
    {
        TValue GetValue(TObject target);
        void Apply(TObject target, in TValue value);
    }
}