using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using MagicTween.Core;

namespace MagicTween.Editor
{
    static class MenuItems
    {
        [MenuItem("Assets/Create/Magic Tween/Magic Tween Settings")]
        public static void CreateSettingsAsset()
        {
            var asset = PlayerSettings.GetPreloadedAssets().OfType<MagicTweenSettingsAsset>().FirstOrDefault();
            if (asset != null)
            {
                throw new InvalidOperationException($"{nameof(MagicTweenSettingsAsset)} already exists in preloaded assets");
            }

            var assetPath = EditorUtility.SaveFilePanelInProject($"Save MagicTween Settings", "MagicTweenSettings", "asset", "", "Assets");

            if (string.IsNullOrEmpty(assetPath)) return;

            var folderPath = Path.GetDirectoryName(assetPath);
            if (!string.IsNullOrEmpty(folderPath) && !Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var instance = ScriptableObject.CreateInstance<MagicTweenSettingsAsset>();
            AssetDatabase.CreateAsset(instance, assetPath);
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            preloadedAssets.Add(instance);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            AssetDatabase.SaveAssets();
        }
    }
}