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
    public static class TweenSettingsExtensions
    {
        const string Error_AnimationCurveKeyframes = "The number of keyframe in Animation Curve must be 2 or more.";
        const string Error_DelayMustBeZeroOrHigher = "Delay must be 0 or higher";

        public static T SetEase<T>(this T self, Ease ease) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(self.GetEntity());
            easing.ease = ease;
            if (easing.customCurve.IsCreated) easing.customCurve.Dispose();
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), easing);

            return self;
        }

        public static T SetEase<T>(this T self, AnimationCurve animationCurve) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            Assert.IsTrue(animationCurve.keys.Length > 2, Error_AnimationCurveKeyframes);

            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(self.GetEntity());
            easing.ease = Ease.Custom;
            if (easing.customCurve.IsCreated) easing.customCurve.Dispose();
            easing.customCurve = new ValueAnimationCurve(animationCurve, Allocator.Persistent);
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), easing);

            return self;
        }

        public static T SetDelay<T>(this T self, float delay) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            Assert.IsTrue(delay >= 0f, Error_DelayMustBeZeroOrHigher);

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(self.GetEntity());
            clip.delay = delay;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), clip);

            return self;
        }

        public static T SetLoops<T>(this T self, int loops) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(self.GetEntity());
            clip.loops = loops;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), clip);

            return self;
        }

        public static T SetLoops<T>(this T self, int loops, LoopType loopType) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(self.GetEntity());
            clip.loops = loops;
            clip.loopType = loopType;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), clip);

            return self;
        }

        public static T SetPlaybackSpeed<T>(this T self, float playbackSpeed) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenPlaybackSpeed() { speed = playbackSpeed });
            return self;
        }

        public static T SetIgnoreTimeScale<T>(this T self, bool ignoreTimeScale = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(self.GetEntity());
            parameters.ignoreTimeScale = ignoreTimeScale;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), parameters);

            return self;
        }

        public static T SetRelative<T>(this T self, bool relative = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(self.GetEntity());
            parameters.isRelative = relative;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), parameters);

            return self;
        }

        public static T SetInvert<T>(this T self, InvertMode invertMode = InvertMode.Immediate) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var parameters = TweenWorld.EntityManager.GetComponentData<TweenParameters>(self.GetEntity());
            parameters.invertMode = invertMode;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), parameters);

            return self;
        }

        public static T SetAutoKill<T>(this T self, bool autoKill = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var playSettings = TweenWorld.EntityManager.GetComponentData<TweenPlaySettings>(self.GetEntity());
            playSettings.autoKill = autoKill;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), playSettings);

            return self;
        }

        public static T SetAutoPlay<T>(this T self, bool autoPlay = true) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var playSettings = TweenWorld.EntityManager.GetComponentData<TweenPlaySettings>(self.GetEntity());
            playSettings.autoPlay = autoPlay;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), playSettings);

            return self;
        }

        public static T SetId<T>(this T self, int id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = TweenWorld.EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.id = id;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), tweenId);

            return self;
        }

        public static T SetId<T>(this T self, string id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = TweenWorld.EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.idString = new FixedString32Bytes(id);
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), tweenId);

            return self;
        }

        public static T SetId<T>(this T self, in FixedString32Bytes id) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var tweenId = TweenWorld.EntityManager.GetComponentData<TweenId>(self.GetEntity());
            tweenId.idString = id;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), tweenId);

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

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<IntegerTweenOptions>()
            {
                options = new IntegerTweenOptions() { roundingMode = roundingMode }
            });

            return self;
        }

        public static Tween<UnsafeText, StringTweenOptions> SetRichTextEnabled(this Tween<UnsafeText, StringTweenOptions> self, bool richTextEnabled = true)
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.richTextEnabled = richTextEnabled;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), options);

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

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.scrambleMode = scrambleMode;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), options);

            return self;
        }

        public static Tween<UnsafeText, StringTweenOptions> SetScrambleMode(this Tween<UnsafeText, StringTweenOptions> self, string customScrambleChars)
        {
            AssertTween.IsActive(self);

            if (string.IsNullOrEmpty(customScrambleChars))
            {
                return self;
            }

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<StringTweenOptions>>(self.GetEntity());
            options.options.scrambleMode = ScrambleMode.Custom;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), options);

            var component = TweenWorld.EntityManager.GetComponentData<StringTweenCustomScrambleChars>(self.GetEntity());
            if (component.customChars.IsCreated)
            {
                component.customChars.CopyFrom(customScrambleChars);
            }
            else
            {
                component.customChars = new UnsafeText(System.Text.Encoding.UTF8.GetByteCount(customScrambleChars), Allocator.Persistent);
                component.customChars.CopyFrom(customScrambleChars);
            }
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), component);

            return self;
        }

        public static Tween<float3, PathTweenOptions> SetPathType(this Tween<float3, PathTweenOptions> self, PathType pathType)
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PathTweenOptions>>(self.GetEntity()).options;
            options.pathType = pathType;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PathTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<float3, PathTweenOptions> SetClosed(this Tween<float3, PathTweenOptions> self, bool closed = true)
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PathTweenOptions>>(self.GetEntity()).options;
            options.isClosed = closed ? (byte)1 : (byte)0;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PathTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, PunchTweenOptions> SetFreqnency<TValue>(this Tween<TValue, PunchTweenOptions> self, int frequency)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(self.GetEntity()).options;
            options.frequency = frequency;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PunchTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, PunchTweenOptions> SetDampingRatio<TValue>(this Tween<TValue, PunchTweenOptions> self, float dampingRatio)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<PunchTweenOptions>>(self.GetEntity()).options;
            options.dampingRatio = dampingRatio;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<PunchTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetFreqnency<TValue>(this Tween<TValue, ShakeTweenOptions> self, int frequency)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.frequency = frequency;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetDampingRatio<TValue>(this Tween<TValue, ShakeTweenOptions> self, float dampingRatio)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.dampingRatio = dampingRatio;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }

        public static Tween<TValue, ShakeTweenOptions> SetRandomSeed<TValue>(this Tween<TValue, ShakeTweenOptions> self, uint seed)
            where TValue : unmanaged
        {
            AssertTween.IsActive(self);

            var options = TweenWorld.EntityManager.GetComponentData<TweenOptions<ShakeTweenOptions>>(self.GetEntity()).options;
            options.randomSeed = seed;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenOptions<ShakeTweenOptions>()
            {
                options = options
            });

            return self;
        }
    }
}