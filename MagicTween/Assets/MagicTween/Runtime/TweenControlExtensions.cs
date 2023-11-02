using System.Runtime.CompilerServices;
using MagicTween.Core;
using MagicTween.Core.Components;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public static class TweenControlExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ITweenController GetController<T>(ref T tween) where T : struct, ITweenHandle
        {
            var id = ECSCache.EntityManager.GetComponentData<TweenControllerReference>(tween.GetEntity()).controllerId;
            return TweenControllerContainer.FindControllerById(id);
        }

        public static void Play<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).Play(self.GetEntity());
        }

        public static void Pause<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).Pause(self.GetEntity());
        }

        public static void Restart<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).Restart(self.GetEntity());
        }

        public static void Complete<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).Complete(self.GetEntity());
        }

        public static void Kill<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).Kill(self.GetEntity());
        }

        public static void CompleteAndKill<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            GetController(ref self).CompleteAndKill(self.GetEntity());
        }

        public static void TogglePause<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);

            var status = ECSCache.EntityManager.GetComponentData<TweenStatus>(self.GetEntity());

            if (status.value == TweenStatusType.Paused)
            {
                GetController(ref self).Play(self.GetEntity());
            }
            else
            {
                GetController(ref self).Pause(self.GetEntity());
            }
        }
    }
}