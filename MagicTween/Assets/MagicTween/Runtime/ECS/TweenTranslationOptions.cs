using Unity.Entities;

namespace MagicTween.Core
{
    public struct TweenTranslationOptionsData : IComponentData
    {
        public TweenTranslationOptions options;
    }
    
    public enum TweenTranslationOptions : byte
    {
        FromTo,
        To
    }
}