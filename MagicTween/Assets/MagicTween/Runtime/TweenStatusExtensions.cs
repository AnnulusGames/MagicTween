using Unity.Entities;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public static class TweenStatusExtensions
    {
        public static bool IsCreated<T>(this T self) where T : struct, ITweenHandle
        {
            return self.GetEntity() != Entity.Null;
        }

        public static bool IsActive<T>(this T self) where T : struct, ITweenHandle
        {
            var entity = self.GetEntity();
            if (entity == Entity.Null) return false;
            if (!TweenWorld.EntityManager.Exists(entity)) return false;
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            return status.value != TweenStatusType.Killed;
        }

        public static bool IsPlaying<T>(this T self) where T : struct, ITweenHandle
        {
            var entity = self.GetEntity();
            if (entity == Entity.Null) return false;
            if (!TweenWorld.EntityManager.Exists(entity)) return false;
            var status = TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity);
            return status.value is TweenStatusType.Delayed or TweenStatusType.Playing;
        }

        public static bool IsRoot<T>(this T self) where T : struct, ITweenHandle
        {
            var entity = self.GetEntity();
            if (entity == Entity.Null) return false;
            if (!TweenWorld.EntityManager.Exists(entity)) return false;
            return TweenWorld.EntityManager.IsComponentEnabled<TweenRootFlag>(entity);
        }

        public static float GetPlaybackSpeed<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenWorld.EntityManager.GetComponentData<TweenParameterPlaybackSpeed>(self.GetEntity()).value;
        }

        public static float GetDuration<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            return TweenHelper.GetDuration(ref TweenWorld.EntityManagerRef, self.GetEntity());
        }

        public static TValue GetValue<TValue, TOptions>(this Tween<TValue, TOptions> self)
            where TValue : unmanaged
            where TOptions : unmanaged, ITweenOptions
        {
            AssertTween.IsActive(self);
            return TweenWorld.EntityManager.GetComponentData<TweenValue<TValue>>(self.GetEntity()).value;
        }
    }
}