using Unity.Entities;

namespace MagicTween
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class MagicTweenSystemGroup : ComponentSystemGroup { }
}

namespace MagicTween.Core
{
    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    public partial class MagicTweenCoreSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    [UpdateAfter(typeof(MagicTweenCoreSystemGroup))]
    public partial class MagicTweenUpdateSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    [UpdateAfter(typeof(MagicTweenUpdateSystemGroup))]
    public partial class MagicTweenTranslationSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    [UpdateAfter(typeof(MagicTweenTranslationSystemGroup))]
    public partial class MagicTweenCallbackSystemGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    [UpdateAfter(typeof(MagicTweenCallbackSystemGroup))]
    public partial class MagicTweenCleanupSystemGroup : ComponentSystemGroup { }
}