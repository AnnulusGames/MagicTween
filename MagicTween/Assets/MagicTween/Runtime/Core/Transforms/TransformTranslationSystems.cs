#if !MAGICTWEEN_DISABLE_TRANSFORM_JOBS
using Unity.Burst;
using Unity.Mathematics;
using MagicTween.Plugins;

namespace MagicTween.Core.Transforms.Systems
{
    [BurstCompile]
    internal sealed partial class TweenTransformPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, NoOptions, Float3TweenPlugin, TransformPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, NoOptions, Float3TweenPlugin, TransformLocalPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformRotationTranslationSystem : TweenTransformTranslationSystemBase<quaternion, NoOptions, QuaternionTweenPlugin, TransformRotationTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalRotationTranslationSystem : TweenTransformTranslationSystemBase<quaternion, NoOptions, QuaternionTweenPlugin, TransformLocalRotationTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, NoOptions, Float3TweenPlugin, TransformEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesYTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformEulerAnglesZTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, NoOptions, Float3TweenPlugin, TransformLocalEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesYTranslator> { }
    
    [BurstCompile]
    internal sealed partial class TweenTransformLocalEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleTranslationSystem : TweenTransformTranslationSystemBase<float3, NoOptions, Float3TweenPlugin, TransformLocalScaleTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleXTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalScaleXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleYTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalScaleYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformLocalScaleZTranslationSystem : TweenTransformTranslationSystemBase<float, NoOptions, FloatTweenPlugin, TransformLocalScaleZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin, TransformPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin, TransformLocalPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin, TransformEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformEulerAnglesYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin, TransformLocalEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalEulerAnglesYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalScaleTranslationSystem : TweenTransformTranslationSystemBase<float3, PunchTweenOptions, Punch3TweenPlugin, TransformLocalScaleTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalScaleXTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalScaleXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalScaleYTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalScaleYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformPunchLocalScaleZTranslationSystem : TweenTransformTranslationSystemBase<float, PunchTweenOptions, PunchTweenPlugin, TransformLocalScaleZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakePositionTranslationSystem : TweenTransformTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin, TransformPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakePositionXTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakePositionYTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakePositionZTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalPositionTranslationSystem : TweenTransformTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin, TransformLocalPositionTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalPositionXTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalPositionXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalPositionYTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalPositionYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalPositionZTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalPositionZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin, TransformEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformEulerAnglesYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalEulerAnglesTranslationSystem : TweenTransformTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin, TransformLocalEulerAnglesTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalEulerAnglesXTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalEulerAnglesXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalEulerAnglesYTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalEulerAnglesYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalEulerAnglesZTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalEulerAnglesZTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalScaleTranslationSystem : TweenTransformTranslationSystemBase<float3, ShakeTweenOptions, Shake3TweenPlugin, TransformLocalScaleTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalScaleXTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalScaleXTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalScaleYTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalScaleYTranslator> { }

    [BurstCompile]
    internal sealed partial class TweenTransformShakeLocalScaleZTranslationSystem : TweenTransformTranslationSystemBase<float, ShakeTweenOptions, ShakeTweenPlugin, TransformLocalScaleZTranslator> { }
}
#endif