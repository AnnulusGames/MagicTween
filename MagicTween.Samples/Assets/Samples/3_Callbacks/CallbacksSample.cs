using UnityEngine;
using TMPro;
using MagicTween;

public class CallbacksSample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TMP_Text tmpText;

    Tween tween;

    void Start()
    {
        tween = target.TweenPosition(Vector2.up * 5f, 3f)
            .SetEase(Ease.InCubic)
            .SetRelative()
            .SetLoops(3, LoopType.Yoyo)
            .SetDelay(2f);

        // You can add callbacks to tween using On**()
        tween.OnStart(() => tmpText.text += "OnStart\n")
            .OnPlay(() => tmpText.text += "OnPlay\n")
            .OnPause(() => tmpText.text += "OnPause\n")
            .OnStepComplete(() => tmpText.text += "OnStepComplete\n")
            .OnComplete(() => tmpText.text += "OnComplete\n")
            .OnKill(() => tmpText.text += "OnKill\n");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tween.IsActive()) tween.TogglePause();
        }
    }
}
