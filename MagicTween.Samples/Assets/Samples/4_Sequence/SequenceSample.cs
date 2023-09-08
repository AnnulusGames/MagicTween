using UnityEngine;
using MagicTween;

public class SequenceSample : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;

    [Header("Sprites")]
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite nineSlicedSprite;
    [SerializeField] private Sprite circleSprite;

    void Start()
    {
        // Create a new Sequence with 'Sequence.Create()' 
        // Note: Don't create a Sequence with 'new Sequence()', which returns an invalid Sequence
        //       because it doesn't include the necessary initialization.
        var sequence = Sequence.Create();

        // The tweens added by Append() will be played in the order they were added when the Sequence is played.
        sequence.Append(
            target.transform.TweenPositionX(5f, 1f).SetEase(Ease.InExpo)
        );

        // A tween added by Join() will be played at the same time as the tween added immediately before.
        sequence.Join(
            target.transform.TweenEulerAnglesZ(360f, 1f).SetEase(Ease.InCirc)
        );

        // You can add callbacks at any point using InsertCallback().
        sequence.InsertCallback(0.9f, () => target.sprite = squareSprite);
        sequence.InsertCallback(0.93f, () => target.sprite = nineSlicedSprite);
        sequence.InsertCallback(0.95f, () => target.sprite = circleSprite);

        // Add intervals to a Sequence using AppendInterval().
        sequence.AppendInterval(0.32f);

        // It is also possible to apply options such as SetLoops() to Sequence.
        sequence.SetLoops(2, LoopType.Yoyo);
    }
}
