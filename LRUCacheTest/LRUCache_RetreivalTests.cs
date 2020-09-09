using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRUCache;
using System.Collections.Generic;

namespace LRUCacheTest
{
    [TestClass]
    public class LRUCache_RetreivalTests
    {
        [TestMethod]
        public void Get_NonExistingItem_ReturnsEmpty()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            Assert.AreEqual(lruCache.Get(2), null);
            lruCache.Put(1, "A");
            Assert.AreEqual(lruCache.Get(2), null);
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
