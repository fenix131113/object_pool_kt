namespace Core.ObjectPool
{
    public interface IObjectPool<T> where T : IObjectPoolItem<T>
    {
        T GetFromPool();
        void PutToPool(T item);
        public delegate T ItemCreateAction();
    }
}