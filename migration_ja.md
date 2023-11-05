# Migration Guides

Magic Tweenは現在も開発中のライブラリであり、バージョンの移行に伴った破壊的変更が行われる可能性があります。ここでは、バージョンのアップデートに伴う移行のガイドを提供します。

## v0.1 to v0.2

### 概要

v0.1からv0.2では、ECSに関するTweenの設計を一新しました。これにより、ECSでMagic Tweenを用いる際のコードにいくつか変更を加える必要が生じます。

### Tween.Entityに関する変更

`Tween.Entity.To()`および`Tween.Entity.FromTo()`はv0.2よりComponentの型引数の指定が必要になりました。

```cs
Entity entity;
float endValue;
float duration;

// v0.1
Tween.Entity.To<ExampleTranslator>(entity, endValue, duration);

// v0.2
Tween.Entity.To<ExampleComponent, ExampleTranslator>(entity, endValue, duration);
```

### LocalTransformに対応したTranslatorの構造体の名前を変更

上記の変更に伴い、記述量を削減するためLocalTransformに対応したTranslatorの名前を変更しました。v0.2では接頭辞のLocalTransformを削除した名前に変更されています。

```cs
Entity entity;
float3 endValue;
float duration;

// v0.1
Tween.Entity.To<LocalTransformPositionTranslator>(entity, endValue, duration);

// v0.2
Tween.Entity.To<LocalTransform, PositionTranslator>(entity, endValue, duration);
```

## カスタムTranslatorに関する変更

`ITweenTranslator`から`TargetEntity`のプロパティを削除しました。v0.2からは対象のEntityの追跡は専用のComponentによって行われます。

```cs
// v0.1
public struct ExampleTranslator : ITweenTranslator<float, ExampleComponent>
{
    // Entityの追跡にTargetEntityが必要
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
    // TargetEntityが不要に

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

また、`TweenTranslationSystemBase`にTweenOptionsとTweenPluginを指定する型引数が追加されました。v0.2からはこれら二つを指定してSystemを作成する必要があります。

```cs
// v0.1
public partial class ExampleTweenTranslationSystem : TweenTranslationSystemBase<float, ExampleComponent, ExampleTranslator> { }

// v0.2
public partial class ExampleTweenTranslationSystem : TweenTranslationSystemBase<float, NoOptions, FloatTweenPlugins, ExampleComponent, ExampleTranslator> { }
```