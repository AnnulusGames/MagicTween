using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;

[assembly: RegisterGenericComponentType(typeof(TweenOptions<NoOptions>))]

namespace MagicTween.Core
{
    public interface ITweenOptions { }
    public readonly struct NoOptions : ITweenOptions { }
}