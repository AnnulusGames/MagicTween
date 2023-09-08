using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MagicTween;

public class SettingsSample : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;

    [Header("UI")]
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text timeScaleText;

    void Start()
    {
        // You can add settings to customize tween behavior using Set**()
        target1.TweenPosition(Vector2.right * 6f, 3f)
            .SetEase(Ease.InOutCubic)
            .SetRelative()
            .SetLoops(-1, LoopType.Restart)
            .SetInvert();

        target2.TweenPosition(Vector2.right * 6f, 3f)
            .SetEase(Ease.OutBack)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo)
            .SetIgnoreTimeScale();

        slider.onValueChanged.AddListener(x => 
        {
            Time.timeScale = x;
            timeScaleText.text = "Time.timeScale = " + x.ToString("F2");
        });
    }
}
