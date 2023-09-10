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

            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterEase(tweenParams.ease));
            if (tweenParams.ease == Ease.Custom)
            {
                var customEasingCurve = TweenWorld.EntityManager.GetComponentData<TweenParameterCustomEasingCurve>(entity).value;
                if (customEasingCurve.IsCreated) customEasingCurve.Dispose();
                customEasingCurve = new ValueAnimationCurve(tweenParams.customEasingCurve, Allocator.Persistent);
                TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterCustomEasingCurve(customEasingCurve));
            }

            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterDelay(tweenParams.delay));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterLoops(tweenParams.loops));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterLoopType(tweenParams.loopType));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterPlaybackSpeed(tweenParams.playbackSpeed));

            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterInvertMode(tweenParams.invertMode));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterIgnoreTimeScale(tweenParams.ignoreTimeScale));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterIsRelative(tweenParams.isRelative));

            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterAutoPlay(tweenParams.autoPlay));
            TweenWorld.EntityManager.SetComponentData(entity, new TweenParameterAutoKill(tweenParams.autoKill));

            TweenWorld.EntityManager.SetComponentData(entity, new TweenId()
            {
                id = tweenParams.customId,
                idString = tweenParams.customIdString
            });

            return self;
        }

    }
}