using System;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicTween.Core
{
    public sealed class FastAction
    {
        struct Item
        {
            public object target;
            public object action;
        }

        Item[] _items;
        int _count;

        public int Count => _count;

        public FastAction(int initialCapacity = 4)
        {
            _items = new Item[initialCapacity];
        }

        public FastAction(Action action)
        {
            _items = new Item[1];
            _items[0] = new Item() { target = null, action = action };
            _count = 1;
        }

        public void Add<T>(object target, Action<T> action) where T : class
        {
            if (_items.Length == _count)
            {
                Array.Resize(ref _items, _count * 2);
            }

            _items[_count] = new Item()
            {
                target = target,
                action = action
            };
            _count++;
        }

        public void Add(Action action)
        {
            if (_items.Length == _count)
            {
                Array.Resize(ref _items, _count * 2);
            }

            _items[_count] = new Item()
            {
                target = null,
                action = action
            };
            _count++;
        }

        public void Invoke()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (i == _count) return;
                var item = _items[i];
                if (item.target != null) UnsafeUtility.As<object, Action<object>>(ref item.action)?.Invoke(item.target);
                else UnsafeUtility.As<object, Action>(ref item.action)?.Invoke();
            }
        }

        public void Clear()
        {
            if (_count == 0) return;
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = default;
            }
            _count = 0;
        }
    }
}