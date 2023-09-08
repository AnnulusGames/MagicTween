using Unity.Entities;
using MagicTween.Core;

namespace MagicTween
{
    public interface ITweenTranslator<TValue, TComponent> : ITweenTranslatorBase<TValue>
        where TValue : unmanaged
        where TComponent : unmanaged, IComponentData
    {
        TValue GetValue(ref TComponent component);
        void Apply(ref TComponent component, in TValue value);
    }
}

namespace MagicTween.Core
{
    public interface ITweenTranslatorBase<TValue> : IComponentData
        where TValue : unmanaged
    {
        Entity TargetEntity { get; set; }
    }
}