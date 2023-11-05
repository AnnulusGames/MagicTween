# Magic Tween
 Extremely fast, GC-free and customizable tween library implemented with Unity ECS

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween/Assets/MagicTween/Documentation~/Header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[English README is here](README.md)

## 概要

Magic TweenはUnityのECS(Entity Component System)で実装されたハイパフォーマンスなトゥイーンライブラリです。

従来のコンポーネントに対応した高性能なトゥイーンの機能に加え、ECS向けのよりハイパフォーマンスなAPIも合わせて提供します。

## 目次
- [概要](#概要)
- [目次](#目次)
- [特徴](#特徴)
- [パフォーマンス](#パフォーマンス)
- [サンプル](#サンプル)
- [セットアップ](#セットアップ)
- [基本の使い方](#基本の使い方)
- [任意の値をトゥイーンする](#任意の値をトゥイーンする)
- [Tweenの制御](#tweenの制御)
- [Tweenの情報を取得する](#tweenの情報を取得する)
- [設定の追加](#設定の追加)
- [コールバック](#コールバック)
- [DelayedCall / Empty](#delayedcall--empty)
- [Sequence](#sequence)
- [コルーチンを用いたTweenの待機](#コルーチンを用いたtweenの待機)
- [ログ出力](#ログ出力)
- [プロジェクト設定](#プロジェクト設定)
- [JobによるTransformのTweenの高速化](#jobによるtransformのtweenの高速化)
- [TextMesh Pro](#textmesh-pro)
- [UniRx](#unirx)
- [UniTask](#unitask)
- [独自のTweenPluginを作成する](#独自のtweenpluginを作成する)
- [ECS向けの実装](#ecs向けの実装)
- [その他の機能](#その他の機能)
- [最適化](#最適化)
- [実験的機能](#実験的機能)
- [既知の問題点](#既知の問題点)
- [サポート](#サポート)
- [ライセンス](#ライセンス)

## 特徴

* ECSで実装された高性能なトゥイーンライブラリ
* 一部の特殊なTweenを除き、全てGCアロケーションなし
* 多くのコンポーネントに対応した拡張メソッドを追加
* Transformに特化した圧倒的速度のトゥイーンのサポート
* Tween.Toによって任意の値をトゥイーン可能
* メソッドチェーンを用いて様々な設定を適用可能
* Sequenceによる複雑なアニメーションの作成
* コールバックによる処理の追加
* コルーチンによるTweenの待機をサポート
* TextMesh Proの文字のトゥイーンをサポート
* UniRxによるObservableへの変換をサポート
* UniTaskによるasync/awaitをサポート
* カスタムTweenPluginを用いて独自の型をトゥイーン可能
* ECS向けのよりハイパフォーマンスな実装

## パフォーマンス

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_64000_floats.png" width="800">

通常のクラスのfloatの値を`Tween.To()`でトゥイーンさせる場合、他のライブラリの2〜5倍以上高速に動作します。ECSのコンポーネント内のfloatの値を`Tween.Entity.To()`でトゥイーンさせる場合、さらに高速な動作を実現することが可能になります。

また、トゥイーンの作成ごとに余計なGCアロケーションは一切発生しません。(stringなどのトゥイーンを除きます)

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_50000_transform_position.png" width="800">

さらに`MAGICTWEEN_ENABLE_TRANSFORM_JOBS`を有効化することでTransformに特化したトゥイーンを作成できます。これにより、大量のTransformをトゥイーンさせる際のパフォーマンスが劇的に向上します。

パフォーマンスの詳細については`MagicTween.Benchmarks`プロジェクトの[README](https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/README_JA.md)を参照してください。

## サンプル

`MagicTween.Samples`プロジェクトには、実際にMagic Tweenを用いて実装されたサンプルがいくつも用意されています。
詳細についてはプロジェクトの[README](https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Samples/README_JA.md)を参照してください。

## セットアップ

### 要件

* Unity 2022.1 以上
* Entities 1.0.0 以上
* Burst 1.8.8 以上

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/AnnulusGames/MagicTween.git?path=/MagicTween/Assets/MagicTween
```

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.magic-tween": "https://github.com/AnnulusGames/MagicTween.git?path=/MagicTween/Assets/MagicTween"
    }
}
```

### 移行ガイド

Magic Tweenは現在開発中のライブラリであり、バージョン毎に破壊的変更が行われる可能性があります。過去バージョンからの移行に関しては[移行ガイド](migration_ja.md)を参照してください。

## 基本の使い方

Magic Tweenを導入することで、従来のUnityのコンポーネントにTweenを作成するための拡張メソッドが多数追加されます。
以下は、拡張メソッドを用いてTransformの位置をアニメーションさせる例です。

```cs
// 現在の位置から(1, 2, 3)の位置に5秒かけて移動する
transform.TweenPosition(
    new Vector3(1f, 2f, 3f), // 終了値
    5f // 値の変更にかかる時間 
);

// (0, 0, 0)の位置から(1, 2, 3)の位置に5秒かけて移動する
transform.TweenPosition(
    new Vector3(0f, 0f, 0f), // 開始値
    new Vector3(1f, 2f, 3f), // 終了値
    5f // 値の変更にかかる時間 
);
```

> **Warning**
> 同じパラメータに対するTweenを複数同時に再生しないでください。値の反映が重複して思わぬ動作を引き起こす可能性があります。(TweenPositionXとTweenPositionYなど、対象の軸が異なれば問題なく動作します。)

> **Warning**
> Tweenの作成をエディタ上で行うことはできません。

### 拡張メソッド

Magic Tweenでは、Unityに含まれるほとんどのコンポーネントに対してトゥイーン用の拡張メソッドを用意しています。後述する`Tween.To()`よりも簡潔な記述が可能であり、パフォーマンスに関しても最適化されているため、利用可能な場合には拡張メソッドを優先して使用するようにしてください。

利用可能な拡張メソッドに関しては、こちらから確認できます。(現在wikiを作成中です)

### 拡張メソッドの分類
トゥイーン用に追加されている拡張メソッドにはいくつかの種類に分類されます。

| メソッド名 | 動作 |
| - | - |
| Tween... | 特定のフィールド/プロパティに対するTweenを作成します。 |
| Punch... | 特定のフィールド/プロパティの値を振動させるTweenを作成します。 |
| Shake... | 特定のフィールド/プロパティの値をランダムに振動させるTweenを作成します。 |
| Set... | Tweenの挙動をカスタマイズする設定を追加します。 |
| On... | Tweenの特定のタイミングにコールバックを追加します。 |
| Log... | Tweenの情報やコールバックをConsoleに出力します。 |
| WaitFor... | Tweenをコルーチンで待機します。 |
| AwaitFor... | Tweenをasync/awaitで待機します。この拡張メソッドを使用するにはUniTaskが必要です。 |

## 任意の値をトゥイーンする

`Tween.To()`メソッドを使うことで任意の値をアニメーションさせることが可能です。

```cs
float foo;

float endValue = 10f;
float duration = 2f;

Tween.To(
    () => foo,
    x => foo = x,
    endValue,
    duration
);
```

変数を定義せずに値だけを使いたい場合は`Tween.FromTo()`を利用できます。

```cs
float startValue = 0f;
float endValue = 10f;
float duration = 2f;

Tween.FromTo(
    x => Debug.Log("current value: " + x),
    startValue,
    endValue,
    duration
);
```

`Tween.Path()`を利用することで複数のポイントを通る曲線のTweenを作成できます。

```cs
Vector3 foo;

Vector3[] points;
float duration = 2f;

Tween.Path(
    () => foo,
    x => foo = x,
    points,
    duration
);
```

### アロケーションの回避
上の`To`や`FromTo`のメソッドは外部の変数をキャプチャするためアロケーションが発生します。何らかのオブジェクトに対してトゥイーンを実行する際、第一引数に対象のオブジェクトを指定することでラムダ式による余計なアロケーションを削減できます。

```cs
// fooというフィールドを持つクラス
ExampleClass target;

float endValue = 10f;
float duration = 2f;

// 第一引数にオブジェクトを渡してアロケーションを回避
Tween.To(
    target,
    obj => obj.foo,
    (obj, x) => obj.foo = x,
    endValue,
    duration
);
```

## Tweenの制御

基本的に作成したTweenは自動で再生/破棄されるため、明示的な操作は必要ありません。しかし、無限ループするTweenを制御する場合など、手動で操作を行う必要がある場面も存在します。そのような場合は`Tween`構造体を介して操作を行うことができます。

```cs
Tween tween = transform.TweenPosition(Vector3.up. 2f);

// Tweenを開始する/再開する
tween.Play();

// Tweenを一時停止する
tween.Pause();

// Tweenを最初から再生する
tween.Restart();

// Tweenを完了させる
tween.Complete();

// Tweenを破棄する
tween.Kill();

// Tweenを完了し、そのまま破棄する
tween.CompleteAndKill();
```

また、再生中の全てのTweenに対してまとめて操作を行うことも可能です。引数にIDを指定すると、IDが一致するTweenのみを操作できます。

```cs
// 再生中の全てのTweenを破棄する
Tween.KillAll();

// SetIdでIDが1に設定されたTweenを全て完了する
Tween.CompleteAll(1);

// SetIdでIDが"Alpha"に設定されたTweenを全て一時停止する
Tween.PauseAll("Alpha");
```

## Tweenの情報を取得する

`IsActive()`を使うことで、Tweenが現在アクティブかを確認できます。
Tweenの操作を行う際、アクティブでない可能性がある場合にはこのメソッドを用いてチェックを行なってください。

```cs
// TweenがアクティブならKillする
if (tween.IsActive()) tween.Kill();
```

また、`GetDuration()`でTweenの長さを取得できます。

```cs
float duration = tween.GetDuration();
```

## 設定の追加

Set系のメソッドを用いることでTweenの挙動をカスタマイズすることが可能です。これらのメソッドは、メソッドチェーンを用いて簡潔に記述することが可能です。
以下のコードは、Tweenに独自の設定を適用する例です。

```cs
transform.TweenLocalScale(Vector3.one * 2f, 5f)
    .SetEase(Ease.OutSine) // イージング関数をOutSineに設定
    .SetLoops(3, LoopType.Restart) // 3回繰り返す
    .SetDelay(1f); // 開始時に1秒間遅延させる
```

### SetEase

Tweenに用いるイージング関数を設定します。
`AnimationCurve`を渡すことで独自のイージングを利用することも可能です。

### SetLoops

Tweenを繰り返す回数を設定します。デフォルトでは1に設定されています。
-1を設定することで、停止されるまで無限に繰り返すTweenを作成することも可能です。

また、第2引数に`LoopType`を設定することで繰り返し時の挙動を設定することが可能です。

| LoopType | 動作 | 
| - | - | 
| LoopType.Restart | デフォルトの設定。ループ終了時に開始値にリセットされます。| 
| LoopType.Yoyo | 開始値と終了地を往復するように値をアニメーションさせます。| 
| LoopType.Increment | ループごとに値が増加します。このオプションはstringのTween、またはSequenceには適用されません。 | 

### SetPlaybackSpeed

Tweenの再生速度を設定します。デフォルトでは1に設定されており、0未満の値はサポートされていません。

### SetDelay

開始時の遅延を秒単位で設定します。

### SetIgnoreTimeScale

TimeScaleによる影響を無視します。

### SetRelative

終了値を開始値からの相対的な値に設定します。

### SetInvert

開始値と終了値を入れ替えます。`InvertMode`を設定することで挙動を調整可能です。

| InvertMode | 動作 |
| - | - |
| InvertMode.None | 通常通り、開始値から終了値に移動します。 |
| InvertMode.Immidiate | Tweenの起動と同時に終了値に移動し、それから開始値に向かって移動します。 |
| InvertMode.AfterDelay | Tweenの開始まで待機した後に終了値に移動し、それから開始値に向かって移動します。 |

### SetId

TweenにIDを設定します。KillAll等の操作を行う際、同じIDのTweenに対して一括で操作を行うことが可能です。
IDにはint、または32bytes以下のstringを渡すことができます。(初期値はintが0、stringが空文字)

### SetLink

Tweenのライフサイクルを対象のGameObjectに紐付けます。
第2引数に`LinkBehaviour`を設定することで挙動を変更することが可能です。ただし、どのオプションを設定してもOnDestroyのタイミングでKillが呼ばれます。

### SetAutoPlay

Tweenを自動で再生するかを設定できます。(デフォルトではtrueに設定されています。)

falseに設定した場合、Tweenを再生するには手動で`Play()`を呼び出す必要があります。

### SetAutoKill

終了時にTweenを自動で削除するかを設定できます。(デフォルトではtrueに設定されています。)

falseに設定した場合、Tweenを削除するには手動で`Kill()`を呼び出す必要があります。このオプションは同じTweenを何度も使い回したい場面で有効です。

### SetFrequency (Punch, Shake)

Punch、Shake系のTweenに対して利用可能なオプションで、振動の振動数を設定します。(デフォルトでは10に設定されています。)

### SetDampingRatio (Punch, Shake)

Punch、Shake系のTweenに対して利用可能なオプションで、振動の減衰比を設定できます。1の場合は終了時に完全に減衰し、0の場合は全く減衰せずに振動します。(デフォルトでは1に設定されています。)

### SetRandomSeed (Shake)

Shake系のTweenに対して利用可能なオプションで、振動に用いる乱数のシード値を設定できます。このオプションは再生前に適用する必要があります。

### SetPathType (Path)

TweenPath系のTweenに対して利用可能なオプションで、各ポイントをどのように結ぶかを設定できます。

| PathType | 動作 |
| - | - |
| PathType.Linear | 各ポイントを直線で結びます。 |
| PathType.CatmullRom | 各ポイントをCatmull-Romスプライン曲線で結びます。 |

### SetClosed (Path)

TweenPath系のTweenに対して利用可能なオプションで、閉じたパスとして設定し、開始地点に戻ってくるようにすることができます。

### SetRoundingMode (int, int2, int3, int4, long)

小数点以下の値の丸め方を設定します。このオプションは整数値を用いる型にのみ適用可能です。

| RoundingMode | 動作 |
| - | - |
| RoundingMode.ToEven | デフォルトの設定。最も近い整数値に値を丸め、値が中間にある場合は最も近い偶数に丸めます。 |
| RoundingMode.AwayFromZero | 一般的な四捨五入の動作。最も近い整数値に値を丸め、値が中間にある場合は0から遠ざかる方向に値を丸めます。 |
| RoundingMode.ToZero | 0に近づく方向に値を丸めます。 |
| RoundingMode.ToPositiveInfinity | 正の無限大に近づく方向に値を丸めます。 |
| RoundingMode.ToNegativeInfinity | 負の無限大に近づく方向に値を丸めます。 |

### SetScrambleMode (string)

まだ表示されていない文字の部分をランダムな文字で埋めることができます。このオプションは文字列のTweenにのみ適用可能です。

| ScrambleMode | 動作 |
| - | - |
| ScrambleMode.None | デフォルトの設定。まだ表示されていない部分には何も表示されません。 |
| ScrambleMode.Uppercase | ランダムな大文字のアルファベットで空白を埋めます。 |
| ScrambleMode.Lowercase | ランダムな小文字のアルファベットで空白を埋めます。 |
| ScrambleMode.Numerals | ランダムな数字で空白を埋めます。 |
| ScrambleMode.All | ランダムな大文字/小文字のアルファベット、または数字で空白を埋めます。 |
| (ScrambleMode.Custom) | 指定された文字列の中のランダムな数字で空白を埋めます。このオプションは明示的に指定できず、SetScrambleModeの引数にstringを渡した際に設定されます。|

### SetRichTextEnabled (string)

RichTextのサポートを有効化し、RichTextタグが含まれるテキストの文字送りが可能になります。このオプションは文字列のTweenにのみ適用可能です。

## コールバック

Tweenの開始時や終了時など、特定のタイミングに何らかの処理を行いたい場合にはOn系のメソッドを使用します。コールバックのメソッドも他の設定と同様、メソッドチェーンを用いて記述できます。

```cs
transform.TweenPosition(new Vector3(1f, 2f, 3f), 5f)
    .SetLoops(5)
    .OnUpdate(() => Debug.Log("update"))
    .OnStepComplete(() => Debug.Log("stem complete"))
    .OnComplete(() => Debug.Log("complete"));
```

> **Note**
> 1つ以上のコールバックを有効化すると再生時のパフォーマンスが低下します。多くの場合パフォーマンスへの影響はごく僅かですが、大量にTweenを作成する際にはコールバックの利用を避けることを推奨します。

### OnPlay

Tweenが再生された時に呼ばれます。OnStartとは違い`SetDelay()`による遅延は無視され、一時停止後にPlayを呼び出した際にも呼ばれます。

### OnStart

Tweenが動作を開始した時に呼ばれます。`SetDelay()`で遅延が設定されている場合には、遅延が終了した際に呼ばれます。

### OnUpdate

Tweenの再生時に毎フレーム呼ばれます。

### OnStepComplete

`SetLoops()`が設定されている際、各ループの完了時に呼ばれます。

### OnComplete

Tweenの完了時に呼ばれます。

### OnKill

Tweenが破棄される際に呼ばれます。

### アロケーションの回避

`Tween.To()`や`Tween.FromTo()`等と同様、第一引数に対象のインスタンスを渡すことでラムダ式によるアロケーションを回避することが可能となっています。

```cs
// fooというフィールドを持つクラス
ExampleClass target;

float endValue = 10f;
float duration = 2f;

// OnUpdateにtargetを渡してラムダ式のアロケーションを回避
Tween.To(target, obj => obj.foo, (obj, x) => obj.foo = x, endValue, duration)
    .OnUpdate(target, obj => Debug.Log(obj.foo));
```

## DelayedCall / Empty

`Tween.DelayedCall()`を利用することで、一定時間後に指定された処理を行うだけのTweenを作成することができます。

```cs
// 3秒後にログを表示
Tween.DelayedCall(3f, () => Debug.Log("delayed call"));
```

また、`Tween.Empty()`を利用することで空のTweenを作成することも可能です。

```cs
// 3秒後に終了するTween
Tween.Empty(3f);

// DelayedCall()は内部で以下のようなコードを呼ぶ
Tween.Empty(3f)
    .OnStepComplete(() => Debug.Log("delayed call"))
    .OnComplete(() => Debug.Log("delayed call"));
```

## Sequence

Sequenceは複数のTweenをグループ化するための機能です。Sequenceを用いて複数のTweenを組み合わせることで、複雑なアニメーションを簡単に作成できます。

### Sequenceの作成

`Sequence.Create()`から新たなSequenceを取得します。

```cs
// Sequenceを新たに作成する
Sequence sequence = Sequence.Create();
```

### Tweenの追加

次にSequenceに含めるTweenを追加していきます。SequenceにはTweenを追加するための様々なメソッドが用意されています。これらを利用することでTweenを組み合わせ、複雑なアニメーションを構築できます。

Sequenceはその階層に関係なくネストすることが可能です。`SetDelay`や`SetLoops`などのオプションやコールバックはSequenceに追加した後も動作します。

### Append

`Append()`は末尾に連結する形でTweenを追加します。追加されたTweenは、Sequenceを再生すると順番に実行されていきます。

```cs
// 末尾にTweenを追加する
sequence.Append(transform.TweenPosition(new Vector3(1f, 0f, 0f), 2f))
    .Append(transform.TweenPosition(new Vector3(1f, 3f, 0f), 2f));
```

間隔を追加したり、コールバックを追加したりする際には`AppendInterval()`と`AppendCallback()`が利用できます。

```cs
// 末尾に待機時間を追加する
sequence.AppendInterval(1f);

// 末尾にコールバックを追加する
sequence.AppendCallback(() => Debug.Log("Hello!"));
```

### Prepend

先頭にTweenを追加したい場合には`Prepend()`が利用できます。この場合、すでに追加されているTweenは追加したTweenの長さの分だけ後ろに移動します。

```cs
// 先頭にTweenを追加する
sequence.Prepend(transform.TweenPosition(new Vector3(1f, 0f, 0f), 2f));
```

`PrependInterval()`と`PrependCallback()`も利用できます。

```cs
// 先頭に待機時間を追加する
sequence.PrependInterval(1f);

// 先頭にコールバックを追加する
sequence.PrependCallback(() => Debug.Log("Hello!"));
```

### Join

前に追加したTweenと連結したい場合には`Join()`が利用できます。Joinで追加したTweenは直前にAppendで追加されたTweenと同時に再生されます。

```cs
sequence.Append(transform.TweenPosition(new Vector3(1f, 0f, 0f), 2f));

// 直前のTweenと連結する
sequence.Join(transform.TweenPosition(new Vector3(1f, 3f, 0f), 2f));
```

### Insert

任意の地点にTweenを挿入したい場合には`Insert()`を使用します。Insertで追加されたTweenは他のTweenとは独立して動作し、指定された位置に到達したら再生を開始します。

```cs
// 開始から1秒の地点にTweenを挿入する
sequence.Insert(1f, transform.TweenPosition(new Vector3(1f, 0f, 0f), 2f));
```

`InsertCallback()`を利用してコールバックを挿入することも可能です。

```cs
// 開始から1秒の地点にTweenを挿入する
sequence.InsertCallback(1f, () => Debug.Log("Hello!"));
```

### Tween型への変換

`Sequence`は`Tween`型に暗黙的に変換されるため、そのまま代入できます。

```cs
Sequence sequence = Sequence.Create();

// Tween型の変数にそのまま代入可能
Tween tween = sequence;
```

### 使用上の注意

* 再生中のTweenを追加することはできません。
* 無限ループするTweenを追加することはできません。
* 一度追加したTweenはロックされ、アクセスできなくなります。Sequence内のTweenを個別に操作することは出来ないので注意してください。
* 同じTweenを複数のSequenceに含めることはできません。

## コルーチンを用いたTweenの待機

コルーチンを用いることでTweenの待機を簡単に行うことができます。

Tweenを待機するにはWaitFor...メソッドを使用します。CompleteやPauseなど、指定されたタイミングまで待機させることが可能です。

```cs
IEnumerator ExampleCoroutine()
{
    // Tweenの完了まで待機する
    yield return Tween.Empty(3f).WaitForComplete();

    // 1回のループが終了するタイミングまで待機する
    yield return transform.TweenPosition(Vector3.one, 1f)
        .SetLoops(3)
        .WaitForStepComplete();
}
```

## ログ出力

Tweenのコールバックや値のデバッグを行いたい場合、専用の拡張メソッドを利用することで簡単にログ出力が可能です。(これらのログはMagicTweenSettingsのLoggingModeがFullの場合のみ表示されます。)

```cs
using MacigTween;
using MagicTween.Diagnostics; // デバッグ用の拡張メソッドを有効化する

// 特定のコールバックをログ出力
transform.TweenPosition(Vector3.up, 5f)
    .LogOnUpdate();

// 全てのコールバックをまとめてログ出力
transform.TweenEulerAngles(new Vector3(0f, 0f, 90f), 5f)
    .LogCallbacks();

// 判別用の名前をつけることも可能 
transform.TweenLocalScale(Vector3.one * 2f, 5f)
    LogCallbacks("Scale");

float foo;
// 値の出力も可能 (フレームごとに現在の値をログに表示する)
Tween.To(() => foo, x => foo = x, 5f, 10f)
    .LogValue();
```

## プロジェクト設定

Tweenの初期設定やログの設定などを変更することも可能です。

### MagicTweenSettingsの作成

`Assets > Create > Magic Tween > Magic Tween Settings`から設定を保存する用のMagicTweenSettingsを作成します。

> **Note**
> 作成したMagicTweenSettingsはプロジェクトのPreload Assetsに自動で登録されます。設定が読み込まれない場合は、MagicTweenSettingsがPreload Assetsに含まれているかを確認してください。

### Logging Mode

ログを有効化するかを設定します。

| LoggingMode | 動作 |
| - | - |
| LoggingMode.Full | Log系の拡張メソッドを含む全てのログをConsoleに表示します。 |
| LoggingMode.WarningsAndErrors | 警告とエラーのみをConsoleに表示します。 |
| LoggingMode.ErrorsOnly | エラーのみをConsoleに表示します。 |

### Capture Exceptions

Onの場合、Tweenの内部で発生した例外を警告としてログに表示します。(Offの場合は普通の例外として表示されます。)

### Default Tween Parameters

Tweenのデフォルトの設定値を変更できます。

### Scriptから設定を変更する

`MagicTweenSettings`クラスからこれらの設定にアクセスできます。

```cs
// Logging ModeをScriptから変更する
MagicTweenSettings.loggingMode = LoggingMode.ErrorsOnly;
```

## JobによるTransformのTweenの高速化

v0.2より、IJobParallelForTransformを用いてTransformのトゥイーンを高速化するオプションが追加されました。このオプションはデフォルトでは無効化されており、`Project Settings > Scripting Define Symbols`の項目に`MAGICTWEEN_ENABLE_TRANSFORM_JOBS`を追加することで有効化されます。

追加後は通常通り拡張メソッドでTransformを操作するだけで、IJobParallelForTransformによる高速化が適用されるようになります。

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween/Assets/MagicTween/Documentation~/benchmark_transform_tween_job.png" width="800">

パフォーマンスの比較はグラフの通りです。50,000個のTransformをトゥイーンさせた場合、およそ1.7倍近くの高速化が適用されます。

## TextMesh Pro

Magic TweenはTextMesh Proをサポートしています。TweenChar系の拡張メソッドを呼び出すことで、1文字ごとにテキストをTweenさせることが可能です。

```cs
TMP_Text tmp;

// GetCharCountでTween可能な文字の数を取得できる
for (int i = 0; i < tmp.GetCharCount(); i++)
{
    tmp.TweenCharScale(i, Vector3.zero).SetInvert().SetDelay(i * 0.07f);
}
```

`ResetCharTweens()`を呼び出すことでTMP_Textに紐付けられたTweenを停止させ、文字の装飾を初期状態に戻すことが可能です。

```cs
// 文字のTweenを停止し、初期状態に戻す
tmp.ResetCharTweens();
```

文字のTweenは内部で`TMPTweenAnimator`クラスを使用します。これは`GetTMPTweenAnimator()`から取得することが可能です。

```cs
// 内部のTMPTweenAnimatorを取得する
TMPTweenAnimator tmpAnimator = tmp.GetTMPTweenAnimator();

// TMP_Textに対する拡張メソッドはTMPTweenAnimatorのメソッドを内部で使用する
tmpAnimator.TweenCharOffset(0, Vector3.up);

// SetChar**で各文字のパラメータを直接設定が可能
tmpAnimator.SetCharScale(1, Vector3.one * 2f);
tmpAnimator.SetCharColor(1, Color.red);

// ResetCharTweens()と同じ
tmpAnimator.Reset();

// GetCharCountと同じ
tmpAnimator.GetCharCount();
```

## UniRx

UniRxを導入することで、TweenのコールバックやTween自体をObservableに変換することが可能になります。

### コールバックをObservableに変換

`OnUpdateAsObservable()`などのメソッドを用いて、TweenのコールバックをObservableに変換できます。

```cs
float foo;

Tween.To(() => foo, x => foo = x, 10f, 10f)
    .OnUpdateAsObservable()
    .Subscribe(_ =>
    {
        Debug.Log("update!");
    });
```

### TweenをObservableに変換

`ToObservable()`を使うことで、フレーム毎にOnNextで値を発行するObservableにTweenを変換できます。

```cs
Tween.FromTo(0f, 10f, 10f, null)
    .ToObservable()
    .Where(x => x >= 5f)
    .Subscribe(x =>
    {
        Debug.Log("current value: " + x);
    });
```

## UniTask

UniTaskを導入することで、Tweenの待機をasync/awaitで行うことが可能になります。

```cs
var tween = transform.TweenPosition(Vector3.up. 2f);

// Tweenを直接await可能 (TweenがKillされるまで待機する)
await tween;
```

`AwaitForKill()`を利用して`CancellationToken`を渡すことで、キャンセル処理を行うことができます。

```cs
// CancellationTokenSourceを作成
var cts = new CancellationTokenSource();

// CancellationTokenを渡してKillまで待機
await transform.TweenPosition(Vector3.up. 2f)
    .AwaitForKill(cancellationToken: cts.Token);
```

`AwaitForComplete()`や`AwaitForPause()`など、Kill以外のタイミングまで待機することも可能です。

```cs
// Completeのタイミングまで待機
await transform.TweenPosition(Vector3.up. 2f).AwaitForComplete();
```

また、`CancelBehaviour`を指定することでキャンセル時の挙動を設定することが可能です。

```cs
var cts = new CancellationTokenSource();

// キャンセル時にCompleteを呼び出し、OperationCanceledExceptionをthrowする
await transform.TweenPosition(Vector3.up. 2f)
    .AwaitForComplete(CancelBehaviour.CompleteAndCancelAwait, cts.Token);
```

| CancelBehaviour | キャンセル時の動作 |
| - | - |
| CancelBehaviour.Kill | Killを呼び出す。 |
| CancelBehaviour.Complete | Completeを呼び出す。 |
| CancelBehaviour.CompleteAndKill | CompleteAndKillを呼び出す。 |
| CancelBehaviour.CancelAwait | OperationCanceledExceptionをスローする。 |
| CancelBehaviour.KillAndCancelAwait | デフォルトの動作。Killを呼び出し、OperationCanceledExceptionをスローする。 |
| CancelBehaviour.CompleteAndCancelAwait | Completeを呼び出し、OperationCanceledExceptionをスローする。 |
| CancelBehaviour.CompleteAndKillAndCancelAwait | CompleteAndKillを呼び出し、OperationCanceledExceptionをスローする。 |

## 独自のTweenPluginを作成する

Magic Tweenはほとんどのプリミティブ型やUnity.Mathematicsの型のトゥイーンをサポートしており、基本的には拡張を作成する必要はありません。とはいえ、細かい動作を実現するために拡張を行いたい場面は存在するでしょう。

Magic Tweenでは型の拡張を行うためのAPIとして`ICustomTweenPlugin`と`ITweenOptions`の2つのインターフェースが用意されています。

### TweenPlugin

TweenPluginは特定の型の拡張をTweenに差し込むための機能です。これを実装することで独自の型をTweenに渡せるようになります。

以下は`double`のトゥイーンをサポートするTweenPluginの実装例です。

```cs
// TweenPluginAttributeを追加する必要がある
// これによってSourceGeneratorが型を認識し、必要なコードを生成する
[TweenPlugin]
// ICustomTweenPluginを実装した構造体を定義
// 型引数にはトゥイーンさせる値の型と、対応するTweenOptionsの型(必要ない場合はNoOptions)を指定
public readonly struct DoubleTweenPlugin : ICustomTweenPlugin<double, NoOptions>
{
    // Evaluate関数内に計算処理を記述
    public double Evaluate(in double startValue, in double endValue, in NoOptions options, in TweenEvaluationContext context)
    {
        // SetRelative(true)が設定されている場合、終了値を相対値に設定する
        var resolvedEndValue = context.IsRelative ? startValue + endValue : endValue;

        // SetInvert(true)が設定されている場合、開始値と終了値を入れ替える
        // そして、context.Progress(0〜1)から現在値を計算して返す
        if (context.IsInverted) return math.lerp(resolvedEndValue, startValue, context.Progress);
        else return math.lerp(startValue, resolvedEndValue, context.Progress);
    }
}
```

TweenPluginは状態を持つことができません。追加の設定を保持したい場合には独自のTweenOptionsを実装します。

### TweenOptions

Tweenに独自の設定を追加したい場合には`ITweenOptions`を実装した構造体を定義します。

以下は整数型のTween用に作成されたTweenOptionsの実装例です。

```cs
// ITweenOptionsを実装した構造体を定義
public struct IntegerTweenOptions : ITweenOptions
{
    public RoundingMode roundingMode;
}

public enum RoundingMode : byte
{
    ToEven,
    AwayFromZero,
    ToZero,
    ToPositiveInfinity,
    ToNegativeInfinity
}
```

作成したTweenOptionsはTweenPluginの型引数に設定することが可能です。

### 作成したTweenPluginを使用する

独自のTweenPluginをTweenに使用するには、`Tween.To()`または`Tween.FromTo()`を使用します。

```cs
double currentValue = 0.0;

// TweenOptionsとTweenPluginsを指定してTweenを作成する
Tween.FromTo<double, NoOptions, DoubleTweenPlugin>(x => currentValue = x, startValue, endValue, duration);
```

独自のTweenOptionsを指定した場合、`SetOptions()`を使用することでTweenOptionsの値を変更できるようになります。また、`GetOptions()`で設定されているTweenOptionsの値を取得できます。

```cs
public struct CustomOptions : ITweenOptions
{
    ...
}

// SetOptionsを用いてTween独自のオプションを変更する
tween.SetOptions(new CustomOptions() { ... });

// GetOptionsを用いてオプションの値を取得する
var options = tween.GetOptions();
```

### 組み込みのTweenPlugin/TweenOptions

Magic Tweenではデフォルトで多くのTweenPlugin/TweenOptionsを用意しています。

以下に利用可能なTweenPlugin/TweenOptionsの一覧を表示します。(これ以外にもいくつかのTweenPlugin/TweenOptionsが用意されていますが、特殊なTween用のものであるため外部での使用しないでください。)

|  | TweenPlugin | 対応するTweenOptions |
| - | - | - |
| float | FloatTweenPlugin | NoOptions |
| float2 | Float2TweenPlugin | NoOptions |
| float3 | Float3TweenPlugin | NoOptions |
| float4 | Float4TweenPlugin | NoOptions |
| double | DoubleTweenPlugin | NoOptions |
| double2 | Double2TweenPlugin | NoOptions |
| double3 | Double3TweenPlugin | NoOptions |
| double4 | Double4TweenPlugin | NoOptions |
| int | IntTweenPlugin | IntegerTweenOptions |
| int2 | Int2TweenPlugin | IntegerTweenOptions |
| int3 | Int3TweenPlugin | IntegerTweenOptions |
| int4 | Int4TweenPlugin | IntegerTweenOptions |
| long | LongTweenPlugin | IntegerTweenOptions |
| quaternion | QuaternionTweenPlugin | NoOptions |

## ECS向けの実装

Magic Tweenでは、ECS向けにTweenを実装するためのAPIが用意されています。これらのAPIを利用することで、通常よりもさらにハイパフォーマンスなTweenを作成することが可能です。

### Translatorの作成

特定のComponentの値をトゥイーンする場合、対象の値にTweenの現在値を反映するためのComponentである`Translator`と、それを動作させるためのSystemを事前に作成しておく必要があります。

例として、以下のComponentをトゥイーンするためのTranslatorを作成します。

```cs
public struct ExampleComponent : IComponentData
{
    public float value;
}
```

まずは、`ITweenTranslator`を実装した構造体を定義します。
Translatorは状態を持たないようにし、必要な処理だけを記述するようにしてください。

```cs
public struct ExampleTranslator : ITweenTranslator<float, ExampleComponent>
{
    // Componentに値を適用する
    public void Apply(ref ExampleComponent component, in float value)
    {
        component.value = value;
    }

    // 現在のComponentの値を返す
    public float GetValue(ref ExampleComponent component)
    {
        return component.value;
    }
}
```

次に、`TweenTranslationSystemBase`を継承したSystemクラスを作成します。型引数には先ほど作成したTranslatorや使用するTweenPluginなどを指定します。指定するTweenPluginについては「組み込みのTweenPlugin/TweenOptions」の表を参照してください。独自のTweenPluginを指定することも可能です。

また、処理自体は基底クラス側で実装されているため、派生クラスの内部には何も記述しないようにしてください。

```cs
public partial class ExampleTweenTranslationSystem : TweenTranslationSystemBase<float, NoOptions, FloatTweenPlugins, ExampleComponent, ExampleTranslator> { }
```

これで値をトゥイーンする準備は完了です。

### Componentの値をトゥイーンする

作成したTranslatorを用いて値をトゥイーンする場合には、`Tween.Entity.To()`や`Tween.Entity.FromTo()`を使用します。

型引数には対象のComponentの型と、利用するTranslatorの型を渡します。

```cs
var entity = EntityManager.CreateEntity();
EntityManager.AddComponent<ExampleComponent>(entity);

// ExampleComponentのvalueを5まで10秒かけてトゥイーンする
Tween.Entity.To<ExampleComponent, ExampleTranslator>(entity, 5f, 10f);
```

これらの値には通常のTween同様、メソッドチェーンを用いて設定を追加できます。

```cs
Tween.Entity.FromTo<ExampleComponent, ExampleTranslator>(entity, 0f, 5f, 10f)
    .SetEase(Ease.OutSine)
    .SetLoops(3, LoopType.Restart)
    .SetDelay(1f);
```

また、SequenceにこれらのTweenを追加することも可能です。

```cs
var entity1 = EntityManager.CreateEntity();
var entity2 = EntityManager.CreateEntity();
EntityManager.AddComponent<ExampleComponent>(entity1);
EntityManager.AddComponent<ExampleComponent>(entity2);

var tween1 = Tween.Entity.To<ExampleComponent, ExampleTranslator>(entity1, 5f, 10f);
var tween2 = Tween.Entity.To<ExampleComponent, ExampleTranslator>(entity2, 5f, 10f);

var sequence = Sequence.Create()
    .Append(tween1)
    .Append(tween2);
```

> **Warning**
> 同じEntityに対して、同一のTranslatorを用いたTweenを複数同時に再生しないでください。値の反映が重複し思わぬ動作を引き起こす可能性があります。

### 組み込みのTranslator
`MagicTween.Translators`以下にはECSのコンポーネント群をトゥイーンさせるための組み込みのTranslatorが含まれます。

現在は`LocalTransform`に対応したTranslatorが提供されています。

またEntities Graphicsパッケージを導入している場合には、マテリアルのプロパティに対応したコンポーネントをトゥイーンさせるためのTranslatorが用意されています。

### 制約

Tween/Sequenceの作成・操作はメインスレッド上でのみサポートされています。Job内から新たにTweenを作成したり、KillやComplete等の操作を行うことはできません。
この制約を緩和するため、専用のCommandBufferを用いてTweenの作成/操作を行うための機能を現在開発中です。

## その他の機能

### EaseUtility

Tweenの内部で用いられているイージング関数はEaseUtilityから利用することができます。

```cs
float value1 = EaseUtility.Evaluate(0.5f, Ease.OutQuad);
float value2 = EaseUtility.InOutQuad(0.5f);
```

## 最適化

### Tweenのキャッシュ

通常TweenやSequenceの作成コストはほとんど問題になりませんが、繰り返し同じアニメーションを利用する場面では、再生のたびに毎回作成を行うのはあまり効率的ではありません。このような場面ではTweenをキャッシュして使い回すことが有効な手段になります。

```cs
// トゥイーンを作成し、手動で再生/破棄を行うように設定を変更する
Tween tween = transform.TweenPosition(Vector3.up. 2f)
    .SetAutoPlay(false)
    .SetAutoKill(false);

// PlayやRestartを用いてトゥイーンを再生する
tween.Play();
tween.Restart();

// 使い終わったら手動でKillを呼び出す
tween.Kill();

// または、SetLinkを用いてライフタイムをGameObjectに紐付けることも可能
// tween.SetLink(transform);
```

Tweenを使い回す場合には必ず`SetAutoKill(false)`を設定します。これがtrueの場合、再生が完了したTweenは自動で破棄されます。
また、再生タイミングを手動で管理したいには合わせて`SetAutoPlay(false)`を設定します。

`SetAutoKill(false)`を設定した場合、必ず使い終わったタイミングで`Kill()`を呼び出してTweenを破棄してください。または、`SetLink()`を用いてGameObject破棄されたタイミングでTweenを破棄するように設定することも可能です。

## 実験的機能

`MagicTween.Experimental`以下には現在開発中の機能が含まれています。このnamespace内の機能は利用可能ですが、動作の保証はなく、予告なく破壊的変更が加えられる可能性があります。

## 既知の問題点

### エディタ上でパフォーマンスが低下する

ECSは安全性を高めるために多くのチェックを行うため、エディタ上ではパフォーマンスが低下します。このパフォーマンス低下はTweenの作成時などで顕著にみられ、状況によっては通常の何倍もの処理時間がかかることもあります。

これらの安全性チェックは実機では無効化されるため、パフォーマンスの測定は必ず実機上で行なってください。

### WebGLでパフォーマンスが低下する

ECSをWebGL上で動作させること自体は可能ですが、WebGLの仕様上マルチスレッドやSIMD演算が使えないため、JobやBurstなどの最適化はすべて無効化されます。ECSの高いパフォーマンスはJob SystemとBurstによって実現されているためパフォーマンスの低下は免れません。(そのため、現状WebGL上でECSを扱う利点はあまりありません。)

上記の理由により、Magic TweenはWebGL上でパフォーマンスが低下します。
通常これらの影響が表面化することはありませんが、大量にTweenを作成する際にはこの点に留意してください。

## サポート

Forum: https://forum.unity.com/threads/magic-tween-extremely-fast-tween-library-implemented-in-ecs.1490080/

## ライセンス

[MIT License](LICENSE)