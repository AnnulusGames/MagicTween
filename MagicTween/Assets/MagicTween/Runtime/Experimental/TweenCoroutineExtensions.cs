using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Entities;
using MagicTween.Core;
using MagicTween.Diagnostics;
using MagicTween.Core.Components;

namespace MagicTween.Experimental
{
    public static class TweenCoroutineExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static CallbackFlags GetCallbackFlags(in Entity entity)
        {
            return TweenWorld.EntityManager.GetComponentData<TweenCallbackFlags>(entity).flags;
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
            while (Exists(entity) && (GetCallbackFlags(entity) & (CallbackFlags.OnPlay | CallbackFlags.OnKill)) != 0)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForStart<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && (GetCallbackFlags(entity) & (CallbackFlags.OnStart | CallbackFlags.OnKill)) != 0)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForPause<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && (GetCallbackFlags(entity) & (CallbackFlags.OnPause | CallbackFlags.OnKill)) != 0)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForStepComplete<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;

            var entity = self.GetEntity();
            while (Exists(entity) && (GetCallbackFlags(entity) & (CallbackFlags.OnStepComplete | CallbackFlags.OnKill)) != 0)
            {
                yield return null;
            }
        }


        public static IEnumerator WaitForComplete<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsValid(self);
            if (!self.IsActive()) yield break;
            
            var entity = self.GetEntity();
            while (Exists(entity) && (GetCallbackFlags(entity) & (CallbackFlags.OnComplete | CallbackFlags.OnKill)) != 0)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForKill<T>(this T self) where T : struct, ITweenHandle
        {
            AssertTween.IsActive(self);
            var entity = self.GetEntity();
            while (Exists(entity) && (GetCallbackFlags(entity) & CallbackFlags.OnKill) != CallbackFlags.OnKill)
            {
                yield return null;
            }
        }
    }
}