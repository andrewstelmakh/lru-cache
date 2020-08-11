using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LRUCache.UnitTests
{
    public class WhenWorkingWithLruCache
    {
        private readonly LruCache<int> _cache;

        public WhenWorkingWithLruCache()
        {
            _cache = new LruCache<int>(5);
        }
        
        [Fact]
        public void ShouldPutItemOnTheHeadIfCacheIsEmpty()
        {
            var value = 150;
            var key = "key";
            
            _cache.Put(key, value);

            var result = _cache.Get(key);
            
            Assert.Equal(value, result);

            AssertItemsInCache(_cache, 150);
        }
        
        [Fact]
        public void ShouldPutItemOnTheHeadIfCacheIsNotEmpty()
        {
            var value1 = 150;
            var key1 = "key1";
            
            var value2 = 300;
            var key2= "key2";
            
            _cache.Put(key1, value1);
            _cache.Put(key2, value2);

            var result1 = _cache.Get(key1);
            var result2 = _cache.Get(key2);

           Assert.Equal(value1, result1);
           Assert.Equal(value2, result2);
           
           AssertItemsInCache(_cache, 300, 150);
        }

        [Fact]
        public void ShouldThrowKeyNotFoundExceptionIfGettingWrongKey()
        {
            Assert.Throws<KeyNotFoundException>(() => _cache.Get("Test"));
        }

        [Fact]
        public void ShouldRemoveOldestValueWhenCapacityIsOverflown()
        {
            _cache.Put("key1", 1);
            _cache.Put("key2", 2);
            _cache.Put("key3", 3);
            _cache.Put("key4", 4);
            _cache.Put("key5", 5);
            
            _cache.Put("key6", 6);
            
            AssertItemsInCache(_cache, 6, 5, 4, 3, 2);
            
            Assert.Throws<KeyNotFoundException>(() => _cache.Get("key1"));
        }
        
        [Fact]
        public void ShouldUpdateTailReference()
        {
            _cache.Put("key1", 1);
            _cache.Put("key2", 2);
            _cache.Put("key3", 3);
            _cache.Put("key4", 4);
            _cache.Put("key5", 5);
            
            var result = _cache.Get("key1");
            
            Assert.Equal(1, result);
            
            AssertItemsInCache(_cache, 1, 5, 4, 3, 2);
        }
        
        [Fact]
        public void ShouldUpdateReferenceInTheMiddle()
        {
            _cache.Put("key1", 1);
            _cache.Put("key2", 2);
            _cache.Put("key3", 3);
            _cache.Put("key4", 4);
            _cache.Put("key5", 5);
            
            var result = _cache.Get("key3");
            
            Assert.Equal(3, result);
            
            AssertItemsInCache(_cache, 3, 5, 4, 2, 1);
        }
        
        [Fact]
        public void ShouldUpdateItemOnKey()
        {
            _cache.Put("key1", 1);
            _cache.Put("key1", 2);
            _cache.Put("key1", 3);
            
            var result = _cache.Get("key1");
            
            Assert.Equal(3, result);

            AssertItemsInCache(_cache, 3);
        }

        [Fact]
        public void ShouldMoveItemToTheHeadWhenUpdated()
        {
            _cache.Put("key1", 1);
            _cache.Put("key2", 2);
            _cache.Put("key3", 3);
            
            _cache.Put("key1", 100);
            
            AssertItemsInCache(_cache, 100, 3, 2);
        }

        private void AssertItemsInCache(LruCache<int> cache, params int[] items)
        {
            var index = 0;
            foreach (var item in cache)
            {
                Assert.Equal(items[index], item);
                index++;
            }
        }
    }
}