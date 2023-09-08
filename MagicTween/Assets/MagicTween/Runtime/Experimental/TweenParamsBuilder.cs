using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;
using MagicTween.Diagnostics;

namespace MagicTween.Experimental.Core
{
    public unsafe ref struct TweenParamsBuilder
    {
        internal TweenParams* paramsPtr;
        internal AnimationCurve customEasingCurve;

        public TweenParamsBuilder SetEase(Ease ease)
        {
            CheckBuilderPtr();

            if (ease == Ease.Custom)
            {
                LogWarning_EaseCustom();
                return this;
            }

            paramsPtr->ease = ease;

            return this;
        }

        [BurstDiscard]
        void LogWarning_EaseCustom()
        {
            Debugger.LogWarning("Ease.Custom cannot be specified explicitly. Use SetEase(AnimatinonCurve) instead.");
        }

        public TweenParamsBuilder SetEase(AnimationCurve animationCurve)
        {
            CheckBuilderPtr();

            paramsPtr->ease = Ease.Custom;
            customEasingCurve = animationCurve;

            return this;
        }

        public TweenParamsBuilder SetDelay(float delay)
        {
            CheckBuilderPtr();
            paramsPtr->delay = delay;
            return this;
        }

        public TweenParamsBuilder SetPlaybackSpeed(float speed)
        {
            CheckBuilderPtr();
            paramsPtr->playbackSpeed = speed;
            return this;
        }

        public TweenParamsBuilder SetLoops(int loops)
        {
            CheckBuilderPtr();
            paramsPtr->loops = loops;
            return this;
        }

        public TweenParamsBuilder SetLoops(int loops, LoopType loopType)
        {
            CheckBuilderPtr();
            paramsPtr->loops = loops;
            paramsPtr->loopType = loopType;
            return this;
        }

        public TweenParamsBuilder SetIgnoreTimeScale(bool ignoreTimeScale = true)
        {
            CheckBuilderPtr();
            paramsPtr->ignoreTimeScale = ignoreTimeScale;
            return this;
        }

        public TweenParamsBuilder SetRelative(bool isRelative = true)
        {
            CheckBuilderPtr();
            paramsPtr->isRelative = isRelative;
            return this;
        }

        public TweenParamsBuilder SetFrom(InvertMode fromMode = InvertMode.Immediate)
        {
            CheckBuilderPtr();
            paramsPtr->fromMode = fromMode;
            return this;
        }

        public TweenParamsBuilder SetAutoKill(bool autoKill = true)
        {
            CheckBuilderPtr();
            paramsPtr->autoKill = autoKill;
            return this;
        }

        public TweenParamsBuilder SetAutoPlay(bool autoPlay = true)
        {
            CheckBuilderPtr();
            paramsPtr->autoPlay = autoPlay;
            return this;
        }

        public TweenParamsBuilder SetId(int id)
        {
            CheckBuilderPtr();
            paramsPtr->customId = id;
            return this;
        }

        public TweenParamsBuilder SetId(string id)
        {
            CheckBuilderPtr();
            paramsPtr->customIdString = new FixedString32Bytes(id);
            return this;
        }

        public TweenParamsBuilder SetId(in FixedString32Bytes id)
        {
            CheckBuilderPtr();
            paramsPtr->customIdString = id;
            return this;
        }

        public TweenParams Build()
        {
            CheckBuilderPtr();

            if (paramsPtr->ease == Ease.Custom)
            {
                paramsPtr->customEasingCurve = new ValueAnimationCurve(customEasingCurve, Allocator.Persistent);
            }

            UnsafeUtility.CopyPtrToStructure<TweenParams>(paramsPtr, out var result);
            Dispose();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CheckBuilderPtr()
        {
            if (paramsPtr == null)
            {
                throw new InvalidOperationException("Builder is not initialized");
            }
        }

        public void Dispose()
        {
            CheckBuilderPtr();
            UnsafeUtility.Free(paramsPtr, Allocator.Temp);
            paramsPtr = null;
            customEasingCurve = null;
        }
    }
}