namespace LRUCache
{
    public class Node<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }
    }
}