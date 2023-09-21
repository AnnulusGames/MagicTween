using UnityEngine;
using TMPro;
using MagicTween;

public class TextMeshProSample : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText1;
    [SerializeField] private TMP_Text tmpText2;
    [SerializeField] private TMP_Text tmpText3;
    [SerializeField] private TMP_Text tmpText4;
    [SerializeField] private TMP_Text tmpText5;
    [SerializeField] private TMP_Text tmpText6;
    [SerializeField] private TMP_Text tmpText7;

    void Start()
    {
        tmpText1.text = string.Empty;
        string text1 = "Lorem ipsum dolor sit amet,";
        tmpText1.TweenText(text1, text1.Length * 0.05f);

        for (int i = 0; i < tmpText2.GetCharCount(); i++)
        {
            tmpText2.TweenCharOffset(i, Vector2.up * 7f, 0.3f)
                .SetDelay(i * 0.05f)
                .SetEase(Ease.InOutSine)
                .SetLoops(2, LoopType.Yoyo);

            tmpText2.TweenCharColorAlpha(i, 0f, 0.6f)
                .SetDelay(i * 0.05f)
                .SetInvert(InvertMode.Immediate);
        }

        for (int i = 0; i < tmpText3.GetCharCount(); i++)
        {
            tmpText3.TweenCharScale(i, Vector3.zero, 0.3f)
                .SetDelay(i * 0.05f)
                .SetEase(Ease.OutSine)
                .SetInvert(InvertMode.Immediate);
        }

        for (int i = 0; i < tmpText4.GetCharCount(); i++)
        {
            tmpText4.TweenCharColor(i, Color.red, 0.3f)
                .SetDelay(i * 0.05f)
                .SetEase(Ease.OutSine);
        }

        for (int i = 0; i < tmpText5.GetCharCount(); i++)
        {
            tmpText5.TweenCharEulerAngles(i, new Vector3(0f, 90f, 0f), 0.3f)
                .SetDelay(i * 0.05f)
                .SetEase(Ease.OutSine)
                .SetInvert(InvertMode.Immediate);
        }

        for (int i = 0; i < tmpText6.GetCharCount(); i++)
        {
            tmpText6.PunchCharScale(i, Vector3.one * 0.7f, 0.7f)
                .SetFrequency(5)
                .SetDelay(i * 0.05f)
                .SetEase(Ease.OutSine);
        }

        for (int i = 0; i < tmpText7.GetCharCount(); i++)
        {
            tmpText7.ShakeCharOffset(i, Random.insideUnitSphere * 30f, 3f)
                .SetFrequency(1)
                .SetDampingRatio(1f)
                .SetEase(Ease.OutSine);
        }
    }
}
