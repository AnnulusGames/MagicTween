using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Assertions;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    using static TweenWorld;

    public static class TweenSettingsExtensions
    {
        const string Error_AnimationCurveKeyframes = "The number of keyframe in Animation Curve must be 2 or more.";
        const string Error_DelayMustBeZeroOrHigher = "Delay must be 0 or higher";
        const string Error_EaseCustom = "Ease.Custom cannot be specified explicitly. If you want to specify a custom easing curve, use SetEase(AnimationCurve) instead.";

        public static T SetEase<T>(this T self, Ease ease) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            Assert.IsFalse(ease == Ease.Custom, Error_EaseCustom);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterEase(ease));
            return self;
        }

        public static T SetEase<T>(this T self, AnimationCurve animationCurve) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            Assert.IsTrue(animationCurve.keys.Length > 2, Error_AnimationCurveKeyframes);

            var curve = EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(self.GetEntity());
            if (curve.value.IsCreated) curve.value.Dispose();
            curve.value = new ValueAnimationCurve(animationCurve, Allocator.Persistent);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterEase(Ease.Custom));
            EntityManager.SetComponentData(self.GetEntity(), curve);

            return self;
        }

        public static T SetDelay<T>(this T self, float delay) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            Assert.IsTrue(delay >= 0f, Error_DelayMustBeZeroOrHigher);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterDelay(delay));
            return self;
        }

        public static T SetLoops<T>(this T self, int loops) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterLoops(loops));
            return self;
        }

        public static T SetLoops<T>(this T self, int loops, LoopType loopType) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterLoops(loops));
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterLoopType(loopType));
            return self;
        }

        public static T SetPlaybackSpeed<T>(this T self, float playbackSpeed) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterPlaybackSpeed(playbackSpeed));
            return self;
        }

        public static T SetIgnoreTimeScale<T>(this T self, bool ignoreTimeScale = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterIgnoreTimeScale(ignoreTimeScale));
            return self;
        }

        public static T SetRelative<T>(this T self, bool relative = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterIsRelative(relative));
            return self;
        }

        public static T SetInvert<T>(this T self, InvertMode invertMode = InvertMode.Immediate) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData(self.GetEntity(), new TweenParameterInvertMode(invertMode));
            return self;
        }

        public static T SetAutoKill<T>(this T self, bool autoKill = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData<TweenParameterAutoKill>(self.GetEntity(), new(autoKill));
            return self;
        }

        public static T SetAutoPlay<T>(this T self, bool autoPlay = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            EntityManager.SetComponentData<TweenParameterAutoPlay>(self.GetEntity(), new(autoPlay));
            return self;
        }

        public static T SetId<T>(this T self, int id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.id = id;
            EntityManager.SetComponentData(self.GetEntity(), tweenId);

            return self;
        }

        public static T SetId<T>(this T self, string id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.idString = new FixedString32Bytes(id);
            EntityManager.SetComponentData(self.GetEntity(), tweenId);

            return self;
        }

        public static T SetId<T>(this T self, in FixedString32Bytes id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.idString = id;
            EntityManager.SetComponentData(self.GetEntity(), tweenId);

            return self;
        }

        public static T SetLink<T>(this T self, GameObject gameObject, LinkBehaviour linkBehaviour = LinkBehaviour.KillOnDestroy) where T : struct, ITweenHandle
        {
            Assert.IsNotNull(gameObject);
            AssertTween.IsActive(self);

            if (!gameObject.TryGetComponent<TweenLinkTrigger>(out var trigger))
            {
                trigger = gameObject.AddComponent<TweenLinkTrigger>();
            }

            trigger.Add(self.AsUnitTween(), linkBehaviour);

            return self;
        }

        public static T SetLink<T>(this T self, Component component, LinkBehaviour linkBehaviour = LinkBehaviour.KillOnDestroy) where T : struct, ITweenHandle
        {
            return SetLink(self, component.gameObject, linkBehaviour);
        }

        public static Tween<TValue, IntegerTweenOptions> SetRoundingMode<TValue>(this Tween<TValue, IntegerTweenOptions> self, RoundingMode roundingMode)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<IntegerTweenOptions>()
            {
                options = new IntegerTweenOptions() { roundingMode = roundingMode }
            });

            return self;
        }

        public static Tween<UnsafeText, StringTweenOptions> SetRichTextEnabled(this Tween<UnsafeText, StringTweenOptions> self, bool richTextEnabled = true)
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.richTextEnabled = richTextEnabled;
            EntityManager.SetComponentData(self.GetEntity(), options);

            return self;
        }


        public static Tween<UnsafeText, StringTweenOptions> SetScrambleMode(this Tween<UnsafeText, StringTweenOptions> self, ScrambleMode scrambleMode)
        {
            AssertTween.IsActive(self);

            if (scrambleMode == ScrambleMode.Custom)
            {
                // TODO: 
                return self;
            }

            var options = EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.scrambleMode = scrambleMode;
            EntityManager.SetComponentData(self.GetEntity(), options);

            return self;
        }

        public static Tween<UnsafeText, StringTweenOptions> SetScrambleMode(this Tween<UnsafeText, StringTweenOptions> self, string customScrambleChars)
        {
            AssertTween.IsActive(self);

            if (string.IsNullOrEmpty(customScrambleChars))
            {
                return self;
            }

            var options = EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.scrambleMode = ScrambleMode.Custom;
            EntityManager.SetComponentData(self.GetEntity(), options);

            var component = EntityManager.GetComponentData<StringTweenCustomScrambleChars>(self.GetEntity());
            if (component.customChars.IsCreated)
            {
                component.customChars.CopyFrom(customScrambleChars);
            }
            else
            {
                component.customChars = new UnsafeText(System.Text.Encoding.UTF8.GetByteCount(customScrambleChars), Allocator.Persistent);
                component.customChars.CopyFrom(customScrambleChars);
            }
            EntityManager.SetComponentData(self.GetEntity(), component);

            return self;
        }

        public static Tween<float3, PathTweenOptions> SetPathType(this Tween<float3, PathTweenOptions> self, PathType pathType)
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<PathTweenOptions>>(self.GetEntity()).options;
            options.pathType = pathType;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PathTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<float3, PathTweenOptions> SetClosed(this Tween<float3, PathTweenOptions> self, bool closed = true)
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<PathTweenOptions>>(self.GetEntity()).options;
            options.isClosed = closed ? (byte)1 : (byte)0;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PathTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, PunchTweenOptions> SetFreqnency<TValue>(this Tween<TValue, PunchTweenOptions> self, int frequency)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(self.GetEntity()).options;
            options.frequency = frequency;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PunchTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, PunchTweenOptions> SetDampingRatio<TValue>(this Tween<TValue, PunchTweenOptions> self, float dampingRatio)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(self.GetEntity()).options;
            options.dampingRatio = dampingRatio;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PunchTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetFreqnency<TValue>(this Tween<TValue, ShakeTweenOptions> self, int frequency)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.frequency = frequency;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetDampingRatio<TValue>(this Tween<TValue, ShakeTweenOptions> self, float dampingRatio)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.dampingRatio = dampingRatio;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetRandomSeed<TValue>(this Tween<TValue, ShakeTweenOptions> self, uint seed)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.randomSeed = seed;
            EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }
    }
}