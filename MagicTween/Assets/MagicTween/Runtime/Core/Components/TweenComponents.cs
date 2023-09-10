using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Core.Components
{
    public readonly struct TweenRootFlag : IComponentData, IEnableableComponent { }
    public readonly struct TweenAutoPlayFlag : IComponentData
    {
        public TweenAutoPlayFlag(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenAutoKillFlag : IComponentData
    {
        public TweenAutoKillFlag(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenIgnoreTimeScaleFlag : IComponentData
    {
        public TweenIgnoreTimeScaleFlag(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenIsRelativeFlag : IComponentData
    {
        public TweenIsRelativeFlag(bool value) => this.value = value;
        public readonly bool value;
    }


    public struct TweenInvertFlag : IComponentData
    {
        public bool value;
    }

    public struct TweenStartedFlag : IComponentData
    {
        public bool value;
    }

    public struct TweenStatus : IComponentData
    {
        public TweenStatusType status;
    }

    public struct TweenPosition : IComponentData
    {
        public float position;
        public int completedLoops;
    }

    public struct TweenProgress : IComponentData
    {
        public float progress;
    }

    public struct TweenClip : IComponentData
    {
        public float duration;
        public float delay;

        public int loops;
        public LoopType loopType;
    }

    public struct TweenPlaybackSpeed : IComponentData
    {
        public float speed;
    }

    public struct TweenInvertMode : IComponentData
    {
        public InvertMode invertMode;
    }

    [BurstCompile]
    public struct TweenEasing : IComponentData
    {
        public Ease ease;
        public ValueAnimationCurve customCurve;

        [BurstCompile]
        public readonly float GetEasedValue(in float t)
        {
            return ease == Ease.Custom ?
                customCurve.Evaluate(t) :
                EaseUtility.Evaluate(t, ease);
        }
    }

    public struct TweenId : IComponentData
    {
        public int id;
        public FixedString32Bytes idString;
    }

    public struct TweenAccessorFlag : IComponentData
    {
        public TweenAccessorFlagType value;
    }

    public struct TweenCallbackFlags : IComponentData
    {
        public CallbackFlags flags;
    }

    public readonly struct TweenControllerReference : IComponentData
    {
        public TweenControllerReference(short controllerId) => this.controllerId = controllerId;
        public readonly short controllerId;
    }

    public enum TweenAccessorFlagType : byte
    {
        None,
        Getter,
        Setter
    }
}