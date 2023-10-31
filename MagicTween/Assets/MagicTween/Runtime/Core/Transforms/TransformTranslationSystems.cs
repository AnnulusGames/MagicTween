using Unity.Burst;
using Unity.Mathematics;
using MagicTween.Plugins;

namespace MagicTween.Core.Transforms
{
    [BurstCompile]
    internal sealed partial class TweenTransformPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, Float3TweenPlugin, TransformPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, Float3TweenPlugin, TransformLocalPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformRotationTranslationSystem : TweenTransformTranslationSystemBase<quaternion, QuaternionTweenPlugin, TransformRotationTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalRotationTranslationSystem : TweenTransformTranslationSystemBase<quaternion, QuaternionTweenPlugin, TransformLocalRotationTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, Float3TweenPlugin, TransformEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformEulerAnglesYTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformEulerAnglesZTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, Float3TweenPlugin, TransformLocalEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalEulerAnglesYTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleTranslationSystem : TweenTransformTranslationSystemBase<float3, Float3TweenPlugin, TransformLocalScaleTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleXTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalScaleXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleYTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalScaleYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleZTranslationSystem : TweenTransformTranslationSystemBase<float, FloatTweenPlugin, TransformLocalScaleZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, Punch3TweenPlugin, TransformPositionTranslator> { }
}