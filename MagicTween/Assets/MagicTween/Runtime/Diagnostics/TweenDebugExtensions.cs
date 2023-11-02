using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using MagicTween.Core;
using MagicTween.Plugins;

namespace MagicTween.Diagnostics
{
    public static class TweenDebugExtensions
    {
        static string GetLogHeader(string tag)
        {
            return tag == null ? string.Empty : $"[{tag}] ";
        }

        public static T LogOnStart<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnStart(self, () => Debugger.Log(GetLogHeader(tag) + "OnStart", false));
        }

        public static T LogOnPlay<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnPlay(self, () => Debugger.Log(GetLogHeader(tag) + "OnPlay", false));
        }

        public static T LogOnUpdate<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnUpdate(self, () => Debugger.Log(GetLogHeader(tag) + "OnUpdate", false));
        }

        public static T LogOnPause<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnPause(self, () => Debugger.Log(GetLogHeader(tag) + "OnPause", false));
        }

        public static T LogOnStepComplete<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnStepComplete(self, () => Debugger.Log(GetLogHeader(tag) + "OnStepComplete", false));
        }

        public static T LogOnComplete<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnComplete(self, () => Debugger.Log(GetLogHeader(tag) + "OnComplete", false));
        }

        public static T LogOnKill<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenCallbackExtensions.OnKill(self, () => Debugger.Log(GetLogHeader(tag) + "OnKill", false));
        }

        public static T LogCallbacks<T>(this T self, string tag = null) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var callbacks = self.GetOrAddCallbackActions();
            var header = GetLogHeader(tag);
            callbacks.onStart = () => Debugger.Log(header + "OnStart", false);
            callbacks.onPlay = () => Debugger.Log(header + "OnPlay", false);
            callbacks.onUpdate = () => Debugger.Log(header + "OnUpdate", false);
            callbacks.onPause = () => Debugger.Log(header + "OnPause", false);
            callbacks.onStepComplete = () => Debugger.Log(header + "OnStepComplete", false);
            callbacks.onComplete = () => Debugger.Log(header + "OnComplete", false);
            callbacks.onKill = () => Debugger.Log(header + "OnKill", false);
            return self;
        }

        public static Tween<TValue, TOptions> LogValue<TValue, TOptions>(this Tween<TValue, TOptions> self)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            AssertTween.IsActive(self);
            self.GetOrAddCallbackActions().onUpdate += () => Debugger.Log(self.GetValue().ToString());
            return self;
        }

        public static Tween<UnsafeText, StringTweenOptions> LogValue(this Tween<UnsafeText, StringTweenOptions> self)
        {
            AssertTween.IsActive(self);

            var text = self.GetValue();
            self.GetOrAddCallbackActions().onUpdate += () => Debugger.Log(text.ConvertToString());
            return self;
        }

        public static T SetEntityName<T>(this T self, string name) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            ECSCache.EntityManager.SetName(self.GetEntity(), name);
            return self;
        }
    }
}
