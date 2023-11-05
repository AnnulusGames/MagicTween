using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using MagicTween.Plugins;
using MagicTween.Core.Controllers;

namespace MagicTween.Core
{
    [BurstCompile]
    public static class TweenControllerContainer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterControllers()
        {
            Register<DelegateTweenController<float, PunchTweenOptions, PunchTweenPlugin>>();
            Register<DelegateTweenController<float2, PunchTweenOptions, Punch2TweenPlugin>>();
            Register<DelegateTweenController<float3, PunchTweenOptions, Punch3TweenPlugin>>();
            Register<DelegateTweenController<float, ShakeTweenOptions, ShakeTweenPlugin>>();
            Register<DelegateTweenController<float2, ShakeTweenOptions, Shake2TweenPlugin>>();
            Register<DelegateTweenController<float3, ShakeTweenOptions, Shake3TweenPlugin>>();
            Register<DelegateTweenController<float3, PathTweenOptions, PathTweenPlugin>>();

            Register<NoAllocDelegateTweenController<float, PunchTweenOptions, PunchTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float2, PunchTweenOptions, Punch2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, PunchTweenOptions, Punch3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float, ShakeTweenOptions, ShakeTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float2, ShakeTweenOptions, Shake2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, ShakeTweenOptions, Shake3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, PathTweenOptions, PathTweenPlugin>>();

            Register<StringDelegateTweenController>();

            Register<SequenceController>();
            Register<UnitTweenController>();
        }

        static ITweenController[] idToController = new ITweenController[32];
        static readonly SharedStatic<short> currentId = SharedStatic<short>.GetOrCreate<CurrentIdSharedStaticTag>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Register<T>() where T : ITweenController, new()
        {
            Container<T>.Register();
        }

        static class Container<T> where T : ITweenController, new()
        {
            public static void Register()
            {
                if (isRegistered.Data) return;

                id.Data = currentId.Data;
                currentId.Data++;

                controller = new T();
                if (Id == idToController.Length)
                {
                    Array.Resize(ref idToController, Id * 2);
                }
                idToController[Id] = controller;

                isRegistered.Data = true;
            }

            static readonly SharedStatic<short> id = SharedStatic<short>.GetOrCreate<IdSharedStaticTag>();
            static readonly SharedStatic<bool> isRegistered = SharedStatic<bool>.GetOrCreate<IsRegisteredSharedStaticTag>();
            readonly struct IdSharedStaticTag { }
            readonly struct IsRegisteredSharedStaticTag { }

            public static short Id => id.Data;
            public static bool IsRegistered => isRegistered.Data;

            static ITweenController controller;
            public static ITweenController Get() => controller;
        }
        
        readonly struct CurrentIdSharedStaticTag { }

        [BurstCompile]
        public static short GetId<T>() where T : ITweenController, new()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!Container<T>.IsRegistered) throw new Exception("Controller Type: " + typeof(T).FullName + " is not registered.");
#endif
            return Container<T>.Id;
        }

        public static ITweenController FindControllerById(short controllerId)
        {
            if (0 <= controllerId && controllerId < idToController.Length) return idToController[controllerId];
            return null;
        }
    }
}