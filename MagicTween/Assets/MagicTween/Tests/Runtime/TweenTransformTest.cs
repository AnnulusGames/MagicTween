using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace MagicTween.Tests
{
    public sealed class TweenTransformTest
    {
        Transform transform;
        const float Duration = 3f;
        readonly WaitForSeconds waitForSeconds = new(Duration + 0.001f);

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestHelper.Setup3DScene();
            transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
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
        public IEnumerator Test_Position_To()
        {
            var endValue = Vector3.one * 2f;
            transform.TweenPosition(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_FromTo()
        {
            var startValue = Vector3.one * -2f;
            var endValue = Vector3.one * 2f;
            transform.TweenPosition(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_Punch()
        {
            var startValue = transform.position;
            var strength = Vector3.one * 2f;
            transform.PunchPosition(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_Shake()
        {
            var startValue = transform.position;
            var strength = Vector3.one * 2f;
            transform.ShakePosition(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_To()
        {
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.TweenPositionX(endValueX, Duration);
            transform.TweenPositionY(endValueY, Duration);
            transform.TweenPositionZ(endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position.x, endValueX);
            AssertAreEqual(transform.position.y, endValueY);
            AssertAreEqual(transform.position.z, endValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_FromTo()
        {
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.TweenPositionX(-1f, endValueX, Duration);
            transform.TweenPositionY(-1f, endValueY, Duration);
            transform.TweenPositionZ(-1f, endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position.x, endValueX);
            AssertAreEqual(transform.position.y, endValueY);
            AssertAreEqual(transform.position.z, endValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_Punch()
        {
            var (strengthX, strengthY, strengthZ) = (1f, 2f, 3f);
            var (startValueX, startValueY, startValueZ) = (transform.position.x, transform.position.y, transform.position.z);
            transform.PunchPositionX(strengthX, Duration);
            transform.PunchPositionY(strengthY, Duration);
            transform.PunchPositionZ(strengthZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position.x, startValueX);
            AssertAreEqual(transform.position.y, startValueY);
            AssertAreEqual(transform.position.z, startValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_Shake()
        {
            var (strengthX, strengthY, strengthZ) = (1f, 2f, 3f);
            var (startValueX, startValueY, startValueZ) = (transform.position.x, transform.position.y, transform.position.z);
            transform.ShakePositionX(strengthX, Duration);
            transform.ShakePositionY(strengthY, Duration);
            transform.ShakePositionZ(strengthZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position.x, startValueX);
            AssertAreEqual(transform.position.y, startValueY);
            AssertAreEqual(transform.position.z, startValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Rotation_To()
        {
            var endValue = Quaternion.Euler(45f, 45f, 45f);
            transform.TweenRotation(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.rotation, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Rotation_FromTo()
        {
            var startValue = Quaternion.Euler(-45f, -45f, -45f);
            var endValue = Quaternion.Euler(45f, 45f, 45f);
            transform.TweenRotation(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.rotation, endValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_To()
        {
            var endValue = new Vector3(90f, 90f, 90f);
            transform.TweenEulerAngles(endValue, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_FromTo()
        {
            var startValue = new Vector3(-90f, -90f, -90f);
            var endValue = new Vector3(90f, 90f, 90f);
            transform.TweenEulerAngles(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_Punch()
        {
            var startValue = transform.eulerAngles;
            var strength = Vector3.one * 90f;
            transform.PunchEulerAngles(strength, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_Shake()
        {
            var startValue = transform.eulerAngles;
            var strength = Vector3.one * 90f;
            transform.ShakeEulerAngles(strength, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_OneAxis_To()
        {
            var endValue = new Vector3(45f, 90f, 135f);
            transform.TweenEulerAnglesX(endValue.x, Duration);
            transform.TweenEulerAnglesY(endValue.y, Duration);
            transform.TweenEulerAnglesZ(endValue.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_OneAxis_FromTo()
        {
            var startValue = new Vector3(-45f, -90f, -135f);
            var endValue = new Vector3(45f, 90f, 135f);
            transform.TweenEulerAnglesX(startValue.x, endValue.x, Duration);
            transform.TweenEulerAnglesY(startValue.y, endValue.y, Duration);
            transform.TweenEulerAnglesZ(startValue.z, endValue.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_OneAxis_Punch()
        {
            var startValue = transform.eulerAngles;
            var strength = new Vector3(45f, 90f, 135f);
            transform.PunchEulerAnglesX(strength.x, Duration);
            transform.PunchEulerAnglesY(strength.y, Duration);
            transform.PunchEulerAnglesZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_EulerAngles_OneAxis_Shake()
        {
            var startValue = transform.eulerAngles;
            var strength = new Vector3(45f, 90f, 135f);
            transform.ShakeEulerAnglesX(strength.x, Duration);
            transform.ShakeEulerAnglesY(strength.y, Duration);
            transform.ShakeEulerAnglesZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.eulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Scale_To()
        {
            var endValue = new Vector3(2f, 2f, 2f);
            transform.TweenLocalScale(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Scale_FromTo()
        {
            var startValue = new Vector3(1f, 2f, 3f);
            var endValue = new Vector3(3f, 2f, 1f);
            transform.TweenLocalScale(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Scale_Punch()
        {
            var startValue = transform.localScale;
            var strength = Vector3.one * 2f;
            transform.PunchLocalScale(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Scale_Shake()
        {
            var startValue = transform.localScale;
            var strength = Vector3.one * 2f;
            transform.ShakeLocalScale(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Scale_OneAxis_To()
        {
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.TweenLocalScaleX(endValueX, Duration);
            transform.TweenLocalScaleY(endValueY, Duration);
            transform.TweenLocalScaleZ(endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale.x, endValueX);
            AssertAreEqual(transform.localScale.y, endValueY);
            AssertAreEqual(transform.localScale.z, endValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Scale_OneAxis_FromTo()
        {
            var (startValueX, startValueY, startValueZ) = (0f, 1f, 2f);
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.TweenLocalScaleX(startValueX, endValueX, Duration);
            transform.TweenLocalScaleY(startValueY, endValueY, Duration);
            transform.TweenLocalScaleZ(startValueZ, endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale.x, endValueX);
            AssertAreEqual(transform.localScale.y, endValueY);
            AssertAreEqual(transform.localScale.z, endValueZ);
        }

        [UnityTest]
        public IEnumerator Test_Scale_OneAxis_Punch()
        {
            var startValue = transform.localScale;
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.PunchLocalScaleX(endValueX, Duration);
            transform.PunchLocalScaleY(endValueY, Duration);
            transform.PunchLocalScaleZ(endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale.x, startValue.x);
            AssertAreEqual(transform.localScale.y, startValue.y);
            AssertAreEqual(transform.localScale.z, startValue.z);
        }

        [UnityTest]
        public IEnumerator Test_Scale_OneAxis_Shake()
        {
            var startValue = transform.localScale;
            var (endValueX, endValueY, endValueZ) = (1f, 2f, 3f);
            transform.ShakeLocalScaleX(endValueX, Duration);
            transform.ShakeLocalScaleY(endValueY, Duration);
            transform.ShakeLocalScaleZ(endValueZ, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale.x, startValue.x);
            AssertAreEqual(transform.localScale.y, startValue.y);
            AssertAreEqual(transform.localScale.z, startValue.z);
        }

        static void AssertAreEqual(float a, float b)
        {
            Assert.That(a, Is.EqualTo(b).Using(FloatEqualityComparer.Instance));
        }

        static void AssertAreEqual(Vector3 a, Vector3 b)
        {
            Assert.That(a, Is.EqualTo(b).Using(Vector3EqualityComparer.Instance));
        }

        static void AssertAnglesAreEqual(Vector3 a, Vector3 b)
        {
            var aq = Quaternion.Euler(a);
            var bq = Quaternion.Euler(b);
            AssertAreEqual(aq, bq);
        }

        static void AssertAreEqual(Quaternion a, Quaternion b)
        {
            Assert.That(a, Is.EqualTo(b).Using(QuaternionEqualityComparer.Instance));
        }
    }

}
