using System;
namespace BoardGame
{
    public class Control
    {
        RingListBoard _myBoard;
        int item;
        Random random;
        bool _isWin;

        public Control()
        {
            _myBoard = new RingListBoard();
            item = 0;
            random = new Random();
            _isWin = false;
        }

        public void MainGame()
        {
            while (!_isWin)
            {
                ConsoleKeyInfo info = Console.ReadKey();

                int step = random.Next(1, 7);

                if (_myBoard.Move(step))
                {
                    item++;
                }

                _myBoard.PrintBoard();
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("前进{0}格", step);
                Console.WriteLine("现有道具{0}个", item);

                if (item >= 5)
                {
                    _isWin = true;
                    Console.WriteLine("游戏胜利");
                }
            }
        }
    }
}
