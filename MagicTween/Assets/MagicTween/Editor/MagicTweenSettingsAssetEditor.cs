using UnityEngine;
using UnityEditor;
using MagicTween.Core;

namespace MagicTween.Editor
{
    [CustomEditor(typeof(MagicTweenSettingsAsset))]
    public sealed class MagicTweenSettingsAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            var isPlaying = Application.isPlaying;

            if (isPlaying)
            {
                EditorGUILayout.HelpBox("You cannot change settings from inspector in play mode.", MessageType.Warning);
            }

            var settingsProperty = serializedObject.FindProperty("settings");

            using (new EditorGUI.DisabledScope(isPlaying))
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("loggingMode"), new GUIContent("Logging Mode"));
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("captureExceptions"), new GUIContent("Capture Exceptions"));
                    }
                    EditorGUILayout.Space(2);
                }

                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Default Tween Parameters", EditorStyles.boldLabel);
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("defaultAutoPlay"), new GUIContent("Auto Play"));
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("defaultAutoKill"), new GUIContent("Auto Kill"));
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("defaultEase"), new GUIContent("Ease"));
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("defaultLoopType"), new GUIContent("Loop Type"));
                        EditorGUILayout.PropertyField(settingsProperty.FindPropertyRelative("defaultIgnoreTimeScale"), new GUIContent("Ignore Time Scale"));
                    }
                    EditorGUILayout.Space(2);
                }

                if (GUILayout.Button("Reset"))
                {
                    ((MagicTweenSettingsAsset)target).ResetSettings();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}