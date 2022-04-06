using System;
namespace PvP
{
    public class Game
    {
        Player _player1;
        Player _player2;
        Monster _monster;


        public Game()
        {
            _player1 = new Player(100, "Player1", 10, 5);
            _player2 = new Player(100, "Player2", 10, 5);
            _monster = new Monster(30, "Murloc", 10, 3);
        }

        public void MainLoop()
        {
            while (true)
            {
                _player1.RandomMove(_monster);
                if (_monster.HP <= 0)
                {
                    Console.WriteLine("{0}赢了", _player1.Name);
                    break;
                }
                _monster.RandomMove(_player1);
                if (_player1.HP <= 0)
                {
                    Console.WriteLine("{0}赢了", _monster.Name);
                    break;
                }
            }
        }
    }
}
