using System;
using UnityEngine;

namespace MagicTween.Diagnostics
{
    internal static class Debugger
    {
        const string LOG_HEADER = "[MagicTween] ";

        public static void Log(object message, bool withHeader = true)
        {
            if (MagicTweenSettings.loggingMode is not LoggingMode.Full) return;
            
            if (withHeader) Debug.Log(LOG_HEADER + message.ToString());
            else Debug.Log(message);
        }

        public static void LogWarning(object message, bool withHeader = true)
        {
            if (MagicTweenSettings.loggingMode is LoggingMode.ErrorsOnly) return;

            if (withHeader) Debug.LogWarning(LOG_HEADER + message.ToString());
            else Debug.LogWarning(message);
        }

        public static void LogExceptionInsideTween(Exception ex)
        {
            if (MagicTweenSettings.captureExceptions)
            {
                LogWarning("captured an exception thrown inside tween. \n" + ex.ToString());
            }
            else
            {
                Debug.LogException(ex);
            }
        }


#if MAGICTWEEN_SUPPORT_URP
        public static void LogWarningIfPostProcessingInactive(UnityEngine.Rendering.VolumeComponent volumeComponent)
        {
            if (volumeComponent == null) return;
            if (MagicTweenSettings.loggingMode == LoggingMode.ErrorsOnly) return;
            if (!volumeComponent.active)
            {
                var typeName = volumeComponent.GetType().Name;
                LogWarning($"'{typeName}' is inactive. Set '{typeName}' to active so the tween values can be applied.");
            }
        }
#endif

#if MAGICTWEEN_SUPPORT_POSTPROCESSING
        public static void LogWarningIfPostProcessingInactive(UnityEngine.Rendering.PostProcessing.PostProcessEffectSettings settings)
        {
            if (settings == null) return;
            if (MagicTweenSettings.loggingMode == LoggingMode.ErrorsOnly) return;
            if (!settings.active)
            {
                var typeName = settings.GetType().Name;
                LogWarning($"'{typeName}' is inactive. Set '{typeName}' to active so the tween values can be applied.");
            }
        }
#endif
    }
}