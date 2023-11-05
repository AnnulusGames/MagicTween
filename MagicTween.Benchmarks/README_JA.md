# MagicTween.Benchmarks

[English README is here](https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/README.md)

このプロジェクトには、GithubやAsset Storeなどで公開されているトゥイーンライブラリの性能を[Performance Testing API](https://docs.unity3d.com/Packages/com.unity.test-framework.performance@3.0/manual/index.html)を用いて計測した結果と使用したソースコードが含まれています。

> **Note**
> 再配布が許可されていないライブラリを含むため、このプロジェクトには実際のライブラリのソースコードは含まれていません。自身でテストを行いたい際にはテストに必要なライブラリを手動で追加してください。

## テスト環境
ベンチマークはmacOS向けに実機ビルドした上で行われています。
マシンやバージョンに関する詳細については以下の通りです。

<b>MacBook Pro</b>\
<b>OS:</b> macOS Ventura 13.0\
<b>CPU:</b> Apple M2\
<b>Memory:</b> 24GB

<b>Unity:</b> 2022.3.1f1\
<b>Scripting Backend:</b> IL2CPP

## 使用したライブラリ

* MagicTween (0.2.0)
* [DOTween Pro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416) (1.0.375)
* [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595) (2.5.1)
* [PrimeTween](https://assetstore.unity.com/packages/tools/animation/primetween-high-performance-animations-and-sequences-252960) (1.0.15)
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
| AnimeRx | 7.88ms |
| AnimeTask | 7.42ms |
| DOTween | 1.97ms |
| UnityTweens | 1.9ms |
| LeanTween | 1.86ms |
| GoKit | 1.45ms |
| ZestKit | 1.29ms |
| PrimeTween | 0.97ms |
| MagicTween | 0.5ms |
| MagicTween (for ECS) | 0.3ms |

### Tween 64,000 floats

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_64000_floats.png" width="800">

|  | Time |
| - | - |
| AnimeRx | 18.9ms |
| AnimeTask | 13ms |
| DOTween | 5.67ms |
| LeanTween | 4.45ms |
| GoKit | 4.03ms |
| UnityTweens | 3.98ms |
| ZestKit | 3.45ms |
| PrimeTween | 2.14ms |
| MagicTween | 1ms |
| MagicTween (for ECS) | 0.5ms |

### Tween 25,000 Transform.position

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_25000_transform_position.png" width="800">

| Tween | Average |
| - | - |
| AnimeTask | 9.16ms |
| AnimeRx | 6.29ms |
| GoKit | 3.55ms |
| LeanTween | 2.97ms |
| DOTween | 2.85ms |
| UnityTweens | 2.79ms |
| PrimeTween | 2.52ms |
| ZestKit | 1.88ms |
| MagicTween | 1.7ms |
| MagicTween (Job) | 1.4ms |

### Tween 50,000 Transform.position

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_50000_transform_position.png" width="800">

| Tween | Average |
| - | - |
| AnimeTask | 19.11ms |
| AnimeRx | 17.64ms |
| GoKit | 11.21ms |
| LeanTween | 8.5ms |
| DOTween | 7.89ms |
| UnityTweens | 7.28ms |
| PrimeTween | 7.28ms |
| ZestKit | 6.55ms |
| MagicTween | 5.31ms |
| MagicTween (Job) | 3.3ms |

### Tween 25,000 Trasnform.rotation

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_25000_transform_rotation.png" width="800">

|  | Time |
| - | - |
| AnimeTask | 8.59ms |
| AnimeRx | 6.84ms |
| GoKit | 3.67ms |
| LeanTween | 3.06ms |
| DOTween | 2.99ms |
| UnityTweens | 2.73ms |
| PrimeTween | 2.61ms |
| ZestKit | 1.93ms |
| MagicTween | 1.69ms |
| MagicTween (Job) | 1.5ms |

### Tween 50,000 Trasnform.rotation

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_50000_transform_rotation.png" width="800">

|  | Time |
| - | - |
| AnimeTask | 19.11ms |
| AnimeRx | 17.64ms |
| GoKit | 11.21ms |
| LeanTween | 8.5ms |
| DOTween | 7.89ms |
| UnityTweens | 7.28ms |
| PrimeTween | 7.28ms |
| ZestKit | 6.55ms |
| MagicTween | 5.31ms |
| MagicTween (Job) | 3.3ms |

### Startup (64,000 float tweens)

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_startup_64000_floats.png" width="800">

|  | Time |
| - | - |
| GoKit | 3,341.76ms |
| ZestKit | 416.03ms |
| AnimeRx | 300.48ms |
| AnimeTask | 90.57ms |
| DOTween | 83.46ms |
| UnityTweens | 75.21ms |
| LeanTween | 51.45ms |
| MagicTween | 31.14ms |
| MagicTween (for ECS) | 16.7ms |
| PrimeTween | 4.38ms |

### Startup (50,000 Transform.position tweens)

<img src="https://github.com/AnnulusGames/MagicTween/blob/main/MagicTween.Benchmarks/Assets/Documentation~/benchmark_startup_50000_transform_position.png" width="800">

|  | Time |
| - | - |
| GoKit | 1,721ms |
| AnimeRx | 241ms |
| AnimeTask | 206ms |
| UnityTweens | 45.68ms |
| MagicTween (Job) | 45.22ms |
| DOTween | 42.95ms |
| LeanTween | 36.39ms |
| MagicTween | 25.91ms |
| ZestKit | 19.08ms |
| PrimeTween | 7.53ms |

> **Note**
> Magic TweenはTweenをキャッシュすることでアニメーションの開始を高速化できます。詳細はREADMEの[最適化の項目](https://github.com/AnnulusGames/MagicTween/blob/main/README_JP.md#最適化)を参照してください。