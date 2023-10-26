using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;
using Unity.Burst;

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
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetLambdaTweenArchetype<TValue, TOptions>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeLambdaTweenComponents<TValue, TPlugin>(entity, getter(), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateFromToTween<TValue, TOptions, TPlugin>(
            in TValue startValue, in TValue endValue, float duration, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetLambdaTweenArchetype<TValue, TOptions>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeLambdaTweenComponents<TValue, TPlugin>(entity, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateToTweenNoAlloc<TObject, TValue, TOptions, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, in TValue endValue, float duration)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetNoAllocLambdaTweenArchetype<TValue, TOptions>();
            var controllerId = TweenControllerContainer.GetId<NoAllocLambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(entity, target, getter(target), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }
        public static Tween<TValue, TOptions> CreateFromToTweenNoAlloc<TObject, TValue, TOptions, TPlugin>(
            TObject target, in TValue startValue, in TValue endValue, float duration, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetNoAllocLambdaTweenArchetype<TValue, TOptions>();
            var controllerId = TweenControllerContainer.GetId<NoAllocLambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(entity, target, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreatePunchTween<TValue, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue strength, float duration)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetPunchLambdaTweenArchetype<TValue>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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
            EntityManager.SetComponentData(entity, TweenPropertyAccessorPool<TValue>.Rent(getter, setter));

            return new Tween<TValue, PunchTweenOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreatePunchTweenNoAlloc<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetNoAllocPunchLambdaTweenArchetype<TValue>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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
            EntityManager.SetComponentData(entity, TweenPropertyAccessorNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));

            return new Tween<TValue, PunchTweenOptions>(entity);
        }

        public static Tween<TValue, ShakeTweenOptions> CreateShakeTween<TValue, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue strength, float duration)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetShakeLambdaTweenArchetype<TValue>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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
            EntityManager.SetComponentData(entity, TweenPropertyAccessorPool<TValue>.Rent(getter, setter));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<TValue, ShakeTweenOptions> CreateShakeTweenNoAlloc<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetNoAllocShakeLambdaTweenArchetype<TValue>();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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

            EntityManager.SetComponentData(entity, TweenPropertyAccessorNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<UnsafeText, StringTweenOptions> CreateStringToTween(TweenGetter<string> getter, TweenSetter<string> setter, string endValue, float duration)
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetStringLambdaTweenArchetype();
            var controllerId = TweenControllerContainer.GetId<StringLambdaTweenController>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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
            EntityManager.SetComponentData(entity, TweenPropertyAccessorPool<string>.Rent(getter, setter));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }
        public static Tween<UnsafeText, StringTweenOptions> CreateStringFromToTween(TweenSetter<string> setter, string startValue, string endValue, float duration)
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetStringLambdaTweenArchetype();
            var controllerId = TweenControllerContainer.GetId<StringLambdaTweenController>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

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
            EntityManager.SetComponentData(entity, TweenPropertyAccessorPool<string>.Rent(null, setter));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }

        public unsafe static Tween<float3, PathTweenOptions> CreatePathTween(TweenGetter<float3> getter, TweenSetter<float3> setter, float3[] points, float duration)
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetPathLambdaTweenArchetype();
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<float3, PathTweenPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);

            var buffer = EntityManager.GetBuffer<PathPoint>(entity);
            buffer.Resize(points.Length + 1, NativeArrayOptions.UninitializedMemory);

            fixed (float3* src = &points[0])
            {
                UnsafeUtility.MemCpy((float3*)buffer.AsNativeArray().GetUnsafePtr() + 1, src, points.Length * sizeof(float3));
            }

            EntityManager.SetComponentData(entity, TweenPropertyAccessorPool<float3>.Rent(getter, setter));

            return new Tween<float3, PathTweenOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateEntityFromToTween<TValue, TOptions, TPlugin, TTranslator>(in Entity target, in TValue startValue, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetEntityTweenArchetype<TValue, TOptions, TTranslator>();
            var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeEntityTweenComponents<TValue, TTranslator>(ref EntityManagerRef, entity, startValue, endValue, target);

            EntityManager.SetComponentData(entity, new TweenTranslationOptionsData(TweenTranslationOptions.FromTo));

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateEntityToTween<TValue, TOptions, TPlugin, TTranslator>(in Entity target, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetEntityTweenArchetype<TValue, TOptions, TTranslator>();
            var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin>>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            InitializeEntityTweenComponents<TValue, TTranslator>(ref EntityManagerRef, entity, endValue, target);

            EntityManager.SetComponentData(entity, new TweenTranslationOptionsData(TweenTranslationOptions.To));

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween CreateUnitTween(float duration)
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetUnitTweenArchetype();
            var controllerId = TweenControllerContainer.GetId<UnitTweenController>();

            CreateTweenEntity(ref EntityManagerRef, archetype, duration, controllerId, out var entity);
            return new Tween(entity);
        }

        public static Sequence CreateSequence()
        {
            var archetype = TweenWorld.ArchetypeStorageRef.GetSequenceArchetype();
            var controllerId = TweenControllerContainer.GetId<SequenceTweenController>();

            CreateTweenEntity(ref EntityManagerRef, archetype, 0f, controllerId, out var entity);
            return new Sequence(entity);
        }

        [BurstCompile]
        static void CreateTweenEntity(ref EntityManager entityManager, in EntityArchetype archetype, float duration, short controllerId, out Entity entity)
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
        static void SetStartAndEndValue<TValue>(in Entity entity, in TValue startValue, in TValue endValue)
            where TValue : unmanaged
        {
            EntityManagerRef.SetComponentData(entity, new TweenStartValue<TValue>() { value = startValue });
            EntityManagerRef.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
        }

        static void InitializeLambdaTweenComponents<TValue, TPlugin>(
            in Entity entity, in TValue startValue, in TValue endValue, TweenGetter<TValue> getter, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            SetStartAndEndValue(entity, startValue, endValue);
            EntityManagerRef.SetComponentData(entity, TweenPropertyAccessorPool<TValue>.Rent(getter, setter));
        }

        static void InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(
            in Entity entity, TObject target, in TValue startValue, in TValue endValue, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            SetStartAndEndValue(entity, startValue, endValue);
            EntityManagerRef.SetComponentData(entity, TweenPropertyAccessorNoAllocPool<TValue>.Rent(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));
        }

        [BurstCompile]
        static void InitializeEntityTweenComponents<TValue, TTranslator>(
            ref EntityManager entityManager, in Entity entity, in TValue startValue, in TValue endValue, in Entity target)
            where TValue : unmanaged
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var translator = default(TTranslator);
            translator.TargetEntity = target;

            SetStartAndEndValue(entity, startValue, endValue);
            entityManager.SetComponentData(entity, translator);
        }
        [BurstCompile]

        static void InitializeEntityTweenComponents<TValue, TTranslator>(
            ref EntityManager entityManager, in Entity entity, in TValue endValue, in Entity target)
            where TValue : unmanaged
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var translator = default(TTranslator);
            translator.TargetEntity = target;

            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, translator);
        }
    }
}