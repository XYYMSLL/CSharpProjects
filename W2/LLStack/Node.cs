using System;
namespace LLStack
{
    public class Node<T>
    {
        private T _value;
        private Node<T> _next;

        public Node()
        {
            _value = default(T);
            _next = null;
        }

        public Node(T value)
        {
            _value = value;
            _next = null;
        }

        public Node(T value, Node<T> next)
        {
            _value = value;
            _next = next;
        }

        public Node<T> Next { get => _next; set => _next = value; }
        public T Value { get => _value; }
    }
}
