
using System;

namespace Day5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            NonPeople nonPeople = new NonPeople();
            People1 people1 = new People1();
            people1.Print();
            //Console.WriteLine((people1 as NonPeople) == null);
        }
    }

    class People
    { }

    class People1 : People
    {
        public void Print()
        {
            Console.WriteLine("people1");
        }

        ~People1()
        {
            Console.WriteLine("destructor");
        }
    }

    class NonPeople
    { }

    //class Singleton
    //{
    //    private static Singleton _instance = null;

    //    private Singleton() { }

    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //            _instance ??= new Singleton();

    //            return _instance;
    //        }
    //    }
    //}
}
