using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MagicTween.Core.Components
{
    public readonly struct TweenRootFlag : IComponentData, IEnableableComponent { }

    public struct TweenInvertFlag : IComponentData
    {
        public TweenInvertFlag(bool value) => this.value = value;
        public bool value;
    }

    public struct TweenStartedFlag : IComponentData
    {
        public TweenStartedFlag(bool value) => this.value = value;
        public bool value;
    }

    public struct TweenStatus : IComponentData
    {
        public TweenStatusType status;
    }

    public struct TweenPosition : IComponentData
    {
        public TweenPosition(float value) => this.value = value;
        public float value;
    }

    public struct TweenCompletedLoops : IComponentData
    {
        public TweenCompletedLoops(int value) => this.value = value;
        public int value;
    }

    public struct TweenProgress : IComponentData
    {
        public float value;
    }

    public readonly struct TweenParameterDuration : IComponentData
    {
        public TweenParameterDuration(float value) => this.value = value;
        public readonly float value;
    }

    public readonly struct TweenParameterDelay : IComponentData
    {
        public TweenParameterDelay(float value) => this.value = value;
        public readonly float value;
    }

    public readonly struct TweenParameterLoops : IComponentData
    {
        public TweenParameterLoops(int value) => this.value = value;
        public readonly int value;
    }

    public readonly struct TweenParameterLoopType : IComponentData
    {
        public TweenParameterLoopType(LoopType value) => this.value = value;
        public readonly LoopType value;
    }

    public readonly struct TweenParameterPlaybackSpeed : IComponentData
    {
        public TweenParameterPlaybackSpeed(float value) => this.value = value;
        public readonly float value;
    }

    public readonly struct TweenParameterAutoPlay : IComponentData
    {
        public TweenParameterAutoPlay(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenParameterAutoKill : IComponentData
    {
        public TweenParameterAutoKill(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenParameterIgnoreTimeScale : IComponentData
    {
        public TweenParameterIgnoreTimeScale(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenParameterIsRelative : IComponentData
    {
        public TweenParameterIsRelative(bool value) => this.value = value;
        public readonly bool value;
    }

    public readonly struct TweenParameterInvertMode : IComponentData
    {
        public TweenParameterInvertMode(InvertMode value) => this.value = value;
        public readonly InvertMode value;
    }

    public readonly struct TweenParameterEase : IComponentData
    {
        public TweenParameterEase(Ease value) => this.value = value;
        public readonly Ease value;
    }

    public struct TweenParameterCustomEasingCurve : IComponentData
    {
        public TweenParameterCustomEasingCurve(ValueAnimationCurve value) => this.value = value;
        public ValueAnimationCurve value;
    }

    public struct TweenId : IComponentData
    {
        public int id;
        public FixedString32Bytes idString;
    }

    public struct TweenAccessorFlags : IComponentData
    {
        public AccessorFlags flags;
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

    [Flags]
    public enum AccessorFlags : byte
    {
        None = 0,
        Getter = 1,
        Setter = 2
    }
}