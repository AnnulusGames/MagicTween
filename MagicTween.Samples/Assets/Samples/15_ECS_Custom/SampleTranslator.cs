using Unity.Entities;
using MagicTween;

// To animate your own ComponentData values, you need to propagate the tween values through a component called 'Translator'.
// Define an unmanaged struct that inherits from ITweenTranslator.
public struct SampleTranslator : ITweenTranslator<float, SampleComponentData>
{
    // 'TargetEntity' is used by the System to track the entity targeted for tweening.
    public Entity TargetEntity { get; set; }

    // In 'Apply()' method, write the process to apply the current value to the target component.
    public void Apply(ref SampleComponentData component, in float value)
    {
        component.value = value;
    }

    // In 'GetValue()' method, write the process that returns the current value of the component.
    public float GetValue(ref SampleComponentData component)
    {
        return component.value;
    }
}

// In addition, define a System class to operate the defined Translator.
// Define it as a partial class that inherits TweenTranslationSystemBase, and leave it empty without writing any processing.
public partial class SampleTranslationSystem : TweenTranslationSystemBase<float, SampleComponentData, SampleTranslator> { }
