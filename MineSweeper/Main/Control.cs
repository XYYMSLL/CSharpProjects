using System;
namespace Main
{
    public class Control
    {
        private Grid _grid;
        private int row, col;

        public Control()
        {
        }

        public void StartGame()
        {
            int select = 0;
            string selectStr = "";
            while (true)
            {
                Console.WriteLine("请选择难度：\n1.简单 9*9，地雷数：10\n2.普通 16*16，地雷数：40\n3.专家 16*30，地雷数：99\n4.自定义");
                selectStr = Console.ReadLine();
                if (!int.TryParse(selectStr, out select))
                {
                    Console.WriteLine("请输入正确的数字");
                    continue;
                }
                else
                {
                    switch (select)
                    {
                        case 1:
                            _grid = new Grid(10, 9);
                            row = 9;
                            col = 9;
                            return;
                        case 2:
                            _grid = new Grid(40, 16);
                            row = 16;
                            col = 16;
                            return;
                        case 3:
                            _grid = new Grid(99, 16, 30);
                            row = 16;
                            col = 30;
                            return;
                        case 4:
                            break;
                    }
                    break;
                }
            }

            while (true)
            {
                string rowStr, colStr, countStr;
                int count;
                Console.WriteLine("请输入行数");
                rowStr = Console.ReadLine();
                if (!int.TryParse(rowStr, out row))
                {
                    Console.WriteLine("请输入正确的数字");
                    continue;
                }

                Console.WriteLine("请输入列数");
                colStr = Console.ReadLine();
                if (!int.TryParse(colStr, out col))
                {
                    Console.WriteLine("请输入正确的数字");
                    continue;
                }

                Console.WriteLine("请输地雷数");
                countStr = Console.ReadLine();
                if (!int.TryParse(countStr, out count))
                {
                    Console.WriteLine("请输入正确的数字");
                    continue;
                }
                else
                {
                    if (count <= 0 || count > row * col)
                    {
                        Console.WriteLine("请输入正确的地雷数");
                        continue;
                    }
                }

                return;
            }
        }

        public void MainLoop()
        {
            int cTop = 1;
            int cLeft = 1;
            bool result = true;
            Console.SetCursorPosition(cLeft, cTop);
            //Console.WriteLine("WASD或方向键控制光标移动，J踩方块，K标记方块");
            while (result)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                switch (info.Key)
                {
                    case ConsoleKey.J:
                        result = _grid.Reveal(Console.CursorTop, Console.CursorLeft);
                        break;
                    case ConsoleKey.K:
                        _grid.Marking(Console.CursorTop, Console.CursorLeft);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        cLeft = cLeft - 1 <= 0 ? 1 : cLeft - 1;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        cTop = cTop + 1 > row ? row : cTop + 1;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        cLeft = cLeft + 1 > col ? col : cLeft + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        cTop = cTop - 1 <= 0 ? 1 : cTop - 1;
                        break;
                }


                Console.SetCursorPosition(cLeft, cTop);
            }

            return;
        }
    }
}
