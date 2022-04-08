using System;
using System.Collections.Generic;

namespace Day8
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            
        }
    }

    class Test1
    {
        int[] arr = new int[3];
        public int this[int num]
        {
            get => arr[num];
            set => arr[num] = value;
        }
    }

    class Test<T, V>
        where T : class, new()
        where V : Test1
    {

    }
}
