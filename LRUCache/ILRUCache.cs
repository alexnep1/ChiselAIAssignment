namespace LRUCache
{
    public interface ILRUCache<TKey, TValue>
    {
        void Put(TKey key, TValue value);

        TValue Get(TKey key);

        void Delete(TKey key);

        void Reset();
    }
}