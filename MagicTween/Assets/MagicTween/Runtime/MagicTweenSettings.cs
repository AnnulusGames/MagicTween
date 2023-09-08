using Unity.Burst;
using MagicTween.Core;
using MagicTween.Diagnostics;

namespace MagicTween
{
    public static class MagicTweenSettings
    {
        public static LoggingMode loggingMode
        {
            get => _sharedStatic.Data.loggingMode;
            set => _sharedStatic.Data.loggingMode = value;
        }

        public static bool captureExceptions
        {
            get => _sharedStatic.Data.captureExceptions;
            set => _sharedStatic.Data.captureExceptions = value;
        }

        public static Ease defaultEase
        {
            get => _sharedStatic.Data.defaultEase;
            set => _sharedStatic.Data.defaultEase = value;
        }

        public static LoopType defaultLoopType
        {
            get => _sharedStatic.Data.defaultLoopType;
            set => _sharedStatic.Data.defaultLoopType = value;
        }

        public static bool defaultIgnoreTimeScale
        {
            get => _sharedStatic.Data.defaultIgnoreTimeScale;
            set => _sharedStatic.Data.defaultIgnoreTimeScale = value;
        }

        public static bool defaultAutoPlay
        {
            get => _sharedStatic.Data.defaultAutoPlay;
            set => _sharedStatic.Data.defaultAutoPlay = value;
        }

        public static bool defaultAutoKill
        {
            get => _sharedStatic.Data.defaultAutoKill;
            set => _sharedStatic.Data.defaultAutoKill = value;
        }

        static readonly SharedStatic<MagicTweenSettingsData> _sharedStatic = SharedStatic<MagicTweenSettingsData>.GetOrCreate<SharedStaticKey>();
        readonly struct SharedStaticKey { }

        internal static void Initialize()
        {
            var asset = MagicTweenSettingsAsset.Instance;

            if (asset != null)
            {
                _sharedStatic.Data = asset.settings;
            }
            else
            {
                _sharedStatic.Data = MagicTweenSettingsData.Default;
            }
        }
    }
}