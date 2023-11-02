// using Unity.Entities;
// using Unity.Collections;
// using Unity.Burst;
// using MagicTween.Core.Systems;

// namespace MagicTween.Core
// {
//     public static class TweenWorld
//     {
//         public static void Initialize()
//         {
//             _world = World.DefaultGameObjectInjectionWorld;
//             _entityManager.Data = _world.EntityManager;
//             _cleanupSystem = _world.GetExistingSystemManaged<TweenCleanupSystem>();
//             _callbackSystem = _world.GetExistingSystemManaged<TweenCallbackSystem>();
//             ArchetypeStorage.Create(Allocator.Persistent, out _archetypeStorage.Data);
//         }

//         static World _world;
//         static readonly SharedStatic<EntityManager> _entityManager = SharedStatic<EntityManager>.GetOrCreate<SharedStaticEntityManagerTag>();
//         static TweenCleanupSystem _cleanupSystem;
//         static TweenCallbackSystem _callbackSystem;
//         static readonly SharedStatic<ArchetypeStorage> _archetypeStorage = SharedStatic<ArchetypeStorage>.GetOrCreate<SharedStaticArchetypeStorageTag>();

//         readonly struct SharedStaticEntityManagerTag { }
//         readonly struct SharedStaticArchetypeStorageTag { }

//         public static World World => _world;
//         public static EntityManager EntityManager => _entityManager.Data;
//         public static ref EntityManager EntityManagerRef => ref _entityManager.Data;
//         public static TweenCleanupSystem CleanupSystem => _cleanupSystem;
//         public static TweenCallbackSystem CallbackSystem => _callbackSystem;
//         internal static ref ArchetypeStorage ArchetypeStorageRef => ref _archetypeStorage.Data;
//     }
// }