using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using AnimeRx;
using UniRx;

public static class AnimeRxTester
{
    static CompositeDisposable disposables;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Init()
    {
        disposables = new();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CleanUp()
    {
        disposables?.Dispose();
        disposables = null;
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateFloatTweens(TestClass[] array, float duration)
    {
        for (int i = 0; i < array.Length; i++)
        {
            var index = i;
            Anime.Play(i, i + 10f, Easing.Linear(duration))
                .Subscribe(x => array[index].value = x)
                .AddTo(disposables);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreatePositionTweens(Transform[] transforms, float duration)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            var index = i;
            Anime.Play(Vector3.zero, Vector3.one * i, Easing.Linear(duration))
                .Subscribe(x => transforms[index].position = x)
                .AddTo(disposables);
        }
    }
}

