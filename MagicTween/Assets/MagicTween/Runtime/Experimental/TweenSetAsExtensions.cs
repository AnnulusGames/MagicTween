using Unity.Collections;
using MagicTween.Core;
using MagicTween.Diagnostics;
using MagicTween.Core.Components;

namespace MagicTween.Experimental
{
    public static class TweenSetAsExtensions
    {
        public static T SetAs<T>(this T self, in TweenParams tweenParams) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(self.GetEntity());
            easing.ease = tweenParams.ease;
            if (easing.customCurve.IsCreated) easing.customCurve.Dispose();
            if (tweenParams.ease == Ease.Custom)
            {
                easing.customCurve = new ValueAnimationCurve(tweenParams.customEasingCurve, Allocator.Persistent);
            }
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), easing);

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(self.GetEntity());
            clip.delay = tweenParams.delay;
            clip.loops = tweenParams.loops;
            clip.loopType = tweenParams.loopType;
            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), clip);

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenPlaybackSpeed()
            {
                speed = tweenParams.playbackSpeed
            });

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenParameters()
            {
                invertMode = tweenParams.fromMode,
                isRelative = tweenParams.isRelative,
                ignoreTimeScale = tweenParams.ignoreTimeScale
            });

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenPlaySettings()
            {
                autoKill = tweenParams.autoKill,
                autoPlay = tweenParams.autoPlay
            });

            TweenWorld.EntityManager.SetComponentData(self.GetEntity(), new TweenId()
            {
                id = tweenParams.customId,
                idString = tweenParams.customIdString
            });

            return self;
        }

    }
}