# Migration Guides

Magic Tween is an actively developed library, and there may be breaking changes associated with version updates. Here, we provide migration guides for transitioning to different versions.

## v0.1 to v0.2

### Overview

In the transition from v0.1 to v0.2, the design of ECS tweens has been completely revamped. This means that you'll need to make some changes to your code when using Magic Tween with ECS.

### Changes related to Tween.Entity

Starting from v0.2, `Tween.Entity.To()` and `Tween.Entity.FromTo()` require the specification of the component type as a type argument:

```cs
Entity entity;
float endValue;
float duration;

// v0.1
Tween.Entity.To<ExampleTranslator>(entity, endValue, duration);

// v0.2
Tween.Entity.To<ExampleComponent, ExampleTranslator>(entity, endValue, duration);
```

### Renaming of Translator Structs for LocalTransform Support

In line with the above change, we've reduced the verbosity by renaming Translator structs that support LocalTransform. In v0.2, we've removed the "LocalTransform" prefix from their names:

```cs
Entity entity;
float3 endValue;
float duration;

// v0.1
Tween.Entity.To<LocalTransformPositionTranslator>(entity, endValue, duration);

// v0.2
Tween.Entity.To<LocalTransform, PositionTranslator>(entity, endValue, duration);
```

## Changes related to Custom Translators

We have removed the `TargetEntity` property from `ITweenTranslator`. Starting from v0.2, tracking the target entity is handled by dedicated components.

```cs
// v0.1
public struct ExampleTranslator : ITweenTranslator<float, ExampleComponent>
{
    // TargetEntity is required for entity tracking
    public Entity TargetEntity { get; set; }

    public void Apply(ref ExampleComponent component, in float value)
    {
        component.value = value;
    }

    public float GetValue(ref ExampleComponent component)
    {
        return component.value;
    }
}

// v0.2
public struct ExampleTranslator : ITweenTranslator<float, ExampleComponent>
{
    // TargetEntity is no longer required

    public void Apply(ref ExampleComponent component, in float value)
    {
        component.value = value;
    }

    public float GetValue(ref ExampleComponent component)
    {
        return component.value;
    }
}
```

Additionally, `TweenTranslationSystemBase` now has type arguments for specifying TweenOptions and TweenPlugins. Starting from v0.2, you must specify both of these when creating a system:

```csharp
// v0.1
public partial class ExampleTweenTranslationSystem : TweenTranslationSystemBase<float, ExampleComponent, ExampleTranslator> { }

// v0.2
public partial class ExampleTweenTranslationSystem : TweenTranslationSystemBase<float, NoOptions, FloatTweenPlugins, ExampleComponent, ExampleTranslator> { }
```