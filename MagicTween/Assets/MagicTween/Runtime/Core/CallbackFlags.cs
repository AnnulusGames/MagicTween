using System;

namespace MagicTween.Core
{
    [Flags]
    public enum CallbackFlags : short
    {
        None = 0,
        OnStartUp = 1,
        OnStart = 2,
        OnPlay = 4,
        OnPause = 8,
        OnUpdate = 16,
        OnStepComplete = 32,
        OnComplete = 64,
        OnRewind = 128,
        OnKill = 256
    }
}