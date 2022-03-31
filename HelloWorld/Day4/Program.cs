using System;

namespace Day4
{

    struct MyStruct
    {
        public MyStruct(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }
        public int Age { get; }

        public override string ToString()
        {
            return Name + ", " + Age;
        }

        public bool Equals(MyStruct other)
        {
            return other.Name == Name;
        }
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            //Console.Write(typeof(string).Assembly.ImageRuntimeVersion);
        }
    }
}
