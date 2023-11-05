// using Unity.Entities;
// using MagicTween.Core;

// namespace MagicTween
// {
//     public interface ITweenTranslator<TValue, TComponent> : ITweenTranslatorBase<TValue>
//         where TValue : unmanaged
//         where TComponent : unmanaged, IComponentData
//     {
//         TValue GetValue(ref TComponent component);
//         void Apply(ref TComponent component, in TValue value);
//     }

//     public interface ITweenTranslatorManaged<TValue, TComponent>
//         where TComponent : class
//     {
//         TValue GetValue(TComponent component);
//         void Apply(TComponent component, in TValue value);
//     }
// }

// namespace MagicTween.Core
// {
//     public interface ITweenTranslatorBase<TValue> : IComponentData
//     {
//         Entity TargetEntity { get; set; }
//     }
// }