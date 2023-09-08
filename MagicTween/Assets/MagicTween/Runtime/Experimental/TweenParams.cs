using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Experimental.Core;

namespace MagicTween.Experimental
{
    public struct TweenParams : IDisposable
    {
        public unsafe static TweenParamsBuilder CreateBuilder()
        {
            var builder = new TweenParamsBuilder()
            {
                paramsPtr = (TweenParams*)UnsafeUtility.Malloc(
                    UnsafeUtility.SizeOf<TweenParams>(),
                    UnsafeUtility.AlignOf<TweenParams>(),
                    Allocator.Temp)
            };
            var defaultValue = new TweenParams()
            {
                loops = 1,
                playbackSpeed = 1f,
                autoKill = true,
                autoPlay = true
            };
            UnsafeUtility.CopyStructureToPtr(ref defaultValue, builder.paramsPtr);
            return builder;
        }

        internal Ease ease;
        internal ValueAnimationCurve customEasingCurve;
        internal int loops;
        internal LoopType loopType;
        internal float playbackSpeed;
        internal float delay;
        internal bool autoPlay;
        internal bool autoKill;
        internal bool ignoreTimeScale;
        internal bool isRelative;
        internal InvertMode fromMode;
        internal int customId;
        internal FixedString32Bytes customIdString;

        public void Dispose()
        {
            if (customEasingCurve.IsCreated) customEasingCurve.Dispose();
        }
    }
}