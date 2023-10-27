using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Diagnostics;
using MagicTween.Core.Components;

namespace MagicTween
{
    public static class TweenCoroutineExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TweenStatusType GetStatus(in Entity entity)
        {
            return TweenWorld.EntityManager.GetComponentData<TweenStatus>(entity).value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool Exists(in Entity entity)
        {
            return TweenWorld.EntityManager.Exists(entity);
        }

        public static IEnumerator WaitForPlay<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && GetStatus(entity) is not (TweenStatusType.Playing or TweenStatusType.Killed))
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForStart<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && !TweenWorld.EntityManager.GetComponentData<TweenStartedFlag>(entity).value)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForPause<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && GetStatus(entity) is not (TweenStatusType.Paused or TweenStatusType.Killed))
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForStepComplete<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            var completedLoops = TweenWorld.EntityManager.GetComponentData<TweenCompletedLoops>(entity).value;
            while (Exists(entity) && TweenWorld.EntityManager.GetComponentData<TweenCompletedLoops>(entity).value == completedLoops)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForComplete<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            var entity = self.GetEntity();
            while (Exists(entity) && GetStatus(entity) is not (TweenStatusType.Completed or TweenStatusType.Killed))
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForKill<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            var entity = self.GetEntity();
            while (Exists(entity) && GetStatus(entity) is not TweenStatusType.Killed)
            {
                yield return null;
            }
        }
    }
}