using Unity.Entities;

namespace MagicTween.Core
{
    public readonly struct TweenTranslationModeData : IComponentData
    {
        public TweenTranslationModeData(TweenTranslationMode value) => this.value = value;
        public readonly TweenTranslationMode value;
    }

    public enum TweenTranslationMode : byte
    {
        None,
        FromTo,
        To
    }
}