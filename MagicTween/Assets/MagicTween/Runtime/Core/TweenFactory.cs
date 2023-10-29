using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;
using MagicTween.Plugins;
using Unity.Burst;
using System.Runtime.CompilerServices;

namespace MagicTween.Core
{
    using static TweenWorld;

    [BurstCompile]
    public static class TweenFactory
    {
        public static Tween<TValue, TOptions> CreateToTween<TValue, TOptions, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateTweenArchetype<TValue, TOptions>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddPropertyAccessor<TValue, TPlugin>(entity, getter(), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateFromToTween<TValue, TOptions, TPlugin>(
            in TValue startValue, in TValue endValue, float duration, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateTweenArchetype<TValue, TOptions>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddPropertyAccessor<TValue, TPlugin>(entity, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateToTweenNoAlloc<TObject, TValue, TOptions, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, in TValue endValue, float duration)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateNoAllocTweenArchetype<TValue, TOptions>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<NoAllocDelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddPropertyAccessorNoAlloc<TObject, TValue, TPlugin>(entity, target, getter(target), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }
        public static Tween<TValue, TOptions> CreateFromToTweenNoAlloc<TObject, TValue, TOptions, TPlugin>(
            TObject target, in TValue startValue, in TValue endValue, float duration, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateNoAllocTweenArchetype<TValue, TOptions>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<NoAllocDelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddPropertyAccessorNoAlloc<TObject, TValue, TPlugin>(entity, target, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreatePunchTween<TValue, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue strength, float duration)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegatePunchTweenArchetype<TValue>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            EntityManager.SetComponentData(entity, new TweenOptions<PunchTweenOptions>
            {
                value = new PunchTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f
                }
            });
            EntityManager.SetComponentData(entity, new VibrationStrength<TValue>() { value = strength });
            EntityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter() });
            EntityManager.SetComponentData(entity, TweenDelegatesPool<TValue>.Rent(getter, setter));

