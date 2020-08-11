using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LRUCache
{
    public class LruCache<T> : IEnumerable<T>
    {
        private readonly int _capacity;
        private readonly ConcurrentDictionary<string, Node<T>> _map;
        private Node<T> _head;
        private Node<T> _tail;

        public LruCache(int capacity)
        {
            _capacity = capacity;
            _map = new ConcurrentDictionary<string, Node<T>>(Environment.ProcessorCount, capacity);
        }

        public void Put(string key, T value)
        {
            if (_map.Count >= _capacity)
            {
                RemoveOldestNode();
            }
            
            if (!_map.ContainsKey(key))
            {
                var node = new Node<T> { Key = key, Value = value };
                _map.TryAdd(key, node);
                
                AddNodeToTheHead(node);
            }
            else
            {
                var node = _map[key];
                node.Value = value;
                
                MoveNodeToTheHead(node);
            }
        }

        private void RemoveOldestNode()
        {
            _map.TryRemove(_tail.Key, out var _);
            
            var tailPrev = _tail.Previous;
            tailPrev.Next = null;
            
            _tail = tailPrev;
        }

        private void AddNodeToTheHead(Node<T> newNode)
        {
            if (_head == null || _head.Equals(newNode))
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                var oldHead = _head;

                _head = newNode;

                newNode.Next = oldHead;
                oldHead.Previous = newNode;
            }
        }
        
        private void MoveNodeToTheHead(Node<T> node)
        {
            var previousNode = node.Previous;
            var nextNode = node.Next;

            if (previousNode != null)
            {
                previousNode.Next = nextNode;
            }

            if (node.Equals(_tail) && !node.Equals(_head))
            {
                var prevTail = _tail.Previous;
                prevTail.Next = null;

                _tail = prevTail;
            }

            AddNodeToTheHead(node);
        }

        public T Get(string key)
        {
            if (_map.ContainsKey(key))
            {
                var node = _map[key];
                
                MoveNodeToTheHead(node);
                
                return node.Value;
            }
            
            throw new KeyNotFoundException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }      
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}