using UnityEngine;
using MagicTween;

public class PunchSample : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private Transform target3;

    void Start()
    {
        // You can use Punch**() to create a tween that vibrates with a given strength.
        target1.PunchPositionY(4f, 1.5f)
            .SetEase(Ease.OutSine);

        // You can set the frequency using SetFrequency(). Default value is 10.
        target2.PunchPositionY(-4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetFreqnency(20);

        // You can also set the vibration damping ratio using SetDampingRatio(). Default value is 1f.
        target3.PunchPositionY(4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetDampingRatio(0f);
    }
}