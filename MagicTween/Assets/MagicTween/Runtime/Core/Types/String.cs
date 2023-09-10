using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

[assembly: RegisterGenericComponentType(typeof(TweenValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<StringTweenOptions>))]
[assembly: RegisterGenericComponentType(typeof(TweenPropertyAccessor<string>))]

namespace MagicTween.Core
{
    // TODO: Support for Unsafe Tween.To methods

    public readonly partial struct StringTweenAspect : IAspect
    {
        readonly RefRW<TweenStartValue<UnsafeText>> startRefRW;
        readonly RefRW<TweenEndValue<UnsafeText>> endRefRW;
        readonly RefRW<TweenValue<UnsafeText>> currentRefRW;
        readonly RefRO<TweenOptions<StringTweenOptions>> optionsRefRO;
        readonly RefRW<StringTweenCustomScrambleChars> customScrambleCharsRefRW;

        public ref UnsafeText startValue => ref startRefRW.ValueRW.value;
        public ref UnsafeText endValue => ref endRefRW.ValueRW.value;
        public ref UnsafeText currentValue => ref currentRefRW.ValueRW.value;
        public ref UnsafeText customScrambleChars => ref customScrambleCharsRefRW.ValueRW.customChars;
        public bool richTextEnabled => optionsRefRO.ValueRO.options.richTextEnabled;
        public ScrambleMode scrambleMode => optionsRefRO.ValueRO.options.scrambleMode;
    }

    public struct StringTweenOptions : ITweenOptions
    {
        public ScrambleMode scrambleMode;
        public bool richTextEnabled;
    }

    public struct StringTweenCustomScrambleChars : IComponentData
    {
        public UnsafeText customChars;
    }

