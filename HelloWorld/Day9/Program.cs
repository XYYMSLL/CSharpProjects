using System;
using System.Collections.Generic;

namespace Day9
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        class MyQueue<T>
        {
            T[] _data;
            int _head;
            int _tail;

            public MyQueue(int size = 4)
            {
                _data = new T[size];
                _head = 0;
                _tail = 0;
            }

            public int Count
            {
                get => (_tail - _head);
            }

            public void EnQueue(T t)
            {
                if (_tail >= _data.Length)
                {
                    if (_head == 0)
                    {
                        Expand();
                    }
                    else
                    {
                        TrimHead();
                    }
                }

                _data[_tail] = t;
                _tail++;
            }

            public T DeQueue()
            {
                if (Count <= 0)
                {
                    ThrowError("Empty Queue");
                }

                T Value = _data[_head];

                _head++;

                if (_head >= _data.Length / 10)
                {
                    TrimHead();
                }

                return Value;
            }

            public T Peek()
            {
                if (Count <= 0)
                {
                    ThrowError("Empty Queue");
                }

                return _data[_head];
            }

            public void Clear()
            {
                _data = new T[4];
                _head = 0;
                _tail = 0;
            }

            private void Expand()
            {
                T[] newData = new T[_data.Length * 2];
                Array.Copy(_data, 0, newData, 0, _data.Length);
                _data = newData;
            }

            private void TrimHead()
            {
                int temp = Count;
                Array.Copy(_data, _head, _data, 0, temp);
                _head = 0;
                _tail = temp;
            }

            private void ThrowError(string msg)
            {
                throw new Exception(msg);
            }
        }
    }
}
