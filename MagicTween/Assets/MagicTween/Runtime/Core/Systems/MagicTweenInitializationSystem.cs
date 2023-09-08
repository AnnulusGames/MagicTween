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
            TweenWorld.Initialize();
            ArchetypeStore.Initialize();
            TweenControllerContainer.Clear();
            MagicTweenSettings.Initialize();
            SharedRandom.InitState((uint)DateTime.Now.Ticks);
        }

        protected override void OnUpdate() { }

        protected override void OnDestroy()
        {
            ArchetypeStore.Dispose();
        }
    }
}