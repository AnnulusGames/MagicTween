using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MagicTween.Tests
{
    public sealed class TweenCallbackTest
    {
        bool flag;

        [SetUp]
        public void Setup()
        {
            flag = false;
        }

        [UnityTest]
        public IEnumerator Test_OnStart()
        {
            var tween = Tween.Empty(1f).OnStart(() => flag = true);
            yield return null;
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnStart_NoAlloc()
        {
            var tween = Tween.Empty(1f).OnStart(this, obj => obj.flag = true);
            yield return null;
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnPlay()
        {
            var tween = Tween.Empty(1f).OnPlay(() => flag = true);
            yield return null;
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnPlay_NoAlloc()
        {
            var tween = Tween.Empty(1f).OnPlay(this, obj => obj.flag = true);
            yield return null;
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnPause()
        {
            var tween = Tween.Empty(999f).OnPause(() => flag = true);
            Tween.DelayedCall(0.5f, () => tween.Pause());
            yield return new WaitForSeconds(0.51f);
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnPause_NoAlloc()
        {
            var tween = Tween.Empty(999f).OnPause(this, obj => obj.flag = true);
            Tween.DelayedCall(0.5f, () => tween.Pause());
            yield return new WaitForSeconds(0.51f);
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnStepComplete()
        {
            var tween = Tween.Empty(1f).SetLoops(-1).OnStepComplete(() => flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnStepComplete_NoAlloc()
        {
            var tween = Tween.Empty(1f).SetLoops(-1).OnStepComplete(this, obj => obj.flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
            tween.Kill();
        }

        [UnityTest]
        public IEnumerator Test_OnComplete()
        {
            var tween = Tween.Empty(1f).OnComplete(() => flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
        }

        [UnityTest]
        public IEnumerator Test_OnComplete_NoAlloc()
        {
            var tween = Tween.Empty(1f).OnComplete(this, obj => obj.flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
        }

        [UnityTest]
        public IEnumerator Test_OnKill()
        {
            var tween = Tween.Empty(1f).OnKill(() => flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
        }

        [UnityTest]
        public IEnumerator Test_OnKill_NoAlloc()
        {
            var tween = Tween.Empty(1f).OnKill(this, obj => obj.flag = true);
            yield return new WaitForSeconds(1.1f);
            Assert.IsTrue(flag);
        }
    }
}
