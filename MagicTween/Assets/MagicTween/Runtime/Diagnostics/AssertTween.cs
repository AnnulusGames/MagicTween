using System.Diagnostics;
using Unity.Entities;
using Unity.Assertions;
using MagicTween.Core;
using MagicTween.Core.Components;

namespace MagicTween.Diagnostics
{
    internal static class AssertTween
    {
        const string Error_TweenIsInvalid = "Tween is not initialized.";
        const string Error_TweenIsKilledOrNotCreated = "Tween has already been killed or has not been initialized.";
        const string Error_TweenIsNested = "Cannot access a nested tween.";
        const string Error_CannotAddToItSelf = "Cannot add a sequence to itself.";
        const string Error_CannotAddNonCompletableTween = "Cannot add a Tween with a loopCount less than 0 or a playbackSpeed of 0 to a Sequence.";
        const string Error_CannotAddPlayingTween = "A tween that is playing or has already played cannot be added to a sequence.";
        const string Error_IntervalMustBeZeroOrHigher = "'interval' must be 0 or higher.";

        [Conditional("UNITY_ASSERTIONS")]
        public static void IsValid<T>(in T tween) where T : struct, ITweenHandle
        {
            Assert.IsFalse(tween.GetEntity() == Entity.Null, Error_TweenIsInvalid);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void IsActive<T>(in T tween) where T : struct, ITweenHandle
        {
            Assert.IsTrue(TweenStatusExtensions.IsActive(tween), Error_TweenIsKilledOrNotCreated);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void IsRoot<T>(in T tween) where T : struct, ITweenHandle
        {
            Assert.IsTrue(TweenWorld.EntityManager.IsComponentEnabled<TweenRootFlag>(tween.GetEntity()), Error_TweenIsNested);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void SequenceItemIsNotEqualsItSelf<T>(in Sequence sequence, in T item) where T : struct, ITweenHandle
        {
            Assert.IsFalse(sequence.GetEntity() == item.GetEntity(), Error_CannotAddToItSelf);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void SequenceItemIsNotStarted<T>(in T item) where T : struct, ITweenHandle
        {
            var started = TweenWorld.EntityManager.GetComponentData<TweenStartedFlag>(item.GetEntity()).value;
            Assert.IsFalse(started, Error_CannotAddPlayingTween);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void SequenceItemIsCompletable(float duration)
        {
            Assert.IsTrue(duration >= 0f, Error_CannotAddNonCompletableTween);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void SequenceIntervalIsHigherThanZero(float interval)
        {
            Assert.IsTrue(interval >= 0f, Error_IntervalMustBeZeroOrHigher);
        }
    }
}