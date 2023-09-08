using System.Collections.Generic;
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
                        tween.Play();
                        break;
                    case LinkBehaviour.RestartOnEnable:
                    case LinkBehaviour.PauseOnDisableRestartOnEnable:
                        tween.Restart();
                        break;
                }
            }
        }

        void OnDisable()
        {
#if UNITY_EDITOR
            if (isQuittingPlayMode) return;
#endif
            for (int i = 0; i < items.Count; i++)
            {
                var (tween, linkBehaviour) = items[i];
                switch (linkBehaviour)
                {
                    case LinkBehaviour.PauseOnDisable:
                    case LinkBehaviour.PauseOnDisablePlayOnEnable:
                    case LinkBehaviour.PauseOnDisableRestartOnEnable:
                        tween.Pause();
                        break;
                    case LinkBehaviour.KillOnDisable:
                        tween.Kill();
                        break;
                    case LinkBehaviour.CompleteOnDisable:
                        tween.Complete();
                        break;
                    case LinkBehaviour.CompleteAndKillOnDisable:
                        tween.CompleteAndKill();
                        break;
                }
            }
        }

        void OnDestroy()
        {
#if UNITY_EDITOR
            if (isQuittingPlayMode) return;
#endif
            for (int i = 0; i < items.Count; i++)
            {
                var (tween, linkBehaviour) = items[i];
                tween.Kill();
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