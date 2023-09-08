using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace MagicTween.Core
{
    internal static class ArchetypeStore
    {
        readonly struct LambdaTweenTypeKey<TValue, TOptions> { }
        readonly struct UnsafeLambdaTweenTypeKey<TValue, TOptions> { }
        readonly struct VibrationTweenTypeKey<TValue, TOptions> { }
        readonly struct UnsafeVibrationTweenTypeKey<TValue, TOptions> { }
        readonly struct EntityTweenTypeKey<TValue, TOptions, TTranslator> { }
        readonly struct UnitTweenTypeKey { }
        readonly struct SequenceTypeKey { }

        static NativeArray<ComponentType> coreComponentTypes;
        readonly static Dictionary<Type, EntityArchetype> cache = new();

        public static void Initialize()
        {
            coreComponentTypes = new NativeArray<ComponentType>(new ComponentType[]
            {
                typeof(TweenInverted),
                typeof(TweenStatus),
                typeof(TweenPosition),
                typeof(TweenProgress),
                typeof(TweenClip),
                typeof(TweenPlaybackSpeed),
                typeof(TweenPlaySettings),
                typeof(TweenEasing),
                typeof(TweenParameters),
                typeof(TweenId),
                typeof(TweenStartedFlag),
                typeof(TweenCallbackFlags),
                typeof(TweenControllerReference),
                typeof(TweenRootFlag),
                typeof(TweenAccessorFlag)
            }, Allocator.Persistent);
            cache.Clear();
        }

        public static void Dispose()
        {
            coreComponentTypes.Dispose();
        }

        public static EntityArchetype GetLambdaTweenArchetype<TValue, TOptions>()
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var typeKey = typeof(LambdaTweenTypeKey<TValue, TOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenEndValue<TValue>),
                    typeof(TweenOptions<TOptions>),
                    typeof(TweenPropertyAccessor<TValue>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetUnsafeLambdaTweenArchetype<TValue, TOptions>()
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            var typeKey = typeof(UnsafeLambdaTweenTypeKey<TValue, TOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenEndValue<TValue>),
                    typeof(TweenOptions<TOptions>),
                    typeof(TweenPropertyAccessorUnsafe<TValue>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetStringLambdaTweenArchetype()
        {
            var typeKey = typeof(LambdaTweenTypeKey<string, StringTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<UnsafeText>),
                    typeof(TweenStartValue<UnsafeText>),
                    typeof(TweenEndValue<UnsafeText>),
                    typeof(TweenOptions<StringTweenOptions>),
                    typeof(StringTweenCustomScrambleChars),
                    typeof(TweenPropertyAccessor<string>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetPunchLambdaTweenArchetype<TValue>()
            where TValue : unmanaged
        {
            var typeKey = typeof(VibrationTweenTypeKey<TValue, PunchTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenOptions<PunchTweenOptions>),
                    typeof(TweenPropertyAccessor<TValue>),
                    typeof(VibrationStrength<TValue>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetUnsafePunchLambdaTweenArchetype<TValue>()
            where TValue : unmanaged
        {
            var typeKey = typeof(UnsafeVibrationTweenTypeKey<TValue, PunchTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenOptions<PunchTweenOptions>),
                    typeof(TweenPropertyAccessorUnsafe<TValue>),
                    typeof(VibrationStrength<TValue>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }


        public static EntityArchetype GetShakeLambdaTweenArchetype<TValue>()
            where TValue : unmanaged
        {
            var typeKey = typeof(VibrationTweenTypeKey<TValue, ShakeTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenOptions<ShakeTweenOptions>),
                    typeof(TweenPropertyAccessor<TValue>),
                    typeof(VibrationStrength<TValue>),
                    typeof(ShakeRandomState)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetUnsafeShakeLambdaTweenArchetype<TValue>()
            where TValue : unmanaged
        {
            var typeKey = typeof(UnsafeVibrationTweenTypeKey<TValue, ShakeTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenOptions<ShakeTweenOptions>),
                    typeof(TweenPropertyAccessorUnsafe<TValue>),
                    typeof(VibrationStrength<TValue>),
                    typeof(ShakeRandomState)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetPathLambdaTweenArchetype()
        {
            var typeKey = typeof(LambdaTweenTypeKey<float3, PathTweenOptions>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<float3>),
                    typeof(PathPoint),
                    typeof(TweenOptions<PathTweenOptions>),
                    typeof(TweenPropertyAccessor<float3>)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetEntityTweenArchetype<TValue, TOptions, TTranslator>()
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var typeKey = typeof(EntityTweenTypeKey<TValue, TOptions, TTranslator>);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(TweenValue<TValue>),
                    typeof(TweenStartValue<TValue>),
                    typeof(TweenEndValue<TValue>),
                    typeof(TweenOptions<TOptions>),
                    typeof(TTranslator),
                    typeof(TweenTranslationOptionsData)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetUnitTweenArchetype()
        {
            var typeKey = typeof(UnitTweenTypeKey);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                archetype = TweenWorld.EntityManager.CreateArchetype(coreComponentTypes);
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }

        public static EntityArchetype GetSequenceArchetype()
        {
            var typeKey = typeof(SequenceTypeKey);
            if (!cache.TryGetValue(typeKey, out var archetype))
            {
                using var types = new NativeList<ComponentType>(0, Allocator.Temp)
                {
                    typeof(SequenceState),
                    typeof(SequenceEntitiesGroup)
                };
                types.AddRange(coreComponentTypes);
                archetype = TweenWorld.EntityManager.CreateArchetype(types.AsArray());
                cache.Add(typeKey, archetype);
            }
            return archetype;
        }
    }
}