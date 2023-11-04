using MagicTween;
using UnityEngine;

public class CustomTweenPluginSample : MonoBehaviour
{
    [SerializeField] private Rect startValue = new Rect(10f, 10f, 0f, 0f);
    [SerializeField] private Rect endValue = new Rect(10f, 10f, 400f, 400f);
    [SerializeField] private float duration = 5f;

    private Rect _currentRect;
    private Texture _texture;

    void Start()
    {
        // Create a Texture used to draw the GUI.
        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        _texture = texture;

        // Create a Tween by specifying the custom TweenPlugin as a type argument.
        Tween.FromTo<Rect, NoOptions, RectTweenPlugin>(x => _currentRect = x, startValue, endValue, duration);
    }

    void OnGUI()
    {
        GUI.DrawTexture(_currentRect, _texture, ScaleMode.StretchToFill, true, 0);
    }
}
