using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;

namespace MagicTween.Core
{
    [BurstCompile]
    internal static class MathUtils
    {
        [BurstCompile]
        public static bool Approximately(float a, float b)
        {
            return math.abs(b - a) < math.max(0.000001f * math.max(math.abs(a), math.abs(b)), math.EPSILON * 8);
        }

        public static quaternion ToQuaternion(float3 v)
        {
            ToQuaternionCore(v.y, v.x, v.z, out var result);
            return result;
        }

        public static float3 ToEulerAngles(quaternion quaternion)
        {
            ToEulerAnglesCore(ref quaternion, out var result);
            return result;
        }

        [BurstCompile]
        public static void ToQuaternionCore(float yaw, float pitch, float roll, out quaternion result)
        {
            yaw *= Mathf.Deg2Rad;
            pitch *= Mathf.Deg2Rad;
            roll *= Mathf.Deg2Rad;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = math.sin(rollOver2);
            float cosRollOver2 = math.cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = math.sin(pitchOver2);
            float cosPitchOver2 = math.cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = math.sin(yawOver2);
            float cosYawOver2 = math.cos(yawOver2);
            float4 f4 = default;
            f4.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            f4.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            f4.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            f4.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

            result = new quaternion(f4);
        }

        [BurstCompile]
        public static void ToEulerAnglesCore(ref quaternion quaternion, out float3 result)
        {
            float4 q1 = quaternion.value;
            float sqw = q1.w * q1.w;
            float sqx = q1.x * q1.x;
            float sqy = q1.y * q1.y;
            float sqz = q1.z * q1.z;
            float unit = sqx + sqy + sqz + sqw;
            float test = q1.x * q1.w - q1.y * q1.z;
            float3 v;

            if (test > 0.4995f * unit)
            {
                v.y = 2f * math.atan2(q1.y, q1.x);
                v.x = math.PI / 2;
                v.z = 0;
                NormalizeAngles(v * Mathf.Rad2Deg, out result);
                return;
            }
            if (test < -0.4995f * unit)
            {
                v.y = -2f * math.atan2(q1.y, q1.x);
                v.x = -math.PI / 2;
                v.z = 0;
                NormalizeAngles(v * Mathf.Rad2Deg, out result);
                return;
            }
            float4 q = new float4(q1.w, q1.z, q1.x, q1.y);
            v.y = math.atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));
            v.x = math.asin(2f * (q.x * q.z - q.w * q.y));
            v.z = math.atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));
            NormalizeAngles(v * Mathf.Rad2Deg, out result);
        }

        static void NormalizeAngles(float3 angles, out float3 result)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            result = angles;
        }

        static float NormalizeAngle(float angle)
        {
            while (angle > 360) angle -= 360;
            while (angle < 0) angle += 360;
            return angle;
        }
    }
}