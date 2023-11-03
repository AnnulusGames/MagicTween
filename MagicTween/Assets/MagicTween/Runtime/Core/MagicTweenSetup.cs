using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core
{
    static class MagicTweenSetup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Setup()
        {
            var world = World.DefaultGameObjectInjectionWorld;

            SharedRandom.InitState((uint)DateTime.Now.Ticks);
            ECSCache.Create(world);
            MagicTweenSettings.Initialize();
#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
            Transforms.TransformManager.Initialize();
#endif

            Application.quitting += () => Cleanup();
        }

        static void Cleanup()
        {
            ECSCache.Dispose();
#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS
            Transforms.TransformManager.Dispose();
#endif
        }
    }
}