    // Evaluate() returns new NaviteText(Allocator.Temp)
    public readonly struct StringTweenPlugin : ITweenPlugin<UnsafeText>
    {
        public UnsafeText Evaluate(in Entity entity, float t, bool isRelative, bool isFrom)
        {
            var startValue = TweenWorld.EntityManager.GetComponentData<TweenStartValue<UnsafeText>>(entity).value;
            var endValue = TweenWorld.EntityManager.GetComponentData<TweenEndValue<UnsafeText>>(entity).value;
            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(entity).options;
            var customChars = TweenWorld.EntityManager.GetComponentData<StringTweenCustomScrambleChars>(entity).customChars;
            return EvaluateCore(startValue, endValue, t, isFrom, options.richTextEnabled, options.scrambleMode, customChars);
        }

        public static UnsafeText EvaluateCore(UnsafeText startValue, UnsafeText endValue, float t, bool isFrom, bool richTextEnabled, ScrambleMode scrambleMode, UnsafeText customChars)
        {
            if (isFrom) return StringUtils.CreateTweenedText(ref endValue, ref startValue, t, scrambleMode, richTextEnabled, ref customChars, Allocator.Temp);
            else return StringUtils.CreateTweenedText(ref startValue, ref endValue, t, scrambleMode, richTextEnabled, ref customChars, Allocator.Temp);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(MagicTweenUpdateSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct StringTweenSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SystemJob();
            job.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            foreach (var valueAspect in SystemAPI.Query<StringTweenAspect>())
            {
                if (valueAspect.startValue.IsCreated) valueAspect.startValue.Dispose();
                if (valueAspect.endValue.IsCreated) valueAspect.endValue.Dispose();
                if (valueAspect.currentValue.IsCreated) valueAspect.currentValue.Dispose();
                if (valueAspect.customScrambleChars.IsCreated) valueAspect.customScrambleChars.Dispose();
            }
        }

        [BurstCompile]
        partial struct SystemJob : IJobEntity
        {
            public void Execute(TweenAspect aspect, StringTweenAspect valueAspect)
            {
                if (aspect.status == TweenStatusType.Killed)
                {
                    if (valueAspect.startValue.IsCreated) valueAspect.startValue.Dispose();
                    if (valueAspect.endValue.IsCreated) valueAspect.endValue.Dispose();
                    if (valueAspect.currentValue.IsCreated) valueAspect.currentValue.Dispose();
                    if (valueAspect.customScrambleChars.IsCreated) valueAspect.customScrambleChars.Dispose();
                    return;
                }

                if (!valueAspect.currentValue.IsCreated) return;

                var text = StringTweenPlugin.EvaluateCore(
                    valueAspect.startValue,
                    valueAspect.endValue,
                    aspect.progress,
                    aspect.inverted,
                    valueAspect.richTextEnabled,
                    valueAspect.scrambleMode,
                    valueAspect.customScrambleChars
                );
                valueAspect.currentValue.CopyFrom(text);
                text.Dispose();
            }
        }
    }

    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class LambdaStringTweenTranslationSystem : SystemBase
    {
        EntityQuery query1;
        ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
        ComponentTypeHandle<TweenStartValue<UnsafeText>> startValueTypeHandle;
        ComponentTypeHandle<TweenValue<UnsafeText>> valueTypeHandle;
        ComponentTypeHandle<TweenPropertyAccessor<string>> accessorTypeHandle;

        protected override void OnCreate()
        {
            query1 = SystemAPI.QueryBuilder()
                .WithAspect<TweenAspect>()
                .WithAspect<StringTweenAspect>()
                .WithAll<TweenPropertyAccessor<string>>()
                .Build();
            callbackFlagsTypeHandle = SystemAPI.GetComponentTypeHandle<TweenCallbackFlags>(true);
            startValueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenStartValue<UnsafeText>>();
            valueTypeHandle = SystemAPI.GetComponentTypeHandle<TweenValue<UnsafeText>>();
            accessorTypeHandle = SystemAPI.ManagedAPI.GetComponentTypeHandle<TweenPropertyAccessor<string>>(true);
        }

        protected override void OnUpdate()
        {
            CompleteDependency();
            callbackFlagsTypeHandle.Update(this);
            startValueTypeHandle.Update(this);
            valueTypeHandle.Update(this);
            accessorTypeHandle.Update(this);
            var job1 = new SystemJob1()
            {
                entityManager = EntityManager,
                callbackFlagsTypeHandle = callbackFlagsTypeHandle,
                startValueTypeHandle = startValueTypeHandle,
                valueTypeHandle = valueTypeHandle,
                accessorTypeHandle = accessorTypeHandle
            };
            Unity.Entities.Internal.InternalCompilerInterface.JobChunkInterface.RunByRefWithoutJobs(ref job1, query1);
        }

        unsafe struct SystemJob1 : IJobChunk
        {
            public EntityManager entityManager;
            [ReadOnly] public ComponentTypeHandle<TweenCallbackFlags> callbackFlagsTypeHandle;
            public ComponentTypeHandle<TweenStartValue<UnsafeText>> startValueTypeHandle;
            public ComponentTypeHandle<TweenValue<UnsafeText>> valueTypeHandle;
            [ReadOnly] public ComponentTypeHandle<TweenPropertyAccessor<string>> accessorTypeHandle;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var callbackFlagsArrayPtr = chunk.GetComponentDataPtrRO(ref callbackFlagsTypeHandle);
                var startValueArrayPtr = chunk.GetComponentDataPtrRO(ref startValueTypeHandle);
                var valueArrayPtr = chunk.GetComponentDataPtrRW(ref valueTypeHandle);
                var accessors = chunk.GetManagedComponentAccessor(ref accessorTypeHandle, entityManager);

                try
                {
                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var accessor = accessors[i];
                        if (accessor == null) continue;

                        var callbackPtr = callbackFlagsArrayPtr + i;
                        if ((callbackPtr->flags & CallbackFlags.OnKill) == CallbackFlags.OnKill) continue;

                        if ((callbackPtr->flags & CallbackFlags.OnStartUp) == CallbackFlags.OnStartUp)
                        {
                            if (accessor.getter != null)
                            {
                                var ptr = startValueArrayPtr + i;
                                if (ptr->value.IsCreated) ptr->value.CopyFrom(accessor.getter());
                            }
                        }
                        else if ((callbackPtr->flags & (CallbackFlags.OnUpdate | CallbackFlags.OnComplete | CallbackFlags.OnRewind)) != 0)
                        {
                            if (accessor.setter != null)
                            {
                                var ptr = valueArrayPtr + i;
                                if (ptr->value.IsCreated) accessor.setter(ptr->value.ConvertToString());
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debugger.LogExceptionInsideTween(ex);
                }
            }
        }
    }
}