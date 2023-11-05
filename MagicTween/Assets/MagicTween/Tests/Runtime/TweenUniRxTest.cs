#if MAGICTWEEN_SUPPORT_UNIRX
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UniRx;

namespace MagicTween.Tests
{
    public sealed class TweenUniRxTest
    {
        [UnityTest]
        public IEnumerator Test_OnStartAsObservable()
        {
            var flag = false;
            var tween = Tween.Empty(1f).SetAutoPlay(false);
            tween.OnStartAsObservable().Subscribe(_ =>
            {
                flag = true;
            });

            Assert.IsFalse(flag);
            tween.Play();
            Assert.IsTrue(flag);

            yield return tween.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_OnCompleteAsObservable()
        {
            var flag = false;
            var tween = Tween.Empty(1f);
            tween.OnCompleteAsObservable().Subscribe(_ =>
            {
                flag = true;
            });
            yield return tween.WaitForComplete();
            Assert.IsTrue(flag);
        }

        [UnityTest]
        public IEnumerator Test_OnKillAsObservable()
        {
            var flag = false;
            var tween = Tween.Empty(1f);
            tween.OnKillAsObservable().Subscribe(_ =>
            {
                flag = true;
            });
            yield return tween.WaitForKill();
            Assert.IsTrue(flag);
        }

        [UnityTest]
        public IEnumerator Test_ToObservable()
        {
            var foo = 0f;
            var observable = Tween.FromTo(null, 0f, 10f, 2f)
                .ToObservable();

            observable.Subscribe(x => foo = x);

            yield return observable.ToYieldInstruction();

            Assert.AreEqual(foo, 10f);
        }
    }
}
#endif