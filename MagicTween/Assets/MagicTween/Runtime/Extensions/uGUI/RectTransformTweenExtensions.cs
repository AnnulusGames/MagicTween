using UnityEngine;
using Unity.Mathematics;
using MagicTween.Core;

namespace MagicTween
{
    public static class RectTransformTweenExtensions
    {
        static readonly TweenGetter<RectTransform, float2> anchoredPosGetter = self => self.anchoredPosition;
        static readonly TweenSetter<RectTransform, float2> anchoredPosSetter = (self, x) => self.anchoredPosition = x;
        static readonly TweenGetter<RectTransform, float> anchoredPosXGetter = self => self.anchoredPosition.x;
        static readonly TweenGetter<RectTransform, float> anchoredPosYGetter = self => self.anchoredPosition.y;
        static readonly TweenSetter<RectTransform, float> anchoredPosXSetter = (self, x) =>
        {
            var p = self.anchoredPosition;
            p.x = x;
            self.anchoredPosition = p;
        };
        static readonly TweenSetter<RectTransform, float> anchoredPosYSetter = (self, y) =>
        {
            var p = self.anchoredPosition;
            p.y = y;
            self.anchoredPosition = p;
        };
        static readonly TweenGetter<RectTransform, float3> anchoredPos3DGetter = self => self.anchoredPosition3D;
        static readonly TweenSetter<RectTransform, float3> anchoredPos3DSetter = (self, x) => self.anchoredPosition3D = x;
        static readonly TweenGetter<RectTransform, float> anchoredPos3DXGetter = self => self.anchoredPosition3D.x;
        static readonly TweenGetter<RectTransform, float> anchoredPos3DYGetter = self => self.anchoredPosition3D.y;
        static readonly TweenGetter<RectTransform, float> anchoredPos3DZGetter = self => self.anchoredPosition3D.z;
        static readonly TweenSetter<RectTransform, float> anchoredPos3DXSetter = (self, x) =>
        {
            var p = self.anchoredPosition3D;
            p.x = x;
            self.anchoredPosition3D = p;
        };
        static readonly TweenSetter<RectTransform, float> anchoredPos3DYSetter = (self, y) =>
        {
            var p = self.anchoredPosition3D;
            p.y = y;
            self.anchoredPosition3D = p;
        };
        static readonly TweenSetter<RectTransform, float> anchoredPos3DZSetter = (self, z) =>
        {
            var p = self.anchoredPosition3D;
            p.z = z;
            self.anchoredPosition3D = p;
        };

        static readonly TweenGetter<RectTransform, float2> pivotGetter = self => self.pivot;
        static readonly TweenSetter<RectTransform, float2> pivotSetter = (self, x) => self.pivot = x;
        static readonly TweenGetter<RectTransform, float> pivotXGetter = self => self.pivot.x;
        static readonly TweenGetter<RectTransform, float> pivotYGetter = self => self.pivot.y;
        static readonly TweenSetter<RectTransform, float> pivotXSetter = (self, x) =>
        {
            var p = self.pivot;
            p.x = x;
            self.pivot = p;
        };
        static readonly TweenSetter<RectTransform, float> pivotYSetter = (self, y) =>
        {
            var p = self.pivot;
            p.y = y;
            self.pivot = p;
        };

        static readonly TweenGetter<RectTransform, float2> sizeDeltaGetter = self => self.sizeDelta;
        static readonly TweenSetter<RectTransform, float2> sizeDeltaSetter = (self, x) => self.sizeDelta = x;
        static readonly TweenGetter<RectTransform, float> sizeDeltaXGetter = self => self.sizeDelta.x;
        static readonly TweenGetter<RectTransform, float> sizeDeltaYGetter = self => self.sizeDelta.y;
        static readonly TweenSetter<RectTransform, float> sizeDeltaXSetter = (self, x) =>
        {
            var p = self.sizeDelta;
            p.x = x;
            self.sizeDelta = p;
        };
        static readonly TweenSetter<RectTransform, float> sizeDeltaYSetter = (self, y) =>
        {
            var p = self.sizeDelta;
            p.y = y;
            self.sizeDelta = p;
        };

        public static Tween<float2, NoOptions> TweenAnchorMin(this RectTransform self, Vector2 endValue, float duration)
        {
            return Tween.To(self, self => self.anchorMin, (self, x) => self.anchorMin = x, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenAnchorMax(this RectTransform self, Vector2 endValue, float duration)
        {
            return Tween.To(self, self => self.anchorMax, (self, x) => self.anchorMax = x, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenAnchoredPosition(this RectTransform self, Vector2 endValue, float duration)
        {
            return Tween.To(self, anchoredPosGetter, anchoredPosSetter, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenAnchoredPosition(this RectTransform self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPosSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPositionX(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, anchoredPosXGetter, anchoredPosXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPositionX(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPosXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPositionY(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, anchoredPosYGetter, anchoredPosYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPositionY(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPosYSetter, startValue, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenAnchoredPosition3D(this RectTransform self, Vector3 endValue, float duration)
        {
            return Tween.To(self, anchoredPos3DGetter, anchoredPos3DSetter, endValue, duration);
        }

        public static Tween<float3, NoOptions> TweenAnchoredPosition3D(this RectTransform self, Vector3 startValue, Vector3 endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPos3DSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DX(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, anchoredPos3DXGetter, anchoredPos3DXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DX(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPos3DXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DY(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, anchoredPos3DYGetter, anchoredPos3DYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DY(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPos3DYSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DZ(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, anchoredPos3DZGetter, anchoredPos3DZSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenAnchoredPosition3DZ(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, anchoredPos3DZSetter, startValue, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenPivot(this RectTransform self, Vector2 endValue, float duration)
        {
            return Tween.To(self, pivotGetter, pivotSetter, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenPivot(this RectTransform self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(self, pivotSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPivotX(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, pivotXGetter, pivotXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPivotX(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, pivotXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPivotY(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, pivotYGetter, pivotYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenPivotY(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, pivotYSetter, startValue, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenSizeDelta(this RectTransform self, Vector2 endValue, float duration)
        {
            return Tween.To(self, sizeDeltaGetter, sizeDeltaSetter, endValue, duration);
        }

        public static Tween<float2, NoOptions> TweenSizeDelta(this RectTransform self, Vector2 startValue, Vector2 endValue, float duration)
        {
            return Tween.FromTo(self, sizeDeltaSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSizeDeltaX(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, sizeDeltaXGetter, sizeDeltaXSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSizeDeltaX(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, sizeDeltaXSetter, startValue, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSizeDeltaY(this RectTransform self, float endValue, float duration)
        {
            return Tween.To(self, sizeDeltaYGetter, sizeDeltaYSetter, endValue, duration);
        }

        public static Tween<float, NoOptions> TweenSizeDeltaY(this RectTransform self, float startValue, float endValue, float duration)
        {
            return Tween.FromTo(self, sizeDeltaYSetter, startValue, endValue, duration);
        }
    }
}