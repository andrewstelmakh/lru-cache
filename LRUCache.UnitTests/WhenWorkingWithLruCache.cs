using System.Collections.Generic;
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
        }
    }
}