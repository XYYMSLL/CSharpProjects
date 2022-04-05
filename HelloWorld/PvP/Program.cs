using System;

namespace PvP
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Game newGame = new Game();

            newGame.MainLoop();
        }
    }
}
