using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MagicTween.Tests
{
    public sealed class TweenSettingsTest
    {
        [UnityTest]
        public IEnumerator Test_Delay()
        {
            var foo = 0f;
            var tween = Tween.To(() => foo, x => foo = x, 10f, 1f)
                .SetDelay(2f);

            yield return new WaitForSeconds(2f);
            Assert.AreEqual(foo, 0f, 0.01f);
            yield return tween.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_AutoKill()
        {
            var foo = 0f;
            var tween = Tween.To(() => foo, x => foo = x, 10f, 1f)
                .SetAutoKill(false);

            yield return tween.WaitForComplete();

            Assert.IsTrue(tween.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_AutoPlay()
        {
            var foo = 0f;
            var tween = Tween.To(() => foo, x => foo = x, 10f, 1f)
                .SetAutoPlay(false);

            yield return new WaitForSeconds(0.5f);

            Assert.IsFalse(tween.IsPlaying());
            tween.Play();

            yield return tween.WaitForComplete();
        }
    }
}
