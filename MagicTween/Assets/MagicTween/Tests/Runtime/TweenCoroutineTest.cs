using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MagicTween.Tests
{
    public class TweenCoroutineTest
    {
        [UnityTest]
        public IEnumerator Test_WaitForComplete()
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            Tween.DelayedCall(2f, () => tween.Complete());

            yield return tween.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_WaitForStepComplete()
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 2f).SetLoops(-1);
            yield return tween.WaitForStepComplete();
        }

        [UnityTest]
        public IEnumerator Test_WaitForKill()
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            Tween.DelayedCall(2f, () => tween.Kill());

            yield return tween.WaitForKill();
        }

        [UnityTest]
        public IEnumerator Test_WaitForPause()
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f);
            Tween.DelayedCall(2f, () => tween.Pause());

            yield return tween.WaitForPause();
        }

        [UnityTest]
        public IEnumerator Test_WaitForPlay()
        {
            var foo = 0f;
            var tween = Tween.FromTo(x => foo = x, 0f, 10f, 999f).SetAutoPlay(false);
            Tween.DelayedCall(2f, () => tween.Play());

            yield return tween.WaitForPlay();
        }
    }
}
