using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using MagicTween;

public class PostProcessingSample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Volume volume;

    VolumeProfile profile;
    ColorAdjustments colorAdjustments;
    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;
    Sequence sequence;

    void Start()
    {
        // Get the required components from volume.profile
        profile = volume.profile;
        profile.TryGet(out colorAdjustments);
        profile.TryGet(out chromaticAberration);
        profile.TryGet(out lensDistortion);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PlayTweens();
    }

    void PlayTweens()
    {
        if (sequence.IsActive()) sequence.CompleteAndKill();
        sequence = Sequence.Create();

        // Magic Tween has extension methods that support Post Processing, making it easy to create screen effects.
        var tween1 = colorAdjustments.TweenPostExposure(4f, 0f, 0.12f).SetEase(Ease.OutQuad);
        var tween2 = chromaticAberration.TweenIntensity(0.5f, 0f, 0.25f);
        var tween3 = lensDistortion.TweenIntensity(-0.6f, 0f, 0.2f).SetEase(Ease.OutQuad);

        // Use Sequence.Join to play all effects simultaneously.
        sequence.Join(tween1)
            .Join(tween2)
            .Join(tween3);

        var rotationTween = target.TweenEulerAnglesZ(180f, 0.3f).SetRelative().SetEase(Ease.OutQuart);
        var scaleTween = target.TweenLocalScale(Vector3.one * 0.5f, 0.3f).SetInvert().SetEase(Ease.OutQuart);

        sequence.Join(rotationTween)
            .Join(scaleTween);
    }

    void OnDestroy()
    {
        // volume.profile creates duplicates and must be destroyed manually to avoid memory leaks.
        Destroy(profile);
    }
}
