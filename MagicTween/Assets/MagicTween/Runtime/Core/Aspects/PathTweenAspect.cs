using Unity.Entities;
using Unity.Mathematics;
using MagicTween.Core.Components;
using MagicTween.Plugins;

namespace MagicTween.Core.Aspects
{
    public readonly partial struct PathTweenAspect : IAspect
    {
        public readonly DynamicBuffer<PathPoint> points;
        readonly RefRW<TweenValue<float3>> current;
        readonly RefRO<TweenOptions<PathTweenOptions>> optionsRef;
#pragma warning disable CS0414
        readonly RefRO<TweenPluginTag<PathTweenPlugin>> pluginTagRef;
#pragma warning restore CS0414

        public float3 CurrentValue
        {
            get => current.ValueRO.value;
            set => current.ValueRW.value = value;
        }

        public PathTweenOptions Options => optionsRef.ValueRO.value;
    }
}