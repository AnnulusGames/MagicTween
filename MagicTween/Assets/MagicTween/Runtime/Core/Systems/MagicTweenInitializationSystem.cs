// using System;
// using Unity.Entities;

// namespace MagicTween.Core.Systems
// {
//     [UpdateInGroup(typeof(MagicTweenSystemGroup))]
//     [UpdateBefore(typeof(MagicTweenCoreSystemGroup))]
//     public partial class MagicTweenInitializationSystem : SystemBase
//     {
//         protected override void OnCreate()
//         {
//             if (TweenWorld.World != null && TweenWorld.World.IsCreated && TweenWorld.World != World) return;

//             TweenWorld.Initialize();
//             MagicTweenSettings.Initialize();
//             SharedRandom.InitState((uint)DateTime.Now.Ticks);

// #if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
//             Transforms.TransformManager.Initialize();
// #endif
//         }

//         protected override void OnUpdate() { }

//         protected override void OnDestroy()
//         {
//             if (TweenWorld.World != World) return;
//             if (TweenWorld.ArchetypeStorageRef.IsCreated) TweenWorld.ArchetypeStorageRef.Dispose();
            
// #if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
//             Transforms.TransformManager.Dispose();
// #endif
//         }
//     }
// }