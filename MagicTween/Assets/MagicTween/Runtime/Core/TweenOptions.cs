using MagicTween.Core;
using Unity.Entities;

[assembly: RegisterGenericComponentType(typeof(TweenOptions<NoOptions>))]

namespace MagicTween.Core
{
    public interface ITweenOptions { }
    public readonly struct NoOptions : ITweenOptions { }
}