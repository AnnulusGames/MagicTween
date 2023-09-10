using Unity.Entities;

namespace MagicTween.Core
{
    public readonly struct TweenTranslationOptionsData : IComponentData
    {
        public TweenTranslationOptionsData(TweenTranslationOptions value) => this.value = value;
        public readonly TweenTranslationOptions value;
    }

    public enum TweenTranslationOptions : byte
    {
        FromTo,
        To
    }
}