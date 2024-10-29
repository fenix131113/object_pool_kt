using System.Collections.Generic;
using UnityEngine;

namespace Core.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> where T : IObjectPoolItem<T>
    {
        protected readonly Queue<T> _items = new();
        protected readonly List<T> _unavailableBullets = new();
        protected IObjectPool<T>.ItemCreateAction _createAction;

        public ObjectPool(IObjectPool<T>.ItemCreateAction createAction) => _createAction = createAction;

        public T GetFromPool()
        {
            if (_items.Count == 0)
                PutToPool(_createAction());

            var item = _items.Dequeue();
            _unavailableBullets.Add(item);

            item.OnObjectLifeEndRequest += PutToPool;

            return item;
        }


        public void PutToPool(T item)
        {
            if (_items.Contains(item))
            {
#if UNITY_EDITOR
                Debug.LogWarning("This bullet already exists in the pool!");
#endif
                return;
            }
            
            if (_unavailableBullets.Contains(item))
                _unavailableBullets.Remove(item);

            _items.Enqueue(item);
        }
    }
}