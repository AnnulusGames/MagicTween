using System;
using Unity.Burst;

namespace MagicTween.Core
{
    internal static class TweenControllerContainer
    {
        static ITweenController[] idToController = new ITweenController[32];
        static readonly SharedStatic<short> currentId = SharedStatic<short>.GetOrCreate<CurrentIdSharedStaticTag>();

        static class Cache<T> where T : ITweenController, new()
        {
            static Cache()
            {
                id.Data = currentId.Data;
                currentId.Data++;

                controller = new T();
                if (Id == idToController.Length)
                {
                    Array.Resize(ref idToController, Id * 2);
                }
                idToController[Id] = controller;
            }

            static readonly SharedStatic<short> id = SharedStatic<short>.GetOrCreate<IdSharedStaticTag>();
            readonly struct IdSharedStaticTag { }

            public static short Id
            {
                get => id.Data;
                set => id.Data = value;
            }

            static readonly ITweenController controller;

            public static ITweenController Get() => controller;
        }
        
        readonly struct CurrentIdSharedStaticTag { }

        public static short GetId<T>() where T : ITweenController, new()
        {
            return Cache<T>.Id;
        }

        public static ITweenController FindControllerById(short controllerId)
        {
            if (0 <= controllerId && controllerId < idToController.Length) return idToController[controllerId];
            return null;
        }
    }
}