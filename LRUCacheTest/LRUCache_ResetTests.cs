using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRUCache;
using System.Collections.Generic;

namespace LRUCacheTest
{
    [TestClass]
    public class LRUCache_ResetTests
    {
        [TestMethod]
        public void Reset_RemovesAllData()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(2, "B");
            lruCache.Put(3, "C");

            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 1UL);
            Assert.AreEqual(lruCache.CurrentSize, 3);
            Assert.AreEqual(lruCache.itemMap.Count, 3);
            Assert.AreEqual(lruCache.values.Count, 3);

            lruCache.Reset();

            Assert.AreEqual(lruCache.priorityQueue.Last, null);
            Assert.AreEqual(lruCache.priorityQueue.First, null);
            Assert.AreEqual(lruCache.itemMap.Count, 0);
            Assert.AreEqual(lruCache.values.Count, 0);
            Assert.AreEqual(lruCache.CurrentSize, 0);
        }
























        [TestMethod]
        public void Get_ExistingItemWithNoValueRecord_Throws()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(2, "B");
            lruCache.values.Remove(2);
            Assert.ThrowsException<KeyNotFoundException>(() => lruCache.Get(2), string.Empty);
        }

        [TestMethod]
        public void Get_ExistingItem_UpdatesToTopPriority()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(2, "B");
            lruCache.Put(3, "C");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            lruCache.Put(2, "B");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 2UL);
        }

        [TestMethod]
        public void Get_ExistingItem_ReturnsCorrectValue()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(2, "B");
            lruCache.Put(3, "C");
            Assert.AreEqual(lruCache.Get(2), "B");
        }
    }
}
