using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;

namespace MagicTween.Core
{
    [BurstCompile]
    public readonly partial struct TweenAspect : IAspect
    {
        public readonly Entity entity;
        readonly RefRW<TweenStatus> statusRefRW;

        readonly RefRW<TweenPosition> positionRefRW;
        readonly RefRW<TweenCompletedLoops> completedLoopsRefRW;
        readonly RefRW<TweenProgress> progressRefRW;

        readonly RefRW<TweenClip> clipRefRW;
        readonly RefRW<TweenPlaybackSpeed> playbackSpeedRefRW;

        readonly RefRW<TweenEasing> easingRefRW;
        readonly RefRW<TweenInvertMode> invertModeRefRW;
        readonly RefRW<TweenId> idRefRW;
    
        readonly RefRW<TweenInvertFlag> invertedFlagRefRW;
        readonly RefRW<TweenStartedFlag> flagsRefRW;
        readonly RefRW<TweenCallbackFlags> callbackFlagsRefRW;

        readonly RefRO<TweenAutoPlayFlag> autoPlayFlagRefRO;
        readonly RefRO<TweenAutoKillFlag> autoKillFlagRefRO;

        readonly RefRO<TweenIgnoreTimeScaleFlag> ignoreTimeScaleFlagRefRO;
        readonly RefRO<TweenIsRelativeFlag> isRelativeFlagRefRO;

        public float position
        {
            get => positionRefRW.ValueRO.value;
            set => positionRefRW.ValueRW.value = value;
        }

        public int completedLoops
        {
            get => completedLoopsRefRW.ValueRO.value;
            set => completedLoopsRefRW.ValueRW.value = value;
        }

        public bool started
        {
            get => flagsRefRW.ValueRO.value;
            set => flagsRefRW.ValueRW.value = value;
        }

        public bool inverted
        {
            get => invertedFlagRefRW.ValueRO.value;
            set => invertedFlagRefRW.ValueRW.value = value;
        }

        public TweenStatusType status
        {
            get => statusRefRW.ValueRO.status;
            set => statusRefRW.ValueRW.status = value;
        }

        public float progress
        {
            get => progressRefRW.ValueRO.value;
            set => progressRefRW.ValueRW.value = value;
        }

        public readonly float duration
        {
            get => clipRefRW.ValueRO.duration;
        }

        public float delay
        {
            get => clipRefRW.ValueRO.delay;
            set => clipRefRW.ValueRW.delay = value;
        }

        public int loops
        {
            get => clipRefRW.ValueRO.loops;
            set => clipRefRW.ValueRW.loops = value;
        }

        public LoopType loopType
        {
            get => clipRefRW.ValueRO.loopType;
            set => clipRefRW.ValueRW.loopType = value;
        }

        public float playbackSpeed
        {
            get => playbackSpeedRefRW.ValueRO.value;
            set => playbackSpeedRefRW.ValueRW.value = value;
        }

        public bool autoPlay => autoPlayFlagRefRO.ValueRO.value;
        public bool autoKill => autoKillFlagRefRO.ValueRO.value;

        public InvertMode invertMode
        {
            get => invertModeRefRW.ValueRO.value;
            set => invertModeRefRW.ValueRW.value = value;
        }

        public bool isRelative => isRelativeFlagRefRO.ValueRO.value;
        public bool ignoreTimeScale => ignoreTimeScaleFlagRefRO.ValueRO.value;

        public int id
        {
            get => idRefRW.ValueRO.id;
            set => idRefRW.ValueRW.id = value;
        }

        public FixedString32Bytes idString
        {
            get => idRefRW.ValueRO.idString;
            set => idRefRW.ValueRW.idString = value;
        }

        public CallbackFlags callbackFlags
        {
            get => callbackFlagsRefRW.ValueRO.flags;
            set => callbackFlagsRefRW.ValueRW.flags = value;
        }

        [BurstCompile]
        public void Update(float currentPosition, ref NativeQueue<Entity>.ParallelWriter parallelWriter)
        {
            UpdateCore(this, currentPosition, ref parallelWriter);
        }

        public void Kill(ref NativeQueue<Entity>.ParallelWriter parallelWriter)
        {
            status = TweenStatusType.Killed;
            if (easingRefRW.ValueRW.customCurve.IsCreated)
            {
                easingRefRW.ValueRW.customCurve.Dispose();
            }
            callbackFlags |= CallbackFlags.OnKill;
            parallelWriter.Enqueue(entity);
        }

        [BurstCompile]
        static void UpdateCore(in TweenAspect aspect, float currentPosition, ref NativeQueue<Entity>.ParallelWriter parallelWriter)
        {
            aspect.callbackFlags = CallbackFlags.None;

            switch (aspect.status)
            {
                case TweenStatusType.Invalid:
                case TweenStatusType.Killed:
                    return;
                case TweenStatusType.WaitingForStart:
                    if (!aspect.autoPlay || currentPosition <= 0f) break;

                    aspect.callbackFlags |= CallbackFlags.OnPlay;
                    aspect.callbackFlags |= CallbackFlags.OnStartUp;

                    if (aspect.delay > 0f)
                    {
                        aspect.status = TweenStatusType.Delayed;
                    }
                    else
                    {
                        aspect.status = TweenStatusType.Playing;
                        aspect.callbackFlags |= CallbackFlags.OnStart;
                        aspect.started = true;
                    }
                    break;
                case TweenStatusType.Completed:
                    if (!aspect.autoKill) break;
                    aspect.Kill(ref parallelWriter);
                    return;
            }

            if (aspect.status is not (TweenStatusType.Playing or TweenStatusType.Delayed or TweenStatusType.RewindCompleted or TweenStatusType.Completed)) return;

            // get current progress  -------------------------------------------------------------------

            float currentTime = currentPosition - aspect.delay;
            float currentProgress;

            int prevCompletedLoops = aspect.completedLoops;
            int currentCompletedLoops;
            int clampedCompletedLoops;

            bool isCompleted;

            if (aspect.duration == 0f)
            {
                isCompleted = currentTime > 0f;

                if (isCompleted)
                {
                    currentProgress = 1f;
                    currentCompletedLoops = aspect.loops;
                }
                else
                {
                    currentProgress = 0f;
                    currentCompletedLoops = currentTime < 0f ? -1 : 0;
                }
                clampedCompletedLoops = aspect.loops < 0 ? math.max(0, currentCompletedLoops) : math.clamp(currentCompletedLoops, 0, aspect.loops);
            }
            else
            {
                currentCompletedLoops = (int)math.floor(currentTime / aspect.duration);
                clampedCompletedLoops = aspect.loops < 0 ? math.max(0, currentCompletedLoops) : math.clamp(currentCompletedLoops, 0, aspect.loops);
                isCompleted = aspect.loops >= 0 && clampedCompletedLoops > aspect.loops - 1;

                if (isCompleted)
                {
                    currentProgress = 1f;
                }
                else
                {
                    float currentLoopTime = currentTime - aspect.duration * clampedCompletedLoops;
                    currentProgress = math.clamp(currentLoopTime / aspect.duration, 0f, 1f);
                }
            }

            // set position and completedLoops --------------------------------------------------------

            aspect.position = math.max(currentPosition, 0f);
            aspect.completedLoops = currentCompletedLoops;

            switch (aspect.loopType)
            {
                case LoopType.Restart:
                    aspect.progress = aspect.easingRefRW.ValueRO.GetEasedValue(currentProgress);
                    break;
                case LoopType.Yoyo:
                    aspect.progress = aspect.easingRefRW.ValueRO.GetEasedValue(currentProgress);
                    if ((clampedCompletedLoops + (int)currentProgress) % 2 == 1) aspect.progress = 1f - aspect.progress;
                    break;
                case LoopType.Incremental:
                    aspect.progress = aspect.easingRefRW.ValueRO.GetEasedValue(1f) * clampedCompletedLoops +
                        aspect.easingRefRW.ValueRO.GetEasedValue(math.fmod(currentProgress, 1f));
                    break;
            }

            // set status & callback flags ----------------------------------------------------------------------

            if (isCompleted)
            {
                if (aspect.status != TweenStatusType.Completed) aspect.callbackFlags |= CallbackFlags.OnComplete;
                aspect.status = TweenStatusType.Completed;
            }
            else
            {
                if (currentTime < 0f)
                {
                    if (aspect.started && currentCompletedLoops <= 0)
                    {
                        if (prevCompletedLoops > currentCompletedLoops)
                        {
                            aspect.callbackFlags |= CallbackFlags.OnRewind;
                            aspect.status = TweenStatusType.RewindCompleted;
                        }
                        else
                        {
                            aspect.started = false;
                            aspect.status = TweenStatusType.WaitingForStart;
                        }
                    }
                    else
                    {
                        aspect.status = TweenStatusType.Delayed;
                    }

                    if (currentPosition >= 0f) aspect.callbackFlags |= CallbackFlags.OnUpdate;
                }
                else
                {
                    if (aspect.status == TweenStatusType.Delayed)
                    {
                        aspect.callbackFlags |= CallbackFlags.OnStart;
                        aspect.started = true;
                    }

                    aspect.status = TweenStatusType.Playing;
                    aspect.callbackFlags |= CallbackFlags.OnUpdate;

                    if (prevCompletedLoops < currentCompletedLoops && currentCompletedLoops > 0)
                    {
                        aspect.callbackFlags |= CallbackFlags.OnStepComplete;
                    }
                }
            }

            // set from  -------------------------------------------------------------------------------

            switch (aspect.invertMode)
            {
                case InvertMode.None:
                    aspect.inverted = false;
                    break;
                case InvertMode.Immediate:
                    aspect.inverted = true;
                    break;
                case InvertMode.AfterDelay:
                    aspect.inverted = currentTime >= 0f;
                    break;
            }
        }
    }
}