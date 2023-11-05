using Unity.Mathematics;
using UnityEngine;
using MagicTween;

// You need to add TweenPluginAttribute. This allows SourceGenerator to recognize the type and generate the necessary code.
[TweenPlugin]
// Specify the value type in the first argument and the option type (NoOptions if not required) in the second argument.
public readonly struct RectTweenPlugin : ICustomTweenPlugin<Rect, NoOptions>
{
    // Write code that returns the value calculated based on TweenEvaluationContext.
    public Rect Evaluate(in Rect startValue, in Rect endValue, in NoOptions options, in TweenEvaluationContext context)
    {
        // Use relative values in calculations if SetRelative(true) is set.
        var resolvedEndValue = context.IsRelative ? 
            new Rect(startValue.x + endValue.x, startValue.y + endValue.y, startValue.width + endValue.width, startValue.height + endValue.height) :
            endValue;

        // If SetInvert(true) is set, swaps the start and end values.
        var rectA = context.IsInverted ? resolvedEndValue : startValue;
        var rectB = context.IsInverted ? startValue : resolvedEndValue;

        // Get the current tween progress (0 to 1)
        var t = context.Progress;

        // Calculates the linearly interpolated value.
        var x = math.lerp(rectA.x, rectB.x, t);
        var y = math.lerp(rectA.y, rectB.y, t);
        var width = math.lerp(rectA.width, rectB.width, t);
        var height = math.lerp(rectA.height, rectB.height, t);

        // returns the result.
        return new Rect(x, y, width, height);
    }
}