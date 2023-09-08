using UnityEngine;
using UnityEngine.UI;
using MagicTween;

public class TextSample : MonoBehaviour
{
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;

    void Start()
    {
        text1.text = string.Empty;
        text2.text = string.Empty;
        text3.text = string.Empty;

        // You can use string tween to create a typewriter-like effect.
        text1.TweenText("Hello, World!", 1.1f);

        // You can also fill blank spaces with random characters by setting ScrambleMode().
        text2.TweenText("Hello, World!", 1.1f)
            .SetScrambleMode(ScrambleMode.Lowercase);

        // Add SetRichTextEnabled() if you want to use rich text tags.
        text3.TweenText("<color=red><size=45>Hello,</size></color> <b>World!</b>", 1.1f)
            .SetRichTextEnabled();
    }
}
