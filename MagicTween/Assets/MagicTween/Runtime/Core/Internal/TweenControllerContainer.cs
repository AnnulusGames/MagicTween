using System;
using System.Collections.Generic;

namespace MagicTween.Core
{
    internal static class TweenControllerContainer
    {
        static readonly Dictionary<short, ITweenController> idToController = new();
        static readonly Dictionary<Type, (ITweenController controller, short id)> typeDict = new();
        static short currentId = 0;

        public static void Clear()
        {
            idToController.Clear();
            typeDict.Clear();
            currentId = 0;
        }

        static short AddAndGetId<T>(T instance) where T : ITweenController
        {
            idToController.Add(currentId, instance);
            typeDict.Add(typeof(T), (instance, currentId));
            var prevId = currentId;
            currentId++;
            return prevId;
        }

        public static short GetId<T>() where T : ITweenController, new()
        {
            if (!typeDict.TryGetValue(typeof(T), out var handle))
            {
                var controller = new T();
                return AddAndGetId(controller);
            }
            return handle.id;
        }

        public static ITweenController FindControllerById(short controllerId)
        {
            if (idToController.TryGetValue(controllerId, out var handle)) return handle;
            return null;
        }
    }
}