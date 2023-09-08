#if MAGICTWEEN_SUPPORT_UNIRX
using System;
using UniRx;
using MagicTween.Core;
using MagicTween.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicTween
{
    public static class TweenObservableExtensions
    {
        public static IObservable<Unit> OnStartAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnStart);
        }

        public static IObservable<Unit> OnPlayAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnPlay);
        }

        public static IObservable<Unit> OnPauseAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnPause);
        }

        public static IObservable<Unit> OnStepCompleteAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnStepComplete);
        }

        public static IObservable<Unit> OnCompleteAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnComplete);
        }

        public static IObservable<Unit> OnKillAsObservable<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self.AsUnitTween(), TweenCallbackObservable.CallbackType.OnKill);
        }

        public static IObservable<Unit> ToObservable(this Tween self)
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self, TweenCallbackObservable.CallbackType.OnUpdate);
        }

        public static IObservable<Unit> ToObservable(this Sequence self)
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenCallbackObservable(self, TweenCallbackObservable.CallbackType.OnUpdate);
        }

        public static IObservable<TValue> ToObservable<TValue, TOptions>(this Tween<TValue, TOptions> self)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            AssertTween.IsActive(self);
            AssertTween.IsRoot(self);
            return new TweenObservable<TValue, TOptions>(self);
        }
    }
}
#endif