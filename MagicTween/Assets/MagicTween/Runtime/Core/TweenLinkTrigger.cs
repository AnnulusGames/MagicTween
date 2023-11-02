using System.Collections.Generic;
using MagicTween.Core;
using UnityEngine;

namespace MagicTween
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class TweenLinkTrigger : MonoBehaviour
    {
        readonly List<(Tween tween, LinkBehaviour linkBehaviour)> items = new(8);

        public void Add(Tween tween, LinkBehaviour linkBehaviour)
        {
            items.Add((tween, linkBehaviour));
        }

        void OnEnable()
        {
            for (int i = 0; i < items.Count; i++)
            {
                var (tween, linkBehaviour) = items[i];
                switch (linkBehaviour)
                {
                    case LinkBehaviour.PlayOnEnable:
                    case LinkBehaviour.PauseOnDisablePlayOnEnable:
                        if (tween.IsActive()) tween.Play();
                        break;
                    case LinkBehaviour.RestartOnEnable:
                    case LinkBehaviour.PauseOnDisableRestartOnEnable:
                        if (tween.IsActive()) tween.Restart();
                        break;
                }
            }
        }

        void OnDisable()
        {
#if UNITY_EDITOR
            if (isQuittingPlayMode) return;
#endif
            if (!ECSCache.World.IsCreated) return;
            for (int i = 0; i < items.Count; i++)
            {
                var (tween, linkBehaviour) = items[i];
                switch (linkBehaviour)
                {
                    case LinkBehaviour.PauseOnDisable:
                    case LinkBehaviour.PauseOnDisablePlayOnEnable:
                    case LinkBehaviour.PauseOnDisableRestartOnEnable:
                        if (tween.IsActive()) tween.Pause();
                        break;
                    case LinkBehaviour.KillOnDisable:
                        if (tween.IsActive()) tween.Kill();
                        break;
                    case LinkBehaviour.CompleteOnDisable:
                        if (tween.IsActive()) tween.Complete();
                        break;
                    case LinkBehaviour.CompleteAndKillOnDisable:
                        if (tween.IsActive()) tween.CompleteAndKill();
                        break;
                }
            }
        }

        void OnDestroy()
        {
#if UNITY_EDITOR
            if (isQuittingPlayMode) return;
#endif
            if (!ECSCache.World.IsCreated) return;
            for (int i = 0; i < items.Count; i++)
            {
                var (tween, linkBehaviour) = items[i];
                if (tween.IsActive()) tween.Kill();
            }
        }

#if UNITY_EDITOR
        bool isQuittingPlayMode = false;

        void OnApplicationQuit()
        {
            isQuittingPlayMode = true;
        }
#endif
    }
}