using System;
using Unity.Entities;
using MagicTween.Core;

namespace MagicTween
{
    public readonly struct Sequence : ITweenHandle
    {
        public static Sequence Create()
        {
            return TweenFactory.CreateSequence();
        }

        public Sequence(Entity entity)
        {
            this.entity = entity;
        }

        readonly Entity entity;

        public Tween AsUnitTween() => new Tween(entity);
        public Entity GetEntity() => entity;

        public static implicit operator Tween(Sequence sequence)
        {
            return sequence.AsUnitTween();
        }

        public Sequence Append<T>(in T tween) where T : struct, ITweenHandle
        {
            SequenceHelper.Append(this, tween, false);
            return this;
        }

        public Sequence AppendInterval(float interval)
        {
            SequenceHelper.AppendInterval(this, interval);
            return this;
        }

        public Sequence AppendCallback(Action callback)
        {
            var tween = Tween.Empty(0f);
            var callbackComponent = tween.GetOrAddCallbackActions();
            callbackComponent.onComplete.Add(callback);
            callbackComponent.onRewind.Add(callback);
            SequenceHelper.Append(this, tween, false);
            return this;
        }

        public Sequence Join<T>(in T tween) where T : struct, ITweenHandle
        {
            SequenceHelper.Append(this, tween, true);
            return this;
        }

        public Sequence Prepend<T>(in T tween) where T : struct, ITweenHandle
        {
            SequenceHelper.Prepend(this, tween);
            return this;
        }

        public Sequence PrependInterval(float interval)
        {
            SequenceHelper.PrependInterval(this, interval);
            return this;
        }

        public Sequence PrependCallback(Action callback)
        {
            var tween = Tween.Empty(0f);
            var callbackComponent = tween.GetOrAddCallbackActions();
            callbackComponent.onComplete.Add(callback);
            callbackComponent.onRewind.Add(callback);
            SequenceHelper.Prepend(this, tween);
            return this;
        }

        public Sequence Insert<T>(float position, T tween) where T : struct, ITweenHandle
        {
            SequenceHelper.Insert(this, tween, position);
            return this;
        }

        public Sequence InsertCallback(float position, Action callback)
        {
            var tween = Tween.Empty(0f);
            var callbackComponent = tween.GetOrAddCallbackActions();
            callbackComponent.onComplete.Add(callback);
            callbackComponent.onRewind.Add(callback);
            SequenceHelper.Insert(this, tween, position);
            return this;
        }
    }
}