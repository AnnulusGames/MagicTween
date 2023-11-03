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
            var endValue = new Vector3(1f, 2f, 3f);
            transform.TweenPositionX(endValue.x, Duration);
            transform.TweenPositionY(endValue.y, Duration);
            transform.TweenPositionZ(endValue.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_FromTo()
        {
            var endValue = new Vector3(1f, 2f, 3f);
            transform.TweenPositionX(-1f, endValue.x, Duration);
            transform.TweenPositionY(-1f, endValue.y, Duration);
            transform.TweenPositionZ(-1f, endValue.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_Punch()
        {
            var strength = new Vector3(1f, 2f, 3f);
            var startValue = transform.position;
            transform.PunchPositionX(strength.x, Duration);
            transform.PunchPositionY(strength.y, Duration);
            transform.PunchPositionZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, startValue);
        }

        [UnityTest]
        public IEnumerator Test_Position_OneAxis_Shake()
        {
            var strength = new Vector3(1f, 2f, 3f);
            var startValue = transform.position;
            transform.ShakePositionX(strength.x, Duration);
            transform.ShakePositionY(strength.y, Duration);
            transform.ShakePositionZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.position, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_To()
        {
            var endValue = Vector3.one * 2f;
            transform.TweenLocalPosition(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_FromTo()
        {
            var startValue = Vector3.one * -2f;
            var endValue = Vector3.one * 2f;
            transform.TweenLocalPosition(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_Punch()
        {
            var startValue = transform.localPosition;
            var strength = Vector3.one * 2f;
            transform.PunchLocalPosition(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_Shake()
        {
            var startValue = transform.localPosition;
            var strength = Vector3.one * 2f;
            transform.ShakeLocalPosition(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_OneAxis_To()
        {
            var endValue = new Vector3(1f, 2f, 3f);
            transform.TweenLocalPositionX(endValue.x, Duration);
            transform.TweenLocalPositionY(endValue.y, Duration);
            transform.TweenLocalPositionZ(endValue.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_OneAxis_FromTo()
        {
            var endValue = new Vector3(1f, 2f, 3f);
            transform.TweenLocalPositionX(-1f, endValue.x, Duration);
            transform.TweenLocalPositionY(-1f, endValue.y, Duration);
            transform.TweenLocalPositionZ(-1f, endValue.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_OneAxis_Punch()
        {
            var strength = new Vector3(1f, 2f, 3f);
            var startValue = transform.localPosition;
            transform.PunchLocalPositionX(strength.x, Duration);
            transform.PunchLocalPositionY(strength.y, Duration);
            transform.PunchLocalPositionZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalPosition_OneAxis_Shake()
        {
            var strength = new Vector3(1f, 2f, 3f);
            var startValue = transform.localPosition;
            transform.ShakeLocalPositionX(strength.x, Duration);
            transform.ShakeLocalPositionY(strength.y, Duration);
            transform.ShakeLocalPositionZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localPosition, startValue);
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
        public IEnumerator Test_LocalRotation_To()
        {
            var endValue = Quaternion.Euler(45f, 45f, 45f);
            transform.TweenLocalRotation(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localRotation, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalRotation_FromTo()
        {
            var startValue = Quaternion.Euler(-45f, -45f, -45f);
            var endValue = Quaternion.Euler(45f, 45f, 45f);
            transform.TweenLocalRotation(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localRotation, endValue);
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
        public IEnumerator Test_LocalEulerAngles_To()
        {
            var endValue = new Vector3(90f, 90f, 90f);
            transform.TweenLocalEulerAngles(endValue, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_FromTo()
        {
            var startValue = new Vector3(-90f, -90f, -90f);
            var endValue = new Vector3(90f, 90f, 90f);
            transform.TweenLocalEulerAngles(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_Punch()
        {
            var startValue = transform.localEulerAngles;
            var strength = Vector3.one * 90f;
            transform.PunchLocalEulerAngles(strength, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_Shake()
        {
            var startValue = transform.localEulerAngles;
            var strength = Vector3.one * 90f;
            transform.ShakeLocalEulerAngles(strength, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_OneAxis_To()
        {
            var endValue = new Vector3(45f, 90f, 135f);
            transform.TweenLocalEulerAnglesX(endValue.x, Duration);
            transform.TweenLocalEulerAnglesY(endValue.y, Duration);
            transform.TweenLocalEulerAnglesZ(endValue.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_OneAxis_FromTo()
        {
            var startValue = new Vector3(-45f, -90f, -135f);
            var endValue = new Vector3(45f, 90f, 135f);
            transform.TweenLocalEulerAnglesX(startValue.x, endValue.x, Duration);
            transform.TweenLocalEulerAnglesY(startValue.y, endValue.y, Duration);
            transform.TweenLocalEulerAnglesZ(startValue.z, endValue.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_OneAxis_Punch()
        {
            var startValue = transform.localEulerAngles;
            var strength = new Vector3(45f, 90f, 135f);
            transform.PunchLocalEulerAnglesX(strength.x, Duration);
            transform.PunchLocalEulerAnglesY(strength.y, Duration);
            transform.PunchLocalEulerAnglesZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalEulerAngles_OneAxis_Shake()
        {
            var startValue = transform.localEulerAngles;
            var strength = new Vector3(45f, 90f, 135f);
            transform.ShakeLocalEulerAnglesX(strength.x, Duration);
            transform.ShakeLocalEulerAnglesY(strength.y, Duration);
            transform.ShakeLocalEulerAnglesZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAnglesAreEqual(transform.localEulerAngles, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_To()
        {
            var endValue = new Vector3(2f, 2f, 2f);
            transform.TweenLocalScale(endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_FromTo()
        {
            var startValue = new Vector3(1f, 2f, 3f);
            var endValue = new Vector3(3f, 2f, 1f);
            transform.TweenLocalScale(startValue, endValue, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_Punch()
        {
            var startValue = transform.localScale;
            var strength = Vector3.one * 2f;
            transform.PunchLocalScale(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_Shake()
        {
            var startValue = transform.localScale;
            var strength = Vector3.one * 2f;
            transform.ShakeLocalScale(strength, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_OneAxis_To()
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
        public IEnumerator Test_LocalScale_OneAxis_FromTo()
        {
            var startValue = new Vector3(0f, 1f, 2f);
            var endValue = new Vector3(1f, 2f, 3f);
            transform.TweenLocalScaleX(startValue.x, endValue.x, Duration);
            transform.TweenLocalScaleY(startValue.y, endValue.y, Duration);
            transform.TweenLocalScaleZ(startValue.z, endValue.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, endValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_OneAxis_Punch()
        {
            var startValue = transform.localScale;
            var strength = new Vector3(1f, 2f, 3f);
            transform.PunchLocalScaleX(strength.x, Duration);
            transform.PunchLocalScaleY(strength.y, Duration);
            transform.PunchLocalScaleZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
        }

        [UnityTest]
        public IEnumerator Test_LocalScale_OneAxis_Shake()
        {
            var startValue = transform.localScale;
            var strength = new Vector3(1f, 2f, 3f);
            transform.ShakeLocalScaleX(strength.x, Duration);
            transform.ShakeLocalScaleY(strength.y, Duration);
            transform.ShakeLocalScaleZ(strength.z, Duration);
            yield return waitForSeconds;
            AssertAreEqual(transform.localScale, startValue);
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
