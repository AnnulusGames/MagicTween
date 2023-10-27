using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using AnimeTask;

namespace MagicTween.Benchmark
{
    public static class AnimeTaskHelper
    {
        static CancellationTokenSource cts;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init()
        {
            cts?.Cancel();
            cts = new();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CleanUp()
        {
            cts?.Cancel();
            cts = null;
            GC.Collect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTween(TestClass instance, float duration)
        {
            Easing.Create<Linear>(0f, 10f, duration).ToAction(x => instance.value = x, cancellationToken: cts.Token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFloatTweens(TestClass[] array, float duration)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var index = i;
                Easing.Create<Linear>(i, i + 10f, duration).ToAction(x => array[index].value = x, cancellationToken: cts.Token);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePositionTweens(Transform[] transforms, float duration)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                Easing.Create<Linear>(Vector3.one * i, duration)
                    .ToGlobalPosition(transforms[i], cancellationToken: cts.Token);
            }
        }
    }
}