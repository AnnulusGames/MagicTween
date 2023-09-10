using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    public static class TweenFactory
    {
        public static Tween<TValue, TOptions> CreateToTween<TValue, TOptions, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetLambdaTweenArchetype<TValue, TOptions>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeLambdaTweenComponents<TValue, TPlugin>(ref entityManager, entity, getter(), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateFromToTween<TValue, TOptions, TPlugin>(
            in TValue startValue, in TValue endValue, float duration, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetLambdaTweenArchetype<TValue, TOptions>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeLambdaTweenComponents<TValue, TPlugin>(ref entityManager, entity, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateToTweenUnsafe<TObject, TValue, TOptions, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, in TValue endValue, float duration)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetUnsafeLambdaTweenArchetype<TValue, TOptions>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(ref entityManager, entity, target, getter(target), endValue, getter, setter);

            return new Tween<TValue, TOptions>(entity);
        }
        public static Tween<TValue, TOptions> CreateFromToTweenUnsafe<TObject, TValue, TOptions, TPlugin>(
            TObject target, in TValue startValue, in TValue endValue, float duration, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetUnsafeLambdaTweenArchetype<TValue, TOptions>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(ref entityManager, entity, target, startValue, endValue, null, setter);

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreatePunchTween<TValue, TPlugin>(
            TweenGetter<TValue> getter, TweenSetter<TValue> setter, in TValue strength, float duration)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetPunchLambdaTweenArchetype<TValue>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            entityManager.SetComponentData(entity, new TweenOptions<PunchTweenOptions>
            {
                options = new PunchTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f
                }
            });
            entityManager.SetComponentData(entity, new VibrationStrength<TValue>() { strength = strength });
            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter() });

            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TweenPropertyAccessor<TValue>(getter, setter));

            return new Tween<TValue, PunchTweenOptions>(entity);
        }

        public static Tween<TValue, PunchTweenOptions> CreateUnsafePunchTween<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetUnsafePunchLambdaTweenArchetype<TValue>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            entityManager.SetComponentData(entity, new TweenOptions<PunchTweenOptions>
            {
                options = new PunchTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f
                }
            });
            entityManager.SetComponentData(entity, new VibrationStrength<TValue>() { strength = strength });
            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter(target) });

            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TweenPropertyAccessorUnsafe<TValue>(
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
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetShakeLambdaTweenArchetype<TValue>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            entityManager.SetComponentData(entity, new TweenOptions<ShakeTweenOptions>
            {
                options = new ShakeTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f,
                    randomSeed = 0
                }
            });
            entityManager.SetComponentData(entity, new VibrationStrength<TValue>() { strength = strength });
            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter() });

            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TweenPropertyAccessor<TValue>(getter, setter));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<TValue, ShakeTweenOptions> CreateUnsafeShakeTween<TObject, TValue, TPlugin>(
            TObject target, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter, TValue strength, float duration)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetUnsafeShakeLambdaTweenArchetype<TValue>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            entityManager.SetComponentData(entity, new TweenOptions<ShakeTweenOptions>
            {
                options = new ShakeTweenOptions()
                {
                    frequency = 10,
                    dampingRatio = 1f,
                    randomSeed = 0
                }
            });
            entityManager.SetComponentData(entity, new VibrationStrength<TValue>() { strength = strength });
            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = getter(target) });

            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TweenPropertyAccessorUnsafe<TValue>(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));

            return new Tween<TValue, ShakeTweenOptions>(entity);
        }

        public static Tween<UnsafeText, StringTweenOptions> CreateStringToTween(TweenGetter<string> getter, TweenSetter<string> setter, string endValue, float duration)
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetStringLambdaTweenArchetype();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

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

            var controllerId = TweenControllerContainer.GetId<StringLambdaTweenController>();

            entityManager.SetComponentData(entity, start);
            entityManager.SetComponentData(entity, end);
            entityManager.SetComponentData(entity, value);
            entityManager.SetComponentData(entity, new TweenPropertyAccessor<string>(getter, setter));
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }
        public static Tween<UnsafeText, StringTweenOptions> CreateStringFromToTween(TweenSetter<string> setter, string startValue, string endValue, float duration)
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetStringLambdaTweenArchetype();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

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

            var controllerId = TweenControllerContainer.GetId<StringLambdaTweenController>();

            entityManager.SetComponentData(entity, start);
            entityManager.SetComponentData(entity, end);
            entityManager.SetComponentData(entity, value);
            entityManager.SetComponentData(entity, new TweenPropertyAccessor<string>(null, setter));
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            return new Tween<UnsafeText, StringTweenOptions>(entity);
        }

        public unsafe static Tween<float3, PathTweenOptions> CreatePathTween(TweenGetter<float3> getter, TweenSetter<float3> setter, float3[] points, float duration)
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetPathLambdaTweenArchetype();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            var buffer = entityManager.GetBuffer<PathPoint>(entity);
            buffer.Resize(points.Length + 1, NativeArrayOptions.UninitializedMemory);

            fixed (float3* src = &points[0])
            {
                UnsafeUtility.MemCpy((float3*)buffer.AsNativeArray().GetUnsafePtr() + 1, src, points.Length * sizeof(float3));
            }

            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<float3, PathTweenPlugin>>();

            entityManager.SetComponentData(entity, new TweenPropertyAccessor<float3>(getter, setter));
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            return new Tween<float3, PathTweenOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateEntityFromToTween<TValue, TOptions, TPlugin, TTranslator>(in Entity target, in TValue startValue, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetEntityTweenArchetype<TValue, TOptions, TTranslator>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeEntityTweenComponents<TValue, TPlugin, TTranslator>(ref entityManager, entity, startValue, endValue, target);

            entityManager.SetComponentData(entity, new TweenTranslationOptionsData() { options = TweenTranslationOptions.FromTo });

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween<TValue, TOptions> CreateEntityToTween<TValue, TOptions, TPlugin, TTranslator>(in Entity target, in TValue endValue, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetEntityTweenArchetype<TValue, TOptions, TTranslator>();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);
            InitializeEntityTweenComponents<TValue, TPlugin, TTranslator>(ref entityManager, entity, endValue, target);

            entityManager.SetComponentData(entity, new TweenTranslationOptionsData() { options = TweenTranslationOptions.To });

            return new Tween<TValue, TOptions>(entity);
        }

        public static Tween CreateUnitTween(float duration)
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetUnitTweenArchetype();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, duration);

            var controllerId = TweenControllerContainer.GetId<UnitTweenController>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            return new Tween(entity);
        }

        public static Sequence CreateSequence()
        {
            var entityManager = TweenWorld.EntityManager;
            var archetype = ArchetypeStore.GetSequenceArchetype();
            var entity = entityManager.CreateEntity(archetype);

            InitializeCoreComponents(ref entityManager, entity, 0f);

            var controllerId = TweenControllerContainer.GetId<SequenceTweenController>();
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            return new Sequence(entity);
        }

        static void InitializeCoreComponents(ref EntityManager entityManager, in Entity entity, float duration)
        {
            var state = new TweenStatus()
            {
                status = TweenStatusType.WaitingForStart
            };
            var clip = new TweenClip()
            {
                duration = duration,
                loops = 1,
                loopType = MagicTweenSettings.defaultLoopType
            };
            var playbackSpeed = new TweenPlaybackSpeed()
            {
                speed = 1f
            };
            var easing = new TweenEasing()
            {
                ease = MagicTweenSettings.defaultEase
            };

            entityManager.SetComponentData(entity, new TweenAutoPlayFlag(MagicTweenSettings.defaultAutoPlay));
            entityManager.SetComponentData(entity, new TweenAutoKillFlag(MagicTweenSettings.defaultAutoKill));
            entityManager.SetComponentData(entity, new TweenIgnoreTimeScaleFlag(MagicTweenSettings.defaultIgnoreTimeScale));
            entityManager.SetComponentData(entity, state);
            entityManager.SetComponentData(entity, clip);
            entityManager.SetComponentData(entity, playbackSpeed);
            entityManager.SetComponentData(entity, easing);
        }

        static void InitializeLambdaTweenComponents<TValue, TPlugin>(
            ref EntityManager entityManager, in Entity entity, in TValue startValue, in TValue endValue, TweenGetter<TValue> getter, TweenSetter<TValue> setter)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var controllerId = TweenControllerContainer.GetId<LambdaTweenController<TValue, TPlugin>>();

            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = startValue });
            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TweenPropertyAccessor<TValue>(getter, setter));
        }

        static void InitializeUnsafeLambdaTweenComponents<TObject, TValue, TPlugin>(
            ref EntityManager entityManager, in Entity entity, TObject target, in TValue startValue, in TValue endValue, TweenGetter<TObject, TValue> getter, TweenSetter<TObject, TValue> setter)
            where TObject : class
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
        {
            var controllerId = TweenControllerContainer.GetId<UnsafeLambdaTweenController<TValue, TPlugin>>();

            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = startValue });
            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));

            entityManager.SetComponentData(entity, new TweenPropertyAccessorUnsafe<TValue>(
                target,
                UnsafeUtility.As<TweenGetter<TObject, TValue>, TweenGetter<object, TValue>>(ref getter),
                UnsafeUtility.As<TweenSetter<TObject, TValue>, TweenSetter<object, TValue>>(ref setter)
            ));
        }

        static void InitializeEntityTweenComponents<TValue, TPlugin, TTranslator>(
            ref EntityManager entityManager, in Entity entity, in TValue startValue, in TValue endValue, in Entity target)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin>>();

            entityManager.SetComponentData(entity, new TweenStartValue<TValue>() { value = startValue });
            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TTranslator() { TargetEntity = target });
        }

        static void InitializeEntityTweenComponents<TValue, TPlugin, TTranslator>(
            ref EntityManager entityManager, in Entity entity, in TValue endValue, in Entity target)
            where TValue : unmanaged
            where TPlugin : unmanaged, ITweenPlugin<TValue>
            where TTranslator : unmanaged, ITweenTranslatorBase<TValue>
        {
            var controllerId = TweenControllerContainer.GetId<EntityTweenController<TValue, TPlugin>>();

            entityManager.SetComponentData(entity, new TweenEndValue<TValue>() { value = endValue });
            entityManager.SetComponentData(entity, new TweenControllerReference(controllerId));
            entityManager.SetComponentData(entity, new TTranslator() { TargetEntity = target });
        }
    }
}