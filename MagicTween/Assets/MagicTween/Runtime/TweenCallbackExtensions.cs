using System;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicTween
{
    public static class TweenCallbackExtensions
    {
        internal static TweenCallbackActions GetOrAddActions(in Entity entity)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                return TweenWorld.EntityManager.GetComponentData<TweenCallbackActions>(entity);
            }
            else
            {
                var actions = TweenCallbackActionsPool.Rent();
                if (TweenWorld.CallbackSystem.IsExecuting)
                {
                    // Use EntityCommandBuffer to avoid structural changes
                    var commandBuffer = TweenWorld.World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
                    commandBuffer.AddComponent(entity, actions);
                }
                else
                {
                    TweenWorld.EntityManager.AddComponentData(entity, actions);
                }
                return actions;
            }
        }

        internal static TweenCallbackActionsNoAlloc GetOrAddActionsNoAlloc(in Entity entity, object target)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActionsNoAlloc>(entity))
            {
                return TweenWorld.EntityManager.GetComponentData<TweenCallbackActionsNoAlloc>(entity);
            }
            else
            {
                var actions = TweenCallbackActionsNoAllocPool.Rent(target);
                if (TweenWorld.CallbackSystem.IsExecuting)
                {
                    // Use EntityCommandBuffer to avoid structural changes
                    var commandBuffer = TweenWorld.World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
                    commandBuffer.AddComponent(entity, actions);
                }
                else
                {
                    TweenWorld.EntityManager.AddComponentData(entity, actions);
                }
                return actions;
            }
        }

        internal static TweenCallbackActions GetOrAddCallbackActions<T>(this T self) where T : struct, ITweenHandle
        {
            return GetOrAddActions(self.GetEntity());
        }

        internal static TweenCallbackActionsNoAlloc GetOrAddCallbackActionsNoAlloc<T>(this T self, object target) where T : struct, ITweenHandle
        {
            return GetOrAddActionsNoAlloc(self.GetEntity(), target);
        }


        public static T OnStart<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStart += callback;
            return self;
        }

        public static T OnPlay<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPlay += callback;
            return self;
        }

        public static T OnUpdate<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onUpdate += callback;
            return self;
        }

        public static T OnPause<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onPause += callback;
            return self;
        }

        public static T OnStepComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onStepComplete += callback;
            return self;
        }

        public static T OnComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onComplete += callback;
            return self;
        }

        public static T OnKill<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddActions(self.GetEntity()).onKill += callback;
            return self;
        }

        public static T OnPlay<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onPlay += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }

        public static T OnUpdate<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onUpdate += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }

        public static T OnPause<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onPause += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }

        public static T OnStepComplete<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onStepComplete += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }

        public static T OnComplete<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onComplete += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }

        public static T OnKill<T, TObject>(this T self, TObject target, Action<TObject> callback)
            where T : struct, ITweenHandle
            where TObject : class
        {
            AssertTween.IsActive(self);
            GetOrAddActionsNoAlloc(self.GetEntity(), target).onKill += UnsafeUtility.As<Action<TObject>, Action<object>>(ref callback);
            return self;
        }
    }
}