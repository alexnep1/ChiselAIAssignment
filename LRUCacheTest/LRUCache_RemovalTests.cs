using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRUCache;

namespace LRUCacheTest
{
    [TestClass]
    public class LRUCache_RemovalTests
    {
        [TestMethod]
        public void Delete_NonExistingItem_DoNotChangeState()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(3, "C");
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 1UL);
            Assert.AreEqual(lruCache.CurrentSize, 2);
            lruCache.Delete(2);
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 1UL);
            Assert.AreEqual(lruCache.CurrentSize, 2);
        }

        [TestMethod]
        public void Delete_ExistingItem_RemovesProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(1, "A");
            lruCache.Put(3, "C");
            lruCache.Put(2, "B");
            Assert.IsTrue(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsTrue(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.CurrentSize, 3);
            lruCache.Delete(3UL);
            Assert.IsFalse(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsFalse(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.CurrentSize, 2);
        }

        [TestMethod]
        public void Delete_ExistingLastItem_RemovesProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(3, "C");

            Assert.IsTrue(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsTrue(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 3UL);
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 3UL);
            Assert.AreEqual(lruCache.CurrentSize, 1);

            lruCache.Delete(3UL);

            Assert.IsFalse(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsFalse(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.Last, null);
            Assert.AreEqual(lruCache.priorityQueue.First, null);
            Assert.AreEqual(lruCache.CurrentSize, 0);
        }

        [TestMethod]
        public void Delete_LRUItem_RemovesProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(3, "C");
            lruCache.Put(81, "T");
            lruCache.Put(13, "B");

            Assert.IsTrue(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsTrue(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 3UL);
            Assert.AreEqual(lruCache.CurrentSize, 3);

            lruCache.Delete(3UL);

            Assert.IsFalse(lruCache.itemMap.TryGetValue(3UL, out _));
            Assert.IsFalse(lruCache.values.TryGetValue(3UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.First.Value, 81UL);
            Assert.AreEqual(lruCache.CurrentSize, 2);
        }

        [TestMethod]
        public void Delete_MRUItem_RemovesProperly()
        {
            var lruCache = new LRUCache<ulong, string>(3);
            lruCache.Put(3, "C");
            lruCache.Put(104, "T");
            lruCache.Put(13, "B");

            Assert.IsTrue(lruCache.itemMap.TryGetValue(13UL, out _));
            Assert.IsTrue(lruCache.values.TryGetValue(13UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 13UL);
            Assert.AreEqual(lruCache.CurrentSize, 3);

            lruCache.Delete(13UL);

            Assert.IsFalse(lruCache.itemMap.TryGetValue(13UL, out _));
            Assert.IsFalse(lruCache.values.TryGetValue(13UL, out _));
            Assert.AreEqual(lruCache.priorityQueue.Last.Value, 104UL);
            Assert.AreEqual(lruCache.CurrentSize, 2);
        }
    }
}
