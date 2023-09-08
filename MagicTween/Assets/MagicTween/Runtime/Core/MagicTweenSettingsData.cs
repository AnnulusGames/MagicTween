using System;
using MagicTween.Diagnostics;

namespace MagicTween.Core
{
    [Serializable]
    public struct MagicTweenSettingsData
    {
        public LoggingMode loggingMode;
        public bool captureExceptions;

        public bool defaultAutoPlay;
        public bool defaultAutoKill;
        public Ease defaultEase;
        public LoopType defaultLoopType;
        public bool defaultIgnoreTimeScale;

        public static MagicTweenSettingsData Default => new()
        {
            loggingMode = LoggingMode.Full,
            captureExceptions = true,
            defaultAutoPlay = true,
            defaultAutoKill = true,
            defaultEase = Ease.Linear,
            defaultLoopType = LoopType.Restart,
            defaultIgnoreTimeScale = false
        };
    }
}