using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MagicTween.Tests
{
    public sealed class TweenPropertyTest
    {
        const float Duration = 3f;
        const float InitialValue = 0f;
        const float EndValue = 10f;
        const float Strength = 5f;
        float foo = 0f;

        [SetUp]
        public void Setup()
        {
            foo = InitialValue;
        }

        [UnityTest]
        public IEnumerator Test_To()
        {
            Tween.To(() => foo, x => foo = x, EndValue, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, EndValue);
        }

        [UnityTest]
        public IEnumerator Test_To_NoAlloc()
        {
            Tween.To(this, (obj) => obj.foo, (obj, x) => obj.foo = x, EndValue, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, EndValue);
        }

        [UnityTest]
        public IEnumerator Test_FromTo()
        {
            Tween.FromTo(x => foo = x, -10f, EndValue, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, EndValue);
        }

        [UnityTest]
        public IEnumerator Test_FromTo_NoAlloc()
        {
            Tween.FromTo(this, (obj, x) => obj.foo = x, -10f, EndValue, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, EndValue);
        }

        [UnityTest]
        public IEnumerator Test_Punch()
        {
            Tween.Punch(() => foo, x => foo = x, Strength, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, InitialValue);
        }

        [UnityTest]
        public IEnumerator Test_Punch_NoAlloc()
        {
            Tween.Punch(this, (obj) => obj.foo, (obj, x) => obj.foo = x, Strength, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, InitialValue);
        }

        [UnityTest]
        public IEnumerator Test_Shake()
        {
            Tween.Shake(() => foo, x => foo = x, Strength, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, InitialValue);
        }

        [UnityTest]
        public IEnumerator Test_Shake_NoAlloc()
        {
            Tween.Shake(this, (obj) => obj.foo, (obj, x) => obj.foo = x, Strength, Duration);
            yield return WaitForComplete();
            Assert.AreEqual(foo, InitialValue);
        }

        IEnumerator WaitForComplete()
        {
            var counter = 0f;
            while (counter < Duration)
            {
                Debug.Log("Foo: " + foo);
                counter += Time.deltaTime;
                yield return null;
            }
        }
    }
}
