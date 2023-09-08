using UnityEngine;
using MagicTween;

public class ShakeSample : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private Transform target3;
    [SerializeField] private Transform target4;
    [SerializeField] private Transform target5;

    void Start()
    {
        // You can create a tween that randomly shakes by using Shake**()
        target1.ShakePositionY(4f, 1.5f)
            .SetEase(Ease.OutSine);

        // You can set the frequency using SetFrequency(). Default value is 10.
        target2.ShakePositionY(-4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetFreqnency(20);

        // You can also set the vibration damping ratio using SetDampingRatio(). Default value is 1f.
        target3.ShakePositionY(4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetDampingRatio(0f);

        // You can specify the random number seed by using SetRandomSeed().
        target4.ShakePositionY(4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetRandomSeed(10);
        target5.ShakePositionY(4f, 1.5f)
            .SetEase(Ease.OutSine)
            .SetRandomSeed(10);
    }
}