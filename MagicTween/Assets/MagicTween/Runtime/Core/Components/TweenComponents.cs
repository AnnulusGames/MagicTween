using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Core.Components
{
    public readonly struct TweenRootFlag : IComponentData, IEnableableComponent { }
    
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

    public struct TweenPlaySettings : IComponentData
    {
        public bool autoPlay;
        public bool autoKill;
    }

    public struct TweenParameters : IComponentData
    {
        public InvertMode invertMode;
        public bool isRelative;
        public bool ignoreTimeScale;
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

    public struct TweenControllerReference : IComponentData
    {
        public short controllerId;
    }

    public enum TweenAccessorFlagType : byte
    {
        None,
        Getter,
        Setter
    }
}