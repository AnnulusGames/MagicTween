using System.Linq;
using UnityEngine;
using MagicTween;

public class PathSample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] points;

    void Start()
    {
        var positions = points.Select(x => x.position).ToArray();

        // You can create a tween that passes through multiple points using TweenPath().
        target.TweenPath(positions, 5f).SetEase(Ease.InOutSine);
    }
}
