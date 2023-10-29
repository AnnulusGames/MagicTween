using Unity.Entities;

namespace MagicTween.Core
{
    public readonly struct TweenTranslationMode : IComponentData
    {
        public TweenTranslationMode(TweenTranslationOptions value) => this.value = value;
        public readonly TweenTranslationOptions value;
    }

    public enum TweenTranslationOptions : byte
    {
        FromTo,
        To
    }
}