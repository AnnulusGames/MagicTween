using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Plugins;
using MagicTween.Core.Components;

namespace MagicTween.Core.Aspects
{
    public readonly partial struct StringTweenAspect : IAspect
    {
        readonly RefRW<TweenStartValue<UnsafeText>> startRefRW;
        readonly RefRW<TweenEndValue<UnsafeText>> endRefRW;
        readonly RefRW<TweenValue<UnsafeText>> currentRefRW;
        readonly RefRO<TweenOptions<StringTweenOptions>> optionsRefRO;
        readonly RefRW<StringTweenCustomScrambleChars> customScrambleCharsRefRW;
#pragma warning disable CS0414
        readonly RefRO<TweenPluginTag<StringTweenPlugin>> pluginTagRefRO;
#pragma warning restore CS0414

        public ref UnsafeText StartValue => ref startRefRW.ValueRW.value;
        public ref UnsafeText EndValue => ref endRefRW.ValueRW.value;
        public ref UnsafeText CurrentValue => ref currentRefRW.ValueRW.value;
        public ref UnsafeText CustomScrambleChars => ref customScrambleCharsRefRW.ValueRW.customChars;
        public bool RichTextEnabled => optionsRefRO.ValueRO.value.richTextEnabled;
        public ScrambleMode ScrambleMode => optionsRefRO.ValueRO.value.scrambleMode;
    }
}