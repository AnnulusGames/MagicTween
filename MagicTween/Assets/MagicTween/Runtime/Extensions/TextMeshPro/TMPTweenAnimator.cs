using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using TMPro;
using MagicTween.Core;

namespace MagicTween
{
    public sealed class TMPTweenAnimator
    {
        internal TMPTweenAnimator(TMP_Text tmpText)
        {
            this.tmpText = tmpText;
            Reset();
        }

        internal struct TweenCharInfo
        {
            public Vector3 offset;
            public Vector3 scale;
            public Quaternion rotation;
            public Color color;
        }

        internal TweenCharInfo[] tweenCharInfo = new TweenCharInfo[0];
        readonly List<Tween> tweens = new();
        TMP_Text tmpText;

        public void SetCharColor(int index, Color value)
        {
            tweenCharInfo[index].color = value;
        }

        public void SetCharColorAlpha(int index, float value)
        {
            var color = tweenCharInfo[index].color;
            color.a = value;
            tweenCharInfo[index].color = color;
        }

        public void SetCharOffset(int index, Vector3 value)
        {
            tweenCharInfo[index].offset = value;
        }

        public void SetCharScale(int index, Vector3 value)
        {
            tweenCharInfo[index].scale = value;
        }

        public void SetCharEulerAngles(int index, Vector3 value)
        {
            tweenCharInfo[index].rotation = Quaternion.Euler(value);
        }

        public void SetCharRotation(int index, Quaternion value)
        {
            tweenCharInfo[index].rotation = value;
        }

        public Tween<float4, NoOptions> TweenCharColor(int index, Color endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(
                this,
                self => UnsafeUtility.As<Color, float4>(ref self.tweenCharInfo[index].color),
                (self, x) => self.tweenCharInfo[index].color = UnsafeUtility.As<float4, Color>(ref x),
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float4, NoOptions> TweenCharColor(int index, Color startValue, Color endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(
                this,
                (self, x) => self.tweenCharInfo[index].color = UnsafeUtility.As<float4, Color>(ref x),
                UnsafeUtility.As<Color, float4>(ref startValue),
                UnsafeUtility.As<Color, float4>(ref endValue),
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float, NoOptions> TweenCharColorAlpha(int index, float endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(
                this,
                self => self.tweenCharInfo[index].color.a,
                (self, x) =>
                {
                    var color = self.tweenCharInfo[index].color;
                    color.a = x;
                    self.tweenCharInfo[index].color = color;
                },
                endValue,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float, NoOptions> TweenCharColorAlpha(int index, float startValue, float endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(
                this,
                (self, x) =>
                {
                    var color = self.tweenCharInfo[index].color;
                    color.a = x;
                    self.tweenCharInfo[index].color = color;
                },
                startValue,
                endValue,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharOffset(int index, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(this, self => self.tweenCharInfo[index].offset, (self, x) => self.tweenCharInfo[index].offset = x, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharOffset(int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(this, (self, x) => self.tweenCharInfo[index].offset = x, startValue, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharEulerAngles(int index, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(
                this,
                self => MathUtils.ToEulerAngles(self.tweenCharInfo[index].rotation),
                (self, x) => self.tweenCharInfo[index].rotation = MathUtils.ToQuaternion(x),
                endValue,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharEulerAngles(int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(
                this,
                (self, x) => self.tweenCharInfo[index].rotation = MathUtils.ToQuaternion(x),
                startValue,
                endValue,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<quaternion, NoOptions> TweenCharRotation(int index, Quaternion endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(this, self => self.tweenCharInfo[index].rotation, (self, x) => self.tweenCharInfo[index].rotation = x, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<quaternion, NoOptions> TweenCharRotation(int index, Quaternion startValue, Quaternion endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(this, (self, x) => self.tweenCharInfo[index].rotation = x, startValue, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharScale(int index, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.To(this, self => self.tweenCharInfo[index].scale, (self, x) => self.tweenCharInfo[index].scale = x, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, NoOptions> TweenCharScale(int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            ResizeArray();
            var tween = Tween.FromTo(this, (self, x) => self.tweenCharInfo[index].scale = x, startValue, endValue, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, PunchTweenOptions> PunchCharOffset(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Punch(this, self => self.tweenCharInfo[index].offset, (self, x) => self.tweenCharInfo[index].offset = x, strength, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, PunchTweenOptions> PunchCharEulerAngles(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Punch(
                this,
                self => MathUtils.ToEulerAngles(self.tweenCharInfo[index].rotation),
                (self, x) => self.tweenCharInfo[index].rotation = MathUtils.ToQuaternion(x),
                strength,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, PunchTweenOptions> PunchCharScale(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Punch(this, self => self.tweenCharInfo[index].scale, (self, x) => self.tweenCharInfo[index].scale = x, strength, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, ShakeTweenOptions> ShakeCharOffset(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Shake(this, self => self.tweenCharInfo[index].offset, (self, x) => self.tweenCharInfo[index].offset = x, strength, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, ShakeTweenOptions> ShakeCharEulerAngles(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Shake(
                this,
                self => MathUtils.ToEulerAngles(self.tweenCharInfo[index].rotation),
                (self, x) => self.tweenCharInfo[index].rotation = MathUtils.ToQuaternion(x),
                strength,
                duration
            );
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public Tween<float3, ShakeTweenOptions> ShakeCharScale(int index, Vector3 strength, float duration)
        {
            ResizeArray();
            var tween = Tween.Shake(this, self => self.tweenCharInfo[index].scale, (self, x) => self.tweenCharInfo[index].scale = x, strength, duration);
            AddTween(tween.AsUnitTween());
            return tween;
        }

        public int GetCharCount()
        {
            return math.min(tmpText.textInfo.characterCount, tmpText.textInfo.characterInfo.Length);
        }

        public void Reset()
        {
            tmpText.ForceMeshUpdate();

            ResizeArray();

            for (int i = 0; i < tweens.Count; i++)
            {
                if (tweens[i].IsActive()) tweens[i].Kill();
            }
            tweens.Clear();

            for (int i = 0; i < tweenCharInfo.Length; i++)
            {
                tweenCharInfo[i].color = new(tmpText.color.r, tmpText.color.g, tmpText.color.b, tmpText.color.a);
                tweenCharInfo[i].rotation = Quaternion.identity;
                tweenCharInfo[i].scale = Vector3.one;
                tweenCharInfo[i].offset = Vector3.zero;
            }
        }

        internal void AddTween(Tween tween)
        {
            tweens.Add(tween);
        }

        internal void ResizeArray()
        {
            var length = GetCharCount();
            var prevLength = tweenCharInfo.Length;
            if (length != prevLength)
            {
                Array.Resize(ref tweenCharInfo, length);

                if (length > prevLength)
                {
                    for (int i = prevLength; i < length; i++)
                    {
                        tweenCharInfo[i].color = new(tmpText.color.r, tmpText.color.g, tmpText.color.b, tmpText.color.a);
                        tweenCharInfo[i].rotation = Quaternion.identity;
                        tweenCharInfo[i].scale = Vector3.one;
                        tweenCharInfo[i].offset = Vector3.zero;
                    }
                }
            }
        }

        public void Update()
        {
            UpdateInternal();
        }

        internal bool UpdateInternal()
        {
            if (tmpText == null) return false;

            // TODO: Improve performance
            tweens.RemoveAll(x => !x.IsActive());
            if (tweens.Count == 0) return true;

            if (!tmpText.gameObject.activeInHierarchy) return true;

            tmpText.ForceMeshUpdate();
            ResizeArray();

            var textInfo = tmpText.textInfo;
            for (int i = 0; i < tweenCharInfo.Length; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var materialIndex = charInfo.materialReferenceIndex;
                var vertexIndex = charInfo.vertexIndex;

                var colors = textInfo.meshInfo[materialIndex].colors32;
                var charColor = tweenCharInfo[i].color;
                for (int n = 0; n < 4; n++)
                {
                    colors[vertexIndex + n] = charColor;
                }

                var verts = textInfo.meshInfo[materialIndex].vertices;
                var center = (verts[vertexIndex] + verts[vertexIndex + 2]) * 0.5f;

                var charRotation = tweenCharInfo[i].rotation;
                var charScale = tweenCharInfo[i].scale;
                var charOffset = tweenCharInfo[i].offset;
                for (int n = 0; n < 4; n++)
                {
                    var vert = verts[vertexIndex + n];
                    var dir = vert - center;
                    verts[vertexIndex + n] = center +
                        charRotation * new Vector3(dir.x * charScale.x, dir.y * charScale.y, dir.z * charScale.z) +
                        charOffset;
                }
            }

            for (int i = 0; i < textInfo.materialCount; i++)
            {
                if (textInfo.meshInfo[i].mesh == null) continue;
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                tmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            return true;
        }

        internal TMP_Text GetTMPText() => tmpText;
    }
}