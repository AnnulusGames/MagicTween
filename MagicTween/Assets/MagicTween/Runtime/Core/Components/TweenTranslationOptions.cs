using Unity.Entities;

namespace MagicTween.Core.Components
{
    public readonly struct TweenTranslationModeData : IComponentData
    {
        public TweenTranslationModeData(TweenTranslationMode value) => this.value = value;
        public readonly TweenTranslationMode value;
    }
}

namespace MagicTween.Core
{
    public enum TweenTranslationMode : byte
    {
        None,
        FromTo,
        To
    }
}