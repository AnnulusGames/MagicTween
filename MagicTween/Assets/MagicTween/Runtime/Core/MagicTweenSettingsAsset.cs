using UnityEngine;

namespace MagicTween.Core
{
    public sealed class MagicTweenSettingsAsset : ScriptableObject
    {
        public static MagicTweenSettingsAsset Instance => _instance;
        static MagicTweenSettingsAsset _instance;
        void OnEnable()
        {
            _instance = this;
        }

        public MagicTweenSettingsData settings = MagicTweenSettingsData.Default;

        public void ResetSettings()
        {
            settings = MagicTweenSettingsData.Default;
        }
    }
}