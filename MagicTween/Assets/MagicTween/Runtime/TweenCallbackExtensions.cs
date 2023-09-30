using System;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public static class TweenCallbackExtensions
    {
        internal static TweenCallbackActions GetOrAddComponent(in Entity entity)
        {
            if (TweenWorld.EntityManager.HasComponent<TweenCallbackActions>(entity))
            {
                return TweenWorld.EntityManager.GetComponentData<TweenCallbackActions>(entity);
            }
            else
            {
                // Use EntityCommandBuffer to avoid structural changes
                var actions = new TweenCallbackActions();
                var commandBuffer = TweenWorld.World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
                commandBuffer.AddComponent(entity, actions);
                return actions;
            }
        }

        internal static TweenCallbackActions GetOrAddCallbackActions<T>(this T self) where T : struct, ITweenHandle
        {
            return GetOrAddComponent(self.GetEntity());
        }

        public static T OnStart<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onStart += callback;
            return self;
        }

        public static T OnPlay<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onPlay += callback;
            return self;
        }

        public static T OnUpdate<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onUpdate += callback;
            return self;
        }

        public static T OnPause<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onPause += callback;
            return self;
        }

        public static T OnStepComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onStepComplete += callback;
            return self;
        }

        public static T OnComplete<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onComplete += callback;
            return self;
        }

        public static T OnKill<T>(this T self, Action callback) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetOrAddComponent(self.GetEntity()).onKill += callback;
            return self;
        }
    }
}