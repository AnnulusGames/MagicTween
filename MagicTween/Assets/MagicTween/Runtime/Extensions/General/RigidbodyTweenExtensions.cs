using UnityEngine;
using Unity.Mathematics;
using MagicTween.Core;

namespace MagicTween
{
    public static class RigidbodyTweenExtensions
    {
        static readonly TweenGetter<Rigidbody, float3> positionGetter = self => self.position;
        static readonly TweenSetter<Rigidbody, float3> positionSetter = (self, x) => self.MovePosition(x);
        static readonly TweenGetter<Rigidbody, float> positionXGetter = self => self.position.x;
        static readonly TweenGetter<Rigidbody, float> positionYGetter = self => self.position.y;
        static readonly TweenGetter<Rigidbody, float> positionZGetter = self => self.position.z;
        static readonly TweenSetter<Rigidbody, float> positionXSetter = (self, x) =>
        {
            var p = self.position;
            p.x = x;
            self.MovePosition(p);
        };
        static readonly TweenSetter<Rigidbody, float> positionYSetter = (self, y) =>
        {
            var p = self.position;
            p.y = y;
            self.MovePosition(p);
        };
        static readonly TweenSetter<Rigidbody, float> positionZSetter = (self, z) =>
        {
            var p = self.position;
            p.z = z;
            self.MovePosition(p);
        };

        public static Tween<float3, NoOptions> TweenPosition(this Rigidbody self, Vector3 endValue, float duration)
        {
            return Tween.To(self, positionGetter, positionSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenPosition(this Rigidbody self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, positionSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Rigidbody self, float endValue, float duration)
        {
            return Tween.To(self, positionXGetter, positionXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Rigidbody self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Rigidbody self, float endValue, float duration)
        {
            return Tween.To(self, positionYGetter, positionYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Rigidbody self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Rigidbody self, float endValue, float duration)
        {
            return Tween.To(self, positionZGetter, positionZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionZ(this Rigidbody self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionZSetter, startValue, endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Rigidbody self, Quaternion endValue, float duration)
        {
            return Tween.To(self, self => self.rotation, (self, x) => self.MoveRotation(x), endValue, duration);
        }

        public static Tween<quaternion, NoOptions> TweenRotation(this Rigidbody self, Quaternion startValue, Quaternion endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.MoveRotation(x), startValue, endValue, duration);
        }

        public static Tween<float3, PathTweenOptions> TweenPath(this Rigidbody self, float3[] points, float duration)
        {
            return Tween.Path(() => self.position, x => self.MovePosition(x), points, duration);
        }

        public static Tween<float3, PathTweenOptions> TweenPath(this Rigidbody self, Vector3[] points, float duration)
        {
            return Tween.Path(() => self.position, x => self.MovePosition(x), points, duration);
        }
    }
}