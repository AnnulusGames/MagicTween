using UnityEngine;
using TMPro;
using MagicTween;

public class CustomTweenSample : MonoBehaviour
{
    [SerializeField] private float foo;

    [Header("Text")]
    [SerializeField] private TMP_Text tmpText;

    void Start()
    {
        // Tween the value of 'foo' to 10f in 5 seconds.
        Tween.To(this, target => target.foo, (target, x) => target.foo = x, 10f, 5f);

        // This is also possible, but if the target object exists, use the above method whenever possible.
        // The tween below causes extra allocation because the lambda expression captures an external variable.
        // Tween.To(() => foo, x => foo = x, 10f, 5f);
    }

    void Update()
    {
        tmpText.text = "foo: " + foo.ToString("F1");
    }
}
