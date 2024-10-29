using System;

namespace Core.ObjectPool
{
    public interface IObjectPoolItem<out T>
    {
        public event Action<T> OnObjectLifeEndRequest;
    }
}