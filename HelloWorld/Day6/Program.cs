using System;

namespace Day6
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    interface IFix
    {
        void FixSony();
        void FixIphone();
        void FixHuawei();
        void FixXiaomi();
        void FixLenovo();
        void FixSamsung();
        void FixOnePlus();
    }

    class FixShop : IFix
    {
        public FixShop()
        {
            Console.WriteLine("Fix shop opened");
            Console.WriteLine("iFix tools inherited");
        }

        void IFix.FixSony()
        { }
        void IFix.FixIphone()
        { }
        public void FixHuawei()
        { }
        public void FixXiaomi()
        {}
        public void FixLenovo()
        { }
        public void FixSamsung()
        { }
        public void FixOnePlus()
        { }
    }
}
