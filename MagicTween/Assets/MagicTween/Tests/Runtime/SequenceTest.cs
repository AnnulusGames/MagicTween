using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace MagicTween.Tests
{
    public sealed class SequenceTest
    {
        Transform transform;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestHelper.Setup3DScene();
            transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            MagicTweenSettings.captureExceptions = false;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            UnityEngine.Object.Destroy(transform.gameObject);
        }

        [SetUp]
        public void Setup()
        {
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
        }

        [UnityTest]
        public IEnumerator Test_Append()
        {
            var sequence = Sequence.Create();
            sequence.Append(transform.TweenLocalScale(new Vector3(5f, 5f, 5f), 0.5f));
            sequence.Append(
                transform.TweenLocalScale(new Vector3(0, 0, 0), 0.1f)
                    .OnStart(() => AssertAreEqual(transform.localScale, new Vector3(5f, 5f, 5f)))
            );
            yield return sequence.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_Prepend()
        {
            var sequence = Sequence.Create();
            sequence.Append(
                transform.TweenLocalScale(new Vector3(0, 0, 0), 0.1f)
                    .OnStart(() => AssertAreEqual(transform.localScale, new Vector3(1.2f, 1.2f, 1.2f)))
            );
            sequence.Prepend(
                transform.TweenLocalScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
            );
            yield return sequence.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_Insert()
        {
            var sequence = Sequence.Create();
            sequence.Append(transform.TweenLocalScale(new Vector3(2f, 2f, 2f), 0.5f));
            sequence.Insert(
                0.25f, transform.TweenPositionY(3f, 0.25f)
            );
            yield return sequence.WaitForComplete();
        }

        [UnityTest]
        public IEnumerator Test_Append_Callbacks()
        {
            var sequence = Sequence.Create();
            var (flag1, flag2) = (false, false);

            sequence.AppendCallback(() => flag1 = true);
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => flag2 = true);

            yield return sequence.WaitForComplete();

            Assert.IsTrue(flag1, "flag1 is false");
            Assert.IsTrue(flag2, "flag2 is false");
        }

        static void AssertAreEqual(Vector3 a, Vector3 b)
        {
            Assert.That(a, Is.EqualTo(b).Using(new Vector3EqualityComparer(0.01f)));
        }
    }
}
