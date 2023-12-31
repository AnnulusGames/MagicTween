using MagicTween;
using MagicTween.Plugins;

// To animate your own ComponentData values, you need to propagate the tween values through a component called 'Translator'.
// Define an unmanaged struct that inherits from ITweenTranslator.
public struct SampleTranslator : ITweenTranslator<float, SampleComponentData>
{
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
public partial class SampleTranslationSystem : TweenTranslationSystemBase<float, NoOptions, FloatTweenPlugin, SampleComponentData, SampleTranslator> { }