            return new Tween<TValue, PunchTweenOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreatePunchTweenNoAlloc<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegatePunchNoAllocTweenArchetype<TValue>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            EntityManager.SetComponentData(entity, new TweenOptions<PunchTweenOptions>
            {
                value = new PunchTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f
                }
            });
            EntityManager.SetComponentData(entity, new VibrationStrength<TValue>() { value = strength });
            EntityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter(target) });
            EntityManager.SetComponentData(entity, TweenDelegatesNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));

            return new Tween<TValue, PunchTweenOptions>(entity);
        }

        public static Tween<TValue, ShakeTweenOptions> CreateShakeTween<TValue, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue strength, float duration)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateShakeTweenArchetype<TValue>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            EntityManager.SetComponentData(entity, new TweenOptions<ShakeTweenOptions>
            {
                value = new ShakeTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f,
                    randomSeed = 0
                }
            });
            EntityManager.SetComponentData(entity, new VibrationStrength<TValue>() { value = strength });
            EntityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter() });
            EntityManager.SetComponentData(entity, TweenDelegatesPool<TValue>.Rent(getter, setter));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<TValue, ShakeTweenOptions> CreateShakeTweenNoAlloc<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            var archetype = ArchetypeStorageRef.GetDelegateShakeNoAllocTweenArchetype<TValue>(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<TValue, TPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            EntityManagerRef.SetComponentData(entity, new TweenOptions<ShakeTweenOptions>
            {
                value = new ShakeTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f,
                    randomSeed = 0
                }
            });
            EntityManager.SetComponentData(entity, new VibrationStrength<TValue>() { value = strength });
            EntityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter(target) });

            EntityManager.SetComponentData(entity, TweenDelegatesNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<UnsafeText, StringTweenOptions> CreateStringToTween(TweenGetter<string> getter, TweenSetter<string> setter, string endValue, float duration)
        {
            var archetype = ArchetypeStorageRef.GetStringLambdaTweenArchetype(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<StringDelegateTweenController>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            var start = new TweenStartValue<UnsafeText>()
            {
                value = new UnsafeText(0, Allocator.Persistent)
            };

            var endValueText = new UnsafeText(System.Text.Encoding.UTF8.GetByteCount(endValue), Allocator.Persistent);
            endValueText.CopyFrom(endValue);
            var end = new TweenEndValue<UnsafeText>()
            {
                value = endValueText
            };

            var value = new TweenValue<UnsafeText>()
            {
                value = new UnsafeText(endValue.Length, Allocator.Persistent)
            };

            EntityManager.SetComponentData(entity, start);
            EntityManager.SetComponentData(entity, end);
            EntityManager.SetComponentData(entity, value);
            EntityManager.SetComponentData(entity, TweenDelegatesPool<string>.Rent(getter, setter));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }
        public static Tween<UnsafeText, StringTweenOptions> CreateStringFromToTween(TweenSetter<string> setter, string startValue, string endValue, float duration)
        {
            var archetype = ArchetypeStorageRef.GetStringLambdaTweenArchetype(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<StringDelegateTweenController>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            var startValueText = new UnsafeText(System.Text.Encoding.UTF8.GetByteCount(startValue), Allocator.Persistent);
            startValueText.CopyFrom(startValue);
            var start = new TweenStartValue<UnsafeText>()
            {
                value = startValueText
            };

            var endValueText = new UnsafeText(System.Text.Encoding.UTF8.GetByteCount(endValue), Allocator.Persistent);
            endValueText.CopyFrom(endValue);
            var end = new TweenEndValue<UnsafeText>()
            {
                value = endValueText
            };

            var value = new TweenValue<UnsafeText>()
            {
                value = new UnsafeText(endValue.Length, Allocator.Persistent)
            };

            EntityManager.SetComponentData(entity, start);
            EntityManager.SetComponentData(entity, end);
            EntityManager.SetComponentData(entity, value);
            EntityManager.SetComponentData(entity, TweenDelegatesPool<string>.Rent(null, setter));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }

        public unsafe static Tween<float3, PathTweenOptions> CreatePathTween(TweenGetter<float3> getter, TweenSetter<float3> setter, float3[] points, float duration)
        {
            var archetype = ArchetypeStorageRef.GetDelegatePathTweenArchetype(ref EntityManagerRef);
            var controllerId = TweenControllerContainer.GetId<DelegateTweenController<float3, PathTweenPlugin>>();

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            var buffer = EntityManager.GetBuffer<PathPoint>(entity);
            buffer.Resize(points.Length + 1, NativeArrayOptions.UninitializedMemory);

            fixed (float3* src = &points[0])
            {
                UnsafeUtility.MemCpy((float3*)buffer.AsNativeArray().GetUnsafePtr() + 1, src, points.Length * sizeof(float3));
            }

            EntityManager.SetComponentData(entity, TweenDelegatesPool<float3>.Rent(getter, setter));

            return new Tween<float3, PathTweenOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateObjectFromTo<TValue, TOptions, TPlugin, TObject, TTranslator>(TObject target, in TValue startValue, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
            where TObject : class
            where TTranslator : unmanaged, ITweenTranslatorManaged<TValue, TObject>
        {
            var controllerId = TweenControllerContainer.GetId<ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>();
            var archetype = ArchetypeStorageRef.GetObjectTweenArchetype<TValue, TOptions, TObject, TTranslator>(ref EntityManagerRef);

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddStartAndEndValue(entity, startValue, endValue);
            AddTargetObjectComponent(ref EntityManagerRef, entity, target);
            EntityManagerRef.SetComponentData(entity, new TweenTranslationModeData(TweenTranslationMode.FromTo));

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateObjectTo<TValue, TOptions, TPlugin, TObject, TTranslator>(TObject target, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
            where TObject : class
            where TTranslator : unmanaged, ITweenTranslatorManaged<TValue, TObject>
        {
            var controllerId = TweenControllerContainer.GetId<ObjectTweenController<TValue, TPlugin, TObject, TTranslator>>();
            var archetype = ArchetypeStorageRef.GetObjectTweenArchetype<TValue, TOptions, TObject, TTranslator>(ref EntityManagerRef);

            CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            AddStartAndEndValue(entity, default(TTranslator).GetValue(target), endValue);
            AddTargetObjectComponent(ref EntityManagerRef, entity, target);
            EntityManagerRef.SetComponentData(entity, new TweenTranslationModeData(TweenTranslationMode.To));

            return new Tween<TValue, TOptions>(entity);
        }

        [BurstCompile]
        public static class ECS
        {
            [BurstCompile]
            public static Tween<TValue, TOptions> CreateFromTo<TValue, TOptions, TPlugin, TComponent, TTranslator>(in Entity target, in TValue startValue, in TValue endValue, float duration)
                where TValue : unmanaged
                where TOptions : unmanaged, ITweenOptions
                where TPlugin : unmanaged, ITweenPluginBase<TValue>
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
            {
                var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>();
                var archetype = ArchetypeStorageRef.GetEntityTweenArchetype<TValue, TOptions, TComponent, TTranslator>(ref EntityManagerRef);

                CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
                AddEntityTweenComponents(ref EntityManagerRef, entity, startValue, endValue, target);

                EntityManagerRef.SetComponentData(entity, new TweenTranslationModeData(TweenTranslationMode.FromTo));

                return new Tween<TValue, TOptions>(entity);
            }

            [BurstCompile]
            public static Tween<TValue, TOptions> CreateTo<TValue, TOptions, TPlugin, TComponent, TTranslator>(in Entity target, in TValue endValue, float duration)
                where TValue : unmanaged
                where TOptions : unmanaged, ITweenOptions
                where TPlugin : unmanaged, ITweenPluginBase<TValue>
                where TComponent : unmanaged, IComponentData
                where TTranslator : unmanaged, ITweenTranslator<TValue, TComponent>
            {
                var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin, TComponent, TTranslator>>();

                var archetype = ArchetypeStorageRef.GetEntityTweenArchetype<TValue, TOptions, TComponent, TTranslator>(ref EntityManagerRef);

                CreateEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
                AddEntityTweenComponents(ref EntityManagerRef, entity, endValue, target);

                EntityManagerRef.SetComponentData(entity, new TweenTranslationModeData(TweenTranslationMode.To));
                return new Tween<TValue, TOptions>(entity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tween CreateUnitTween(float duration)
        {
            CreateUnitTweenCore(ref EntityManagerRef, ref ArchetypeStorageRef, duration, out var tween);
            return tween;
        }

        static void CreateUnitTweenCore(ref EntityManager entityManager, ref ArchetypeStorage archetypeStorage, float duration, out Tween tween)
        {
            var archetype = archetypeStorage.GetUnitTweenArchetype(ref entityManager);
            var controllerId = TweenControllerContainer.GetId<UnitTweenController>();

            CreateEntity(ref entityManager, archetype, duration, controllerId, out var entity);
            tween = new Tween(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sequence CreateSequence()
        {
            CreateSequenceCore(ref EntityManagerRef, ref ArchetypeStorageRef, out var sequence);
            return sequence;
        }

        static void CreateSequenceCore(ref EntityManager entityManager, ref ArchetypeStorage archetypeStorage, out Sequence sequence)
        {
            var archetype = archetypeStorage.GetSequenceArchetype(ref entityManager);
            var controllerId = TweenControllerContainer.GetId<SequenceTweenController>();

            CreateEntity(ref entityManager, archetype, 0f, controllerId, out var entity);
            sequence = new Sequence(entity);
        }

        [BurstCompile]
        static void CreateEntity(ref EntityManager entityManager, in EntityArchetype archetype, float duration, short controllerId, out Entity entity)
        {
            entity = entityManager.CreateEntity(archetype);

            if (MagicTweenSettings.defaultAutoPlay) entityManager.SetComponentData(entity, new TweenParameterAutoPlay(true));
            if (MagicTweenSettings.defaultAutoKill) entityManager.SetComponentData(entity, new TweenParameterAutoKill(true));
            entityManager.SetComponentData(entity, new TweenParameterDuration(duration));
            entityManager.SetComponentData(entity, new TweenParameterPlaybackSpeed(1f));
            entityManager.SetComponentData(entity, new TweenParameterLoops(1));
            entityManager.SetComponentData(entity, new TweenParameterIgnoreTimeScale(MagicTweenSettings.defaultIgnoreTimeScale));
            if (MagicTweenSettings.defaultEase != Ease.Linear) entityManager.SetComponentData(entity, new TweenParameterEase(MagicTweenSettings.defaultEase));
            if (MagicTweenSettings.defaultLoopType != LoopType.Restart) entityManager.SetComponentData(entity, new TweenParameterLoopType(MagicTweenSettings.defaultLoopType));
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
        }

        [BurstCompile]
        static void AddStartAndEndValue<TValue>(in Entity entity, in TValue startValue, in TValue endValue)
            where TValue : unmanaged
        {
            EntityManagerRef.SetComponentData(entity, new TweenStartValue<TValue>() { value = startValue });
            EntityManagerRef.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddPropertyAccessor<TValue, TPlugin>(
            in Entity entity, in TValue startValue, in TValue endValue, TweenGetter<TValue> getter, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            AddStartAndEndValue(entity, startValue, endValue);
            EntityManagerRef.SetComponentData(entity, TweenDelegatesPool<TValue>.Rent(getter, setter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddPropertyAccessorNoAlloc<TObject, TValue, TPlugin>(
            in Entity entity, TObject target, in TValue startValue, in TValue endValue, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPluginBase<TValue>
        {
            AddStartAndEndValue(entity, startValue, endValue);
            EntityManagerRef.SetComponentData(entity, TweenDelegatesNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));
        }

        [BurstCompile]
        static void AddEntityTweenComponents<TValue>(
            ref EntityManager entityManager, in Entity entity, in TValue startValue, in TValue endValue, in Entity target)
            where TValue : unmanaged
        {
            AddStartAndEndValue(entity, startValue, endValue);
            entityManager.SetComponentData(entity, new TweenTargetEntity() { target = target });
        }

        [BurstCompile]
        static void AddEntityTweenComponents<TValue>(
            ref EntityManager entityManager, in Entity entity, in TValue endValue, in Entity target)
            where TValue : unmanaged
        {
            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, new TweenTargetEntity() { target = target });
        }

        static void AddTargetObjectComponent(ref EntityManager entityManager, in Entity entity, object target)
        {
            var targetComponent = TweenTargetObjectPool.Rent();
            targetComponent.target = target;
            entityManager.SetComponentData(entity, targetComponent);
        }
    }
}