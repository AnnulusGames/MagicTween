using MagicTween.Core.Systems;
using Unity.Entities;

namespace MagicTween.Core.Transforms
{
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public partial class TweenTransformTranslationSystemGroup : ComponentSystemGroup { }
}