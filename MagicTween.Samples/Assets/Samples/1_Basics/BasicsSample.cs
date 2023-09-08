using UnityEngine;
using MagicTween;

public class BasicsSample : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private Transform target3;

    void Start()
    {
        // Move to position (3.2f, 2f, 0f) in 4 seconds
        target1.TweenPosition(new Vector3(3.2f, 2f, 0f), 4f)
            .SetEase(Ease.OutQuad);

        // Rotate the z-axis to 180 degrees in 4 seconds
        target2.TweenEulerAngles(new Vector3(0f, 0f, 180f), 4f)
            .SetEase(Ease.InOutCubic);

        // Increase the scale to (1.2f, 1.2f, 1.2f) in 4 seconds
        target3.TweenLocalScale(new Vector3(1.2f, 1.2f, 1.2f), 4f)
            .SetEase(Ease.InSine);
    }
}
