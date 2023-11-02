#if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
using System.Runtime.CompilerServices;
using Unity.Assertions;
using Unity.Entities;
using Unity.Burst;
using UnityEngine;
using MagicTween.Core.Transforms;
using MagicTween.Plugins;

namespace MagicTween.Core
{
    internal static partial class TweenFactory
    {
        [BurstCompile]
        public static class Transforms
        {
            public static Tween<TValue, TOptions> CreateTo<TValue, TOptions, TPlugin, TTranslator>(Transform target, TValue endValue, float duration)
                where TValue : unmanaged
                where TOptions : unmanaged, ITweenOptions
                where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
                where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
            {
                Assert.IsNotNull(target);
                var controllerId = TweenControllerContainer.GetId<TransformTweenController<TValue, TOptions, TPlugin, TTranslator>>();
                var archetype = ECSCache.ArchetypeStorage.GetTransformTweenArchetype<TValue, TOptions, TPlugin, TTranslator>(ref ECSCache.EntityManager);

                CreateEntity(ref ECSCache.EntityManager, archetype, duration, controllerId, out var entity);
                AddStartAndEndValue(entity, default(TTranslator).GetValueManaged(target), endValue);
                AddTranslationMode(entity, TweenTranslationMode.To);
                AddTarget(entity, target);

                return new Tween<TValue, TOptions>(entity);
            }

            public static Tween<TValue, TOptions> CreateFromTo<TValue, TOptions, TPlugin, TTranslator>(Transform target, TValue startValue, TValue endValue, float duration)
                where TValue : unmanaged
                where TOptions : unmanaged, ITweenOptions
                where TPlugin : unmanaged, ITweenPlugin<TValue, TOptions>
                where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
            {
                Assert.IsNotNull(target);
                var controllerId = TweenControllerContainer.GetId<TransformTweenController<TValue, TOptions, TPlugin, TTranslator>>();
                var archetype = ECSCache.ArchetypeStorage.GetTransformTweenArchetype<TValue, TOptions, TPlugin, TTranslator>(ref ECSCache.EntityManager);

                CreateEntity(ref ECSCache.EntityManager, archetype, duration, controllerId, out var entity);
                AddStartAndEndValue(entity, startValue, endValue);
                AddTranslationMode(entity, TweenTranslationMode.FromTo);
                AddTarget(entity, target);

                return new Tween<TValue, TOptions>(entity);
            }

            public static Tween<TValue, PunchTweenOptions> CreatePunch<TValue, TPlugin, TTranslator>(Transform target, TValue strength, float duration)
                where TValue : unmanaged
                where TPlugin : unmanaged, ITweenPlugin<TValue, PunchTweenOptions>
                where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
            {
                Assert.IsNotNull(target);
                var controllerId = TweenControllerContainer.GetId<TransformTweenController<TValue, PunchTweenOptions, TPlugin, TTranslator>>();
                var archetype = ECSCache.ArchetypeStorage.GetTransformPunchTweenArchetype<TValue, TPlugin, TTranslator>(ref ECSCache.EntityManager);

                CreateEntity(ref ECSCache.EntityManager, archetype, duration, controllerId, out var entity);
                AddPunchComponents(entity, default(TTranslator).GetValueManaged(target), strength);
                AddTranslationMode(entity, TweenTranslationMode.FromTo);
                AddTarget(entity, target);

                return new Tween<TValue, PunchTweenOptions>(entity);
            }

            public static Tween<TValue, ShakeTweenOptions> CreateShake<TValue, TPlugin, TTranslator>(Transform target, TValue strength, float duration)
                where TValue : unmanaged
                where TPlugin : unmanaged, ITweenPlugin<TValue, ShakeTweenOptions>
                where TTranslator : unmanaged, ITransformTweenTranslator<TValue>
            {
                Assert.IsNotNull(target);
                var controllerId = TweenControllerContainer.GetId<TransformTweenController<TValue, ShakeTweenOptions, TPlugin, TTranslator>>();
                var archetype = ECSCache.ArchetypeStorage.GetTransformShakeTweenArchetype<TValue, TPlugin, TTranslator>(ref ECSCache.EntityManager);

                CreateEntity(ref ECSCache.EntityManager, archetype, duration, controllerId, out var entity);
                AddShakeComponents(entity, default(TTranslator).GetValueManaged(target), strength);
                AddTranslationMode(entity, TweenTranslationMode.FromTo);
                AddTarget(entity, target);

                return new Tween<TValue, ShakeTweenOptions>(entity);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void AddTarget(Entity entity, Transform target)
            {
                var instance = TweenTargetTransformPool.Rent();
                instance.target = target;
                instance.instanceId = target.GetInstanceID();
                TransformManager.Register(instance);
                ECSCache.EntityManager.SetComponentData(entity, instance);
            }
        }
    }
}
#endif