# MagicTween.Benchmarks

[English README is here](https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/README.md)

このプロジェクトには、GithubやAsset Storeなどで公開されているトゥイーンライブラリの性能を[Performance Testing API](https://docs.unity3d.com/Packages/com.unity.test-framework.performance@3.0/manual/index.html)を用いて計測した結果と使用したソースコードが含まれています。

> **Note**
> 再配布が許可されていないライブラリを含むため、このプロジェクトには実際のライブラリのソースコードは含まれていません。自身でテストを行いたい際にはテストに必要なライブラリを手動で追加してください。

## テスト環境
ベンチマークはmacOS向けに実機ビルドした上で行われています。
また、マシンやバージョンに関する詳細については以下の通りです。

<b>MacBook Pro</b>\
<b>OS:</b> macOS Ventura 13.0\
<b>CPU:</b> Apple M2\
<b>Memory:</b> 24GB

<b>Unity:</b> 2022.3.1f1\
<b>Scripting Backend:</b> IL2CPP

## 使用したライブラリ

* MagicTween (0.1.0)
* [DOTween Pro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416) (1.0.375)
* [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595) (2.5.1)
* [PrimeTween](https://assetstore.unity.com/packages/tools/animation/primetween-high-performance-animations-and-sequences-252960) (1.0.4)
* [GoKit](https://github.com/prime31/GoKit)
* [ZestKit](https://github.com/prime31/ZestKit)
* [AnimeRx](https://github.com/kyubuns/AnimeRx) (1.3.2)
* [AnimeTask](https://github.com/kyubuns/AnimeTask) (1.13.1)
* [unity-tweens](https://github.com/jeffreylanters/unity-tweens) (3.2.0)

## 結果

Timeは各フレームの処理時間の中央値を表します。

StartUpについては、トゥイーンを作成するのにかかる時間のみを計測したものになります。

### Tween 32,000 floats

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_32000_floats.png" width="800">

|  | Time |
| - | - |
| AnimeTask | 8.73ms |
| AnimeRx | 5.75ms |
| DOTween | 2.18ms |
| LeanTween | 1.96ms |
| UnityTweens | 1.9ms |
| GoKit | 1.46ms |
| ZestKit | 1.29ms |
| PrimeTween | 1.08ms |
| MagicTween | 0.5ms |
| MagicTween (for ECS) | 0.3ms |

### Tween 64,000 floats

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_64000_floats.png" width="800">

|  | Time |
| - | - |
| AnimeRx | 20.18ms |
| AnimeTask | 14.6ms |
| DOTween | 5.78ms |
| GoKit | 4.32ms |
| UnityTweens | 4.2ms |
| LeanTween | 4.11ms |
| ZestKit | 3.36ms |
| PrimeTween | 2.21ms |
| MagicTween | 1ms |
| MagicTween (for ECS) | 0.5ms |

### Tween 25,000 Transform.position

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_25000_transform" width="800">

|  | Time |
| - | - |
| AnimeTask | 8.63ms |
| AnimeRx | 6.38ms |
| GoKit | 3.21ms |
| LeanTween | 3.02ms |
| DOTween | 3.09ms |
| UnityTweens | 2.8ms |
| PrimeTween | 2.7ms |
| ZestKit | 1.85ms |
| MagicTween | 1.79ms |

### Tween 50,000 Transform.position

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/startup.png" width="800">

|  | Time |
| - | - |
| AnimeTask | 19.61ms |
| AnimeRx | 17.21ms |
| GoKit | 9.9ms |
| LeanTween | 7.45ms |
| DOTween | 7.55ms |
| UnityTweens | 6.81ms |
| PrimeTween | 6.5ms |
| ZestKit | 6.54ms |
| MagicTween | 6.48ms |

### Startup (64,000 float tweens)

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/banchmark_50000_transform.png" width="800">

|  | Time |
| - | - |
| GoKit | 3,394.66ms |
| ZestKit | 413.5ms |
| AnimeRx | 296.22ms |
| DOTween | 82.16ms |
| AnimeTask | 81.45ms |
| UnityTweens | 77.46ms |
| MagicTween (for ECS) | 70.81ms |
| MagicTween | 47.78ms |
| LeanTween | 45.4ms |
| PrimeTween | 34.02ms |