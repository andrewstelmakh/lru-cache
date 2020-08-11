using System;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var lruCache = new LruCache<int>(10);
            lruCache.Put("key1", 1);
            lruCache.Put("key2", 2);
            lruCache.Put("key3", 3);
            lruCache.Put("key4", 4);
            lruCache.Put("key5", 5);
            lruCache.Put("key6", 6);
            lruCache.Put("key7", 7);
            lruCache.Put("key8", 8);
            lruCache.Put("key9", 9);
            lruCache.Put("key10", 10);

            foreach (var item in lruCache)
            {
                Console.Write(item);
                Console.Write(" ");
            }
        }
    }
}