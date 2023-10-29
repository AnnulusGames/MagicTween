using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core.Components;
using MagicTween.Plugins;

[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float>))]
[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float2>))]
[assembly: RegisterGenericComponentType(typeof(VibrationStrength<float3>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<PunchTweenOptions>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<ShakeTweenOptions>))]

[assembly: RegisterGenericComponentType(typeof(TweenValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenStartValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenEndValue<UnsafeText>))]
[assembly: RegisterGenericComponentType(typeof(TweenOptions<StringTweenOptions>))]
[assembly: RegisterGenericComponentType(typeof(TweenDelegates<string>))]

[assembly: RegisterGenericComponentType(typeof(TweenOptions<PathTweenOptions>))]


namespace MagicTween.Core.Components
{
    public struct VibrationStrength<TValue> : IComponentData
        where TValue : unmanaged
    {
        public TValue value;
    }
    
    public struct ShakeRandomState : IComponentData
    {
        public Random random;
    }

    [InternalBufferCapacity(10)]
    public struct PathPoint : IBufferElementData
    {
        public float3 point;
    }

    public struct StringTweenCustomScrambleChars : IComponentData
    {
        public UnsafeText customChars;
    }
}