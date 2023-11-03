#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
using Unity.Entities;
using MagicTween.Core.Systems;

namespace MagicTween.Core.Transforms.Systems
{
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public partial class TweenTransformTranslationSystemGroup : ComponentSystemGroup { }
}
#endif