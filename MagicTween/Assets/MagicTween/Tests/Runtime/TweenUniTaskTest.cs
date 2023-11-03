using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace MagicTween.Tests
{
    public sealed class TweenUniTaskTest
    {
        CancellationTokenSource cts;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            cts = new();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            cts.Cancel();
        }

        [UnityTest]
        public IEnumerator Test_Await() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            await Tween.FromTo(x => foo = x, 0f, 10f, 2f);
        });

        [UnityTest]
        public IEnumerator Test_AwaitForPlay() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f).SetAutoPlay(false);
            InvokeAfter(() => tween.Play(), 2f).Forget();
            await tween.AwaitForPlay(cancellationToken: cts.Token);
        });

        [UnityTest]
        public IEnumerator Test_AwaitForPause() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            InvokeAfter(() => tween.Pause(), 2f).Forget();
            await tween.AwaitForPause(cancellationToken: cts.Token);
        });

        [UnityTest]
        public IEnumerator Test_AwaitForStepComplete() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 2f).SetLoops(-1);
            await tween.AwaitForStepComplete(cancellationToken: cts.Token);
        });

        [UnityTest]
        public IEnumerator Test_AwaitForComplete() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            InvokeAfter(() => tween.Complete(), 2f).Forget();
            await tween.AwaitForComplete(cancellationToken: cts.Token);
        });

        [UnityTest]
        public IEnumerator Test_AwaitForKill() => UniTask.ToCoroutine(async () =>
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            InvokeAfter(() => tween.Kill(), 2f).Forget();
            await tween.AwaitForKill(cancellationToken: cts.Token);
        });

        [UnityTest]
        public IEnumerator Test_CancelAndKill() => UniTask.ToCoroutine(async () =>
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            cancellationTokenSource.CancelAfter(1000);

            try
            {
                await tween.AwaitForKill(CancelBehaviour.KillAndCancelAwait, cancellationToken: cancellationTokenSource.Token);
                Assert.Fail();
            }
            catch (OperationCanceledException)
            {
                Assert.IsFalse(tween.IsActive());
            }
        });

        async UniTaskVoid InvokeAfter(Action action, float delay)
        {
            await UniTask.Delay((int)(delay * 1000), cancellationToken: cts.Token);
            action();
        }
    }
}
