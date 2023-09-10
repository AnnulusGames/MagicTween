using Unity.Entities;

namespace MagicTween.Core.Components
{
    public struct TweenValue<T> : IComponentData
        where T : unmanaged
    {
        public T value;
    }

    public struct TweenStartValue<T> : IComponentData
        where T : unmanaged
    {
        public T value;
    }
    
    public struct TweenEndValue<T> : IComponentData
        where T : unmanaged
    {
        public T value;
    }

    public struct TweenOptions<TOptions> : IComponentData
        where TOptions : unmanaged, ITweenOptions
    {
        public TOptions options;
    }
}