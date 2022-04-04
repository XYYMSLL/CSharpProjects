using System;

namespace Main
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Control GameControl = new Control();

            GameControl.StartGame();
            GameControl.MainLoop();

            return;
        }
    }
}
