using UnityEngine;
using Unity.Mathematics;
using MagicTween.Core;

namespace MagicTween
{
    public static class Rigidbody2DTweenExtensions
    {
        static readonly TweenGetter<Rigidbody2D, float2> positionGetter = self => self.position;
        static readonly TweenSetter<Rigidbody2D, float2> positionSetter = (self, x) => self.MovePosition(x);
        static readonly TweenGetter<Rigidbody2D, float> positionXGetter = self => self.position.x;
        static readonly TweenGetter<Rigidbody2D, float> positionYGetter = self => self.position.y;
        static readonly TweenSetter<Rigidbody2D, float> positionXSetter = (self, x) =>
        {
            var p = self.position;
            p.x = x;
            self.MovePosition(p);
        };
        static readonly TweenSetter<Rigidbody2D, float> positionYSetter = (self, y) =>
        {
            var p = self.position;
            p.y = y;
            self.MovePosition(p);
        };

        public static Tween<float2, NoOptions> TweenPosition(this Rigidbody2D self, Vector2 endValue, float duration)
        {
            return Tween.To(self, positionGetter, positionSetter, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenPosition(this Rigidbody2D self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(self, positionSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Rigidbody2D self, float endValue, float duration)
        {
            return Tween.To(self, positionXGetter, positionXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionX(this Rigidbody2D self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Rigidbody2D self, float endValue, float duration)
        {
            return Tween.To(self, positionYGetter, positionYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPositionY(this Rigidbody2D self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, positionYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenRotation(this Rigidbody2D self, float endValue, float duration)
        {
            return Tween.To(self, self => self.rotation, (self, x) => self.MoveRotation(x), endValue, duration);
        }

        public static Tween<float, NoOptions> TweenRotation(this Rigidbody2D self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, (self, x) => self.MoveRotation(x), startValue, endValue, duration);
        }
    }
}