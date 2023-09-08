#if MAGICTWEEN_SUPPORT_UNIRX
using System;
using System.Collections.Generic;
using MagicTween.Core;

namespace MagicTween
{
    internal sealed class TweenObservable<TValue, TOptions> : IObservable<TValue>
        where TValue : unmanaged
        where TOptions : unmanaged, ITweenOptions
    {
        public TweenObservable(Tween<TValue, TOptions> tween)
        {
            var callbacks = tween.GetOrAddCallbackActions();
            callbacks.onUpdate += () =>
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    observers[i].OnNext(tween.GetValue());
                }
            };
            callbacks.onComplete += () =>
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    observers[i].OnNext(tween.GetValue());
                }
            };
            callbacks.onKill += () =>
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    observers[i].OnCompleted();
                }
            };
        }

        readonly List<IObserver<TValue>> observers = new();

        public IDisposable Subscribe(IObserver<TValue> observer)
        {
            observers.Add(observer);
            return new Subscription(this, observer);
        }

        sealed class Subscription : IDisposable
        {
            readonly TweenObservable<TValue, TOptions> observable;
            readonly IObserver<TValue> item;

            public Subscription(TweenObservable<TValue, TOptions> observable, IObserver<TValue> item)
            {
                this.observable = observable;
                this.item = item;
            }

            public void Dispose()
            {
                observable.observers.Remove(item);
            }
        }
    }
}
#endif