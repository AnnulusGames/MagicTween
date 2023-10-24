using Unity.Entities;
using Unity.Collections;

namespace MagicTween.Core
{
    public static class TweenWorld
    {
        public static void Initialize()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _entityManager = _world.EntityManager;
            _cleanupSystem = _world.GetExistingSystemManaged<TweenCleanupSystem>();
            ArchetypeStorage.Create(Allocator.Persistent, out _archetypeStorage);
        }

        static World _world;
        static EntityManager _entityManager;
        static TweenCleanupSystem _cleanupSystem;
        static ArchetypeStorage _archetypeStorage;

        public static World World => _world;
        public static EntityManager EntityManager => _entityManager;
        public static ref EntityManager EntityManagerRef => ref _entityManager;
        public static TweenCleanupSystem CleanupSystem => _cleanupSystem;
        internal static ref ArchetypeStorage ArchetypeStorageRef => ref _archetypeStorage;
    }
}