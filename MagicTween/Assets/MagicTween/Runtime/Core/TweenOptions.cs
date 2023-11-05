using UnityEngine;

namespace MagicTween
{
    public interface ITweenOptions { }
    public readonly struct NoOptions : ITweenOptions
    {
        [HideInInspector] readonly byte dummy;
    }
}