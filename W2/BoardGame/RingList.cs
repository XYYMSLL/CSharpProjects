using System;
namespace BoardGame
{
    public class RingListBoard
    {
        private Square _start;
        private Square _tail;
        private Square _player;
        
        public RingListBoard()
        {

            Random random = new Random();
            _start = new Square(random.Next(1, 11) < 4);
            _tail = _start;
            _tail.Next = _start;

            for (int i = 0; i < 29; i++)
            {
                bool item = random.Next(1, 11) < 7;
                Square newSquare = new Square(item);
                Add(newSquare);
            }

            _start.Player = true;
            _player = _start;
            _player.Item = false;

            PrintBoard();
        }

        private void Add(Square newSquare)
        {
            newSquare.Next = _start;
            _start = newSquare;
            _tail.Next = _start;
        }

        public void PrintBoard()
        {
            Console.Clear();

            Square printS = _start;

            Console.SetCursorPosition(1, 1);
            for (int i = 0; i < 10; i++)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                Console.Write("■");
                if (printS.Player)
                {
                    Console.SetCursorPosition(left, top - 1);
                    Console.Write("⭑");
                    Console.SetCursorPosition(left, top);
                }
                else if (printS.Item)
                {
                    Console.SetCursorPosition(left, top - 1);
                    Console.Write("○");
                    Console.SetCursorPosition(left, top);
                }

                printS = printS.Next;
                Console.SetCursorPosition(left + 1, top);
            }

            for (int j = 0; j < 5; j++)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                Console.Write("■");
                if (printS.Player)
                {
                    Console.SetCursorPosition(left + 1, top);
                    Console.Write("⭑");
                    Console.SetCursorPosition(left, top);
                }
                else if (printS.Item)
                {
                    Console.SetCursorPosition(left + 1, top);
                    Console.Write("○");
                    Console.SetCursorPosition(left, top);
                }

                printS = printS.Next;

                Console.SetCursorPosition(left, top + 1);
            }

            for (int k = 0; k < 10; k++)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                Console.Write("■");
                if (printS.Player)
                {
                    Console.SetCursorPosition(left, top + 1);
                    Console.Write("⭑");
                    Console.SetCursorPosition(left, top);
                }
                else if (printS.Item)
                {
                    Console.SetCursorPosition(left, top + 1);
                    Console.Write("○");
                    Console.SetCursorPosition(left, top);
                }

                printS = printS.Next;
                Console.SetCursorPosition(left - 1, top);
            }

            for (int x = 0; x < 5; x++)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                Console.Write("■");
                if (printS.Player)
                {
                    Console.SetCursorPosition(left - 1, top);
                    Console.Write("⭑");
                    Console.SetCursorPosition(left, top);
                }
                else if (printS.Item)
                {
                    Console.SetCursorPosition(left - 1, top);
                    Console.Write("○");
                    Console.SetCursorPosition(left, top);
                }

                printS = printS.Next;
                Console.SetCursorPosition(left, top - 1);
            }
        }

        public bool Move(int step)
        {
            Square dest = _player;
            dest.Player = false;

            for (int i = 0; i < step; i++)
            {
                dest = dest.Next;
            }

            dest.Player = true;
            _player = dest;

            if (dest.Item)
            {
                dest.Item = false;
                return true;
            }

            return false;
        }
    }
}
