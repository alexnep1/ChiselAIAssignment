using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRUCache;

namespace LRUCacheTest
{
    [TestClass]
    public class LRUCache_InsertionTests
    {
        [TestMethod]
        public void Put_ExistingItem_UpdatesPriority()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(3, "C");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            lruCache.Put(1, "AA");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 1UL);
        }

        [TestMethod]
        public void Put_ExistingItem_UpdatesValue()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(1, "AA");
            Assert.AreEqual(lruCache.values[1], "AA");
        }

        [TestMethod]
        public void Put_AddingFirstItem_UpdatesCacheProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(2, "B");
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 2UL);
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 2UL);
            Assert.AreEqual(lruCache.CurrentSize, 1);
        }

        [TestMethod]
        public void Put_AddingNewItem_UpdatesCacheProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(2, "B");
            Assert.AreEqual(lruCache.values[1], "A");
            Assert.AreEqual(lruCache.values[2], "B");
            Assert.AreEqual(lruCache.CurrentSize, 2);
            lruCache.Put(8, "ZZ");
            Assert.AreEqual(lruCache.values[8], "ZZ");
            Assert.AreEqual(lruCache.CurrentSize, 3);
        }

        [TestMethod]
        public void Put_AddingItemsOverLimit_UpdatesPriorityProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(3, "C");
            lruCache.Put(2, "B");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 2UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 1UL);
            lruCache.Put(17, "R");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 17UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 3UL);
            lruCache.Put(32, "K");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 32UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 2UL);
        }
    }
}
