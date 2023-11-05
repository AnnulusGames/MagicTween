using UnityEngine;

namespace MagicTween.Tests
{
    public static class TestHelper
    {
        public static void Setup3DScene()
        {
            var camera = new GameObject("Main Camera").AddComponent<Camera>();
            camera.transform.position = new Vector3(0, 0, -7);
            camera.orthographic = false;
            camera.clearFlags = CameraClearFlags.Skybox;

            var light = new GameObject("Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.transform.eulerAngles = new Vector3(50, -30, 0);
        }
    }
}