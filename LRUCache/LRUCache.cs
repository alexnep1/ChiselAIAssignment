using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LRUCacheTest")]
namespace LRUCache
{
    public class LRUCache<TKey, TValue> : ILRUCache<TKey, TValue>
    {
        private readonly object _cacheLock;
        private readonly int _maxSize;

        internal int CurrentSize { get; private set; }
        internal LinkedList<TKey> priorityQueue;
        internal readonly Dictionary<TKey, LinkedListNode<TKey>> itemMap;
        internal readonly Dictionary<TKey, TValue> values;

        public LRUCache(int size)
        {
            _cacheLock = new object();
            _maxSize = size;
            CurrentSize = 0;
            priorityQueue = new LinkedList<TKey>();
            itemMap = new Dictionary<TKey, LinkedListNode<TKey>>();
            values = new Dictionary<TKey, TValue>();
        }

        public void Put(TKey key, TValue value)
        {
            lock (_cacheLock)
            {
                if (itemMap.TryGetValue(key, out var node))
                {
                    // item exists
                    UpdatePriority(node);
                    values[key] = value;
                    return;
                }

                AddToCache(key, value);

                while (CurrentSize > _maxSize)
                {
                    var keyToRemove = priorityQueue.First.Value;
                    priorityQueue.RemoveFirst();
                    RemoveFromCache(keyToRemove);
                }
            }
        }

        public TValue Get(TKey key)
        {
            lock (_cacheLock)
            {
                if (!itemMap.TryGetValue(key, out var node))
                    return default;

                UpdatePriority(node);

                return values.TryGetValue(key, out var value) ? value : throw new KeyNotFoundException($"Value corresponding to key = {key} was not found!");
            }
        }

        public void Delete(TKey key)
        {
            lock (_cacheLock)
            {
                if (itemMap.TryGetValue(key, out var node))
                {
                    priorityQueue.Remove(node);
                    RemoveFromCache(key);
                }
            }
        }

        public void Reset()
        {
            lock (_cacheLock)
            {
                priorityQueue.Clear();
                itemMap.Clear();
                values.Clear();
                CurrentSize = 0;
            }
        }

        private void UpdatePriority(LinkedListNode<TKey> node)
        {
            priorityQueue.Remove(node);
            priorityQueue.AddLast(node.Value);
        }

        private void AddToCache(TKey key, TValue value)
        {
            priorityQueue.AddLast(key);
            itemMap.Add(key, priorityQueue.Last);
            values.Add(key, value);
            CurrentSize++;
        }

        private void RemoveFromCache(TKey key)
        {
            itemMap.Remove(key);
            values.Remove(key);
            if (CurrentSize > 0)
                CurrentSize--;
        }
    }
}