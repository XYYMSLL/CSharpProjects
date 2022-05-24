using System;
namespace LLStack
{
    public class LLStack<T>
    {
        private Node<T> _head;
        private int _count;

        public LLStack()
        {
            _head = null;
            _count = 0;
        }

        public LLStack(Node<T> head)
        {
            _head = head;
            _count = 1;
        }

        public Node<T> Head { get => _head; }
        public int Count { get => _count; }

        public T Peek()
        {
            return Head.Value;
        }

        public void Push(T value)
        {
            Node<T> newNode = new Node<T>(value, _head);
            _head = newNode;
            _count++;
        }

        public T Pop()
        {
            if (Count < 0)
            {
                throw new Exception("Empty Stack");
            }

            T temp = _head.Value;
            _head = _head.Next;
            _count--;
            return temp;
        }
    }
}
