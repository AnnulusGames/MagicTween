using UnityEngine;
using TMPro;
using MagicTween;

public class DelayedCallSample : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float delay;

    void Start()
    {
        // You can use DelayedCall() to create a tween that processes after a specified number of seconds.
        Tween.DelayedCall(delay, () => tmpText.text = "Complete!");
    }
}
