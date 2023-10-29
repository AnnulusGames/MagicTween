using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace MagicTween.Core
{
    [BurstCompile]
    public static class TweenControllerContainer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterControllers()
        {
            Register<DelegateTweenController<float, FloatTweenPlugin>>();
            Register<DelegateTweenController<float2, Float2TweenPlugin>>();
            Register<DelegateTweenController<float3, Float3TweenPlugin>>();
            Register<DelegateTweenController<float4, Float4TweenPlugin>>();
            Register<DelegateTweenController<double, DoubleTweenPlugin>>();
            Register<DelegateTweenController<double2, Double2TweenPlugin>>();
            Register<DelegateTweenController<double3, Double3TweenPlugin>>();
            Register<DelegateTweenController<double4, Double4TweenPlugin>>();
            Register<DelegateTweenController<int, IntTweenPlugin>>();
            Register<DelegateTweenController<int2, Int2TweenPlugin>>();
            Register<DelegateTweenController<int3, Int3TweenPlugin>>();
            Register<DelegateTweenController<int4, Int4TweenPlugin>>();
            Register<DelegateTweenController<long, LongTweenPlugin>>();
            Register<DelegateTweenController<quaternion, QuaternionTweenPlugin>>();
            Register<DelegateTweenController<float, PunchTweenPlugin>>();
            Register<DelegateTweenController<float2, Punch2TweenPlugin>>();
            Register<DelegateTweenController<float3, Punch3TweenPlugin>>();
            Register<DelegateTweenController<float, ShakeTweenPlugin>>();
            Register<DelegateTweenController<float2, Shake2TweenPlugin>>();
            Register<DelegateTweenController<float3, Shake3TweenPlugin>>();
            Register<DelegateTweenController<float3, PathTweenPlugin>>();

            Register<NoAllocDelegateTweenController<float, FloatTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float2, Float2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, Float3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float4, Float4TweenPlugin>>();
            Register<NoAllocDelegateTweenController<double, DoubleTweenPlugin>>();
            Register<NoAllocDelegateTweenController<double2, Double2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<double3, Double3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<double4, Double4TweenPlugin>>();
            Register<NoAllocDelegateTweenController<int, IntTweenPlugin>>();
            Register<NoAllocDelegateTweenController<int2, Int2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<int3, Int3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<int4, Int4TweenPlugin>>();
            Register<NoAllocDelegateTweenController<long, LongTweenPlugin>>();
            Register<NoAllocDelegateTweenController<quaternion, QuaternionTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float, PunchTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float2, Punch2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, Punch3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float, ShakeTweenPlugin>>();
            Register<NoAllocDelegateTweenController<float2, Shake2TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, Shake3TweenPlugin>>();
            Register<NoAllocDelegateTweenController<float3, PathTweenPlugin>>();

            Register<StringDelegateTweenController>();
            Register<SequenceTweenController>();
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