using System;
using Unity.Entities;

namespace MagicTween.Core
{
    [UpdateInGroup(typeof(MagicTweenSystemGroup))]
    [UpdateBefore(typeof(MagicTweenCoreSystemGroup))]
    public partial class MagicTweenInitializationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            if (TweenWorld.World != null && TweenWorld.World != World) return;

            TweenWorld.Initialize();
            ArchetypeStore.Initialize();
            TweenControllerContainer.Clear();
            MagicTweenSettings.Initialize();
            SharedRandom.InitState((uint)DateTime.Now.Ticks);
        }

        protected override void OnUpdate() { }

        protected override void OnDestroy()
        {
            if (TweenWorld.World != World) return;

            ArchetypeStore.Dispose();
        }
    }
}