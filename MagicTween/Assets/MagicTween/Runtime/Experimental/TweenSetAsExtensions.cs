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

            var entity = self.GetEntity();

            var easing = TweenWorld.EntityManager.GetComponentData<TweenEasing>(entity);
            easing.ease = tweenParams.ease;
            if (easing.customCurve.IsCreated) easing.customCurve.Dispose();
            if (tweenParams.ease == Ease.Custom)
            {
                easing.customCurve = new ValueAnimationCurve(tweenParams.customEasingCurve, Allocator.Persistent);
            }
            TweenWorld.EntityManager.SetComponentData(entity, easing);

            var clip = TweenWorld.EntityManager.GetComponentData<TweenClip>(entity);
            clip.delay = tweenParams.delay;
            clip.loops = tweenParams.loops;
            clip.loopType = tweenParams.loopType;
            TweenWorld.EntityManager.SetComponentData(entity, clip);

            TweenWorld.EntityManager.SetComponentData(entity, new TweenPlaybackSpeed()
            {
                value = tweenParams.playbackSpeed
            });

            TweenWorld.EntityManager.SetComponentData(entity, new TweenInvertMode()
            {
                value = tweenParams.fromMode,
            });
            TweenWorld.EntityManager.SetComponentData(entity, new TweenIgnoreTimeScaleFlag(tweenParams.ignoreTimeScale));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenIsRelativeFlag(tweenParams.isRelative));

            TweenWorld.EntityManager.SetComponentData(entity, new TweenAutoPlayFlag(tweenParams.autoPlay));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenAutoKillFlag(tweenParams.autoKill));

            TweenWorld.EntityManager.SetComponentData(entity, new TweenId()
            {
                id = tweenParams.customId,
                idString = tweenParams.customIdString
            });

            return self;
        }

    }
}