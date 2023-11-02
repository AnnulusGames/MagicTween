using MagicTween.Core.Systems;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Core
{
    internal static class ECSCache
    {
        static World _world;
        static readonly SharedStatic<EntityManager> _entityManager = SharedStatic<EntityManager>.GetOrCreate<SharedStaticEntityManagerKey>();
        static TweenCleanupSystem _cleanupSystem;
        static TweenCallbackSystem _callbackSystem;
        static readonly SharedStatic<ArchetypeStorage> _archetypeStorage = SharedStatic<ArchetypeStorage>.GetOrCreate<SharedStaticArchetypeStorageKey>();

        readonly struct SharedStaticEntityManagerKey { }
        readonly struct SharedStaticArchetypeStorageKey { }

        public static World World => _world;
        public static ref EntityManager EntityManager => ref _entityManager.Data;
        public static ref ArchetypeStorage ArchetypeStorage => ref _archetypeStorage.Data;

        public static TweenCleanupSystem CleanupSystem => _cleanupSystem;
        public static TweenCallbackSystem CallbackSystem => _callbackSystem;

        public static void Create(World world)
        {
            _world = world;
            _entityManager.Data = world.EntityManager;
            ArchetypeStorage.Create(Allocator.Persistent, out _archetypeStorage.Data);
            _cleanupSystem = world.GetExistingSystemManaged<TweenCleanupSystem>();
            _callbackSystem = world.GetExistingSystemManaged<TweenCallbackSystem>();
        }

        public static void Dispose()
        {
            if (ArchetypeStorage.IsCreated) ArchetypeStorage.Dispose();
        }
    }
}