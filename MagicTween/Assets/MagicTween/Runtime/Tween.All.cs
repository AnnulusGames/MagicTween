using Unity.Collections;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween
{
    partial struct Tween
    {
        static EntityQuery CreateAllTweenEntityQuery()
        {
            return TweenWorld.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<TweenControllerReference>(),
                ComponentType.ReadOnly<TweenRootFlag>()
            );
        }

        public static void PlayAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Play(entities[i]);
            }
        }

        public static void PlayAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Play(entities[i]);
            }
        }

        public static void PlayAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }


        public static void PlayAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }

        public static void RestartAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Restart(entities[i]);
            }
        }

        public static void RestartAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Restart(entities[i]);
            }
        }

        public static void RestartAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Restart(entities[i]);
            }
        }


        public static void RestartAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Restart(entities[i]);
            }
        }

        public static void PauseAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Pause(entities[i]);
            }
        }

        public static void PauseAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Pause(entities[i]);
            }
        }

        public static void PauseAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Pause(entities[i]);
            }
        }

        public static void PauseAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Pause(entities[i]);
            }
        }

        public static void CompleteAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Complete(entities[i]);
            }
        }

        public static void CompleteAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Complete(entities[i]);
            }
        }

        public static void CompleteAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Complete(entities[i]);
            }
        }

        public static void CompleteAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Complete(entities[i]);
            }
        }

        public static void KillAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }

        public static void KillAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }

        public static void KillAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }

        public static void KillAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.Kill(entities[i]);
            }
        }

        public static void CompleteAndKillAll()
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.CompleteAndKill(entities[i]);
            }
        }

        public static void CompleteAndKillAll(int id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdInt>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.CompleteAndKill(entities[i]);
            }
        }
        public static void CompleteAndKillAll(string id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.CompleteAndKill(entities[i]);
            }
        }

        public static void CompleteAndKillAll(in FixedString32Bytes id)
        {
            var query = CreateAllTweenEntityQuery();
            using var entities = query.ToEntityArray(Allocator.Temp);
            using var idArray = query.ToComponentDataArray<TweenIdString>(Allocator.Temp);
            using var refArray = query.ToComponentDataArray<TweenControllerReference>(Allocator.Temp);
            for (int i = 0; i < refArray.Length; i++)
            {
                if (idArray[i].value != id) continue;
                var handle = TweenControllerContainer.FindControllerById(refArray[i].controllerId);
                handle?.CompleteAndKill(entities[i]);
            }
        }

        public static void Clear()
        {
            var query = TweenWorld.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<TweenIdInt>()
            );
            TweenWorld.EntityManager.DestroyEntity(query);
            TweenWorld.World.GetExistingSystemManaged<TweenCleanupSystem>().ClearQueue();
        }
    }
}