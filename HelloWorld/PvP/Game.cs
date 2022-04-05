using System;
namespace PvP
{
    public class Game
    {
        Player _player1;
        Player _player2;


        public Game()
        {
            _player1 = new Player("Player1");
            _player2 = new Player("Player2");
        }

        public void MainLoop()
        {
            Random random = new Random();
            while (true)
            {
                _player1.RandomMove(_player2, random);
                if (_player2.HP <= 0)
                {
                    Console.WriteLine("{0}赢了", _player1.Name);
                    break;
                }
                _player2.RandomMove(_player1, random);
                if (_player1.HP <= 0)
                {
                    Console.WriteLine("{0}赢了", _player2.Name);
                    break;
                }
            }
        }
    }
}
