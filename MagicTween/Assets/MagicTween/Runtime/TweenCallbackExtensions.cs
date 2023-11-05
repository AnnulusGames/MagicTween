using System;
using Unity.Assertions;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public static class TweenCallbackExtensions
    {
        static TweenCallbackActions GetOrAddActions(in Entity entity)
        {
            if (ECSCache.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                return ECSCache.EntityManager.GetComponentData<TweenCallbackActions>(entity);
            }
            else
            {
                if (ECSCache.CallbackSystem.IsExecuting)
                {
                    return ECSCache.CallbackSystem.TryEnqueueAndGetActions(entity);
                }
                else
                {
                    var actions = TweenCallbackActionsPool.Rent();
                    ECSCache.EntityManager.AddComponentData(entity, actions);
                    return actions;
                }
            }
        }

        internal static TweenCallbackActions GetOrAddCallbackActions<T>(this T self) where T : struct, ITweenHandle
        {
            return GetOrAddActions(self.GetEntity());
        }

        public static T OnStart<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStart.Add(callback);
            return self;
        }

        public static T OnPlay<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPlay.Add(callback);
            return self;
        }

        public static T OnUpdate<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onUpdate.Add(callback);
            return self;
        }

        public static T OnPause<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPause.Add(callback);
            return self;
        }

        public static T OnStepComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStepComplete.Add(callback);
            return self;
        }

        public static T OnComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onComplete.Add(callback);
            return self;
        }

        public static T OnKill<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onKill.Add(callback);
            return self;
        }

        public static T OnPlay<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPlay.Add(target, callback);
            return self;
        }

        public static T OnStart<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStart.Add(target, callback);
            return self;
        }

        public static T OnUpdate<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onUpdate.Add(target, callback);
            return self;
        }

        public static T OnPause<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPause.Add(target, callback);
            return self;
        }

        public static T OnStepComplete<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStepComplete.Add(target, callback);
            return self;
        }

        public static T OnComplete<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onComplete.Add(target, callback);
            return self;
        }

        public static T OnKill<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            Assert.IsNotNull(target);
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onKill.Add(target, callback);
            return self;
        }
    }
}