using System;
using System.Collections;
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

        class MyKvp<TKey, TValue>
        {
            TKey _key;
            TValue _value;

            public TKey Key
            {
                get => _key;
            }

            public TValue Value
            {
                get => _value;
                set => _value = value;
            }

            public MyKvp(TKey key, TValue value)
            {
                _key = key;
                _value = value;
            }
        }

        class MyDictionary<TKey, TValue>
        {
            MyKvp<TKey, TValue>[] _data;
            int count;
            bool _isReadOnly;

            public ICollection<TKey> Keys
            {
                get
                {
                    TKey[] keys = new TKey[count];
                    for (int i = 0; i < count; i++)
                    {
                        keys[i] = _data[i].Key;
                    }
                    return keys;
                }
            }

            public ICollection<TValue> Values
            {
                get
                {
                    TValue[] values = new TValue[count];
                    for (int i = 0; i < count; i++)
                    {
                        values[i] = _data[i].Value;
                    }
                    return values;
                }
            }

            public int Count => count;

            public bool IsReadOnly => _isReadOnly;

            public TValue this[TKey key]
            {
                get => GetValueByKey(key);
                set
                {
                    if (!_isReadOnly)
                    {
                        int i = GetIndexByKey(key);

                        if (i == -1)
                        { _data[GetIndexByKey(key)].Value = value; }
                        else
                        {
                            Add(key, value);
                        }
                    }
                    else
                    {
                        throw new Exception("Dictionary is read only");
                    }
                }
            }

            public MyDictionary()
            {
                _data = new MyKvp<TKey, TValue>[4];
                count = 0;
                _isReadOnly = false;
            }

            public MyDictionary(bool isReadOnly)
            {
                _data = new MyKvp<TKey, TValue>[4];
                count = 0;
                _isReadOnly = isReadOnly;
            }

            public TValue GetValueByKey(TKey key)
            {
                int i = GetIndexByKey(key);

                if (i >= 0)
                {
                    return _data[i].Value;
                }

                throw new Exception("{key} not exists");
            }

            public void Add(TKey key, TValue value)
            {
                int i = GetIndexByKey(key);

                if (i < 0)
                {
                    MyKvp<TKey, TValue> newKvp = new MyKvp<TKey, TValue>(key, value);
                    if (count >= _data.Length)
                    {
                        Expand();
                    }
                    _data[count] = newKvp;
                    count++;
                    return;
                }

                throw new Exception("{key} already exists");
            }

            public bool Remove(TKey key)
            {
                int i = GetIndexByKey(key);

                if (i >= 0)
                {
                    Array.Copy(_data, i + 1, _data, i, count - i - 1);
                    count--;
                    return true;
                }

                return false;
            }

            private void Expand()
            {
                MyKvp<TKey, TValue>[] newData = new MyKvp<TKey, TValue>[_data.Length * 2];
                Array.Copy(_data, newData, _data.Length);
                _data = newData;
            }

            private int GetIndexByKey(TKey key)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_data[i].Key.Equals(key))
                    {
                        return i;
                    }
                }

                return -1;
            }

            public bool ContainsKey(TKey key)
            {
                int i = GetIndexByKey(key);
                return i != -1;
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                int i = GetIndexByKey(key);

                if (i >= 0)
                {
                    value = _data[i].Value;
                    return true;
                }

                value = default(TValue);

                return false;
            }

            public void Add(MyKvp<TKey, TValue> item)
            {
                int i = GetIndexByKey(item.Key);

                if (i < 0)
                {
                    if (count >= _data.Length)
                    {
                        Expand();
                    }
                    _data[count] = item;
                    count++;
                    return;
                }

                throw new Exception("Key name \"{key}\" already exists");
            }

            public void Clear()
            {
                _data = new MyKvp<TKey, TValue>[4];
                count = 0;
            }
        }
    }
}
