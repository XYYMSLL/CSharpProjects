using System;

namespace Day2
{
    enum MazeElement
    {
        floor,
        wall,
        player,
        voidTile
    }

    public class Map
    {
        private MazeElement[,] _Map;
        private int[] _PlayerPos = new int[] { 0, 0 };

        public Map()
        {
            GenerateMaze(12, 12);
        }
        public Map(int x, int y)
        {
            x = Math.Max(x, 12);
            y = Math.Max(y, 12);
            GenerateMaze(x, y);
        }

        public MazeElement GetTile(int x, int y)
        {
            if (x >= _Map.GetLength(0) || y > _Map.GetLength(1))
            {
                return MazeElement.voidTile;
            }
            return _Map[x, y];
        }

        private void GenerateMaze(int x, int y)
        {
            _Map = new MazeElement[x, y];

            int block = 0;
            Random random = new Random();
            for (int i = 0; i < _Map.GetLength(0); i++)
            {
                for (int j = 0; j < _Map.GetLength(1); j++)
                {
                    if (i == 0 || i == _Map.GetLength(0) - 1 || j == 0 || j == _Map.GetLength(1) - 1)
                    {
                        _Map[i, j] = MazeElement.wall;
                    }
                    else if (block <= 30 && random.Next(0, 10) >= 7)
                    {
                        _Map[i, j] = MazeElement.wall;
                        block++;
                    }
                }
            }
        }

        public void printMaze()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _Map.GetLength(0); i++)
            {
                for (int j = 0; j < _Map.GetLength(1); j++)
                {
                    if (_Map[i, j] == MazeElement.wall)
                    {
                        Console.Write("■");
                    }
                    else if (_Map[i, j] == MazeElement.player)
                    {
                        Console.Write("♀");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    //Console.Write(maze[i, j]);
                }

                Console.WriteLine();
            }
        }

        private bool CheckOverBoundry(int[] checkPos)
        {
            if (checkPos[0] <= 0 || checkPos[0] >= _Map.GetLength(0) - 1 || checkPos[1] <= 0 || checkPos[1] >= _Map.GetLength(1) - 1)
            {
                return false;
            }
            else
            {
                return _Map[checkPos[0], checkPos[1]] != MazeElement.wall;
            }
        }

        public void SetPlayerPosition()
        {
            int row, line;
            string inputRow, inputLine;

        #region set initial position
        SetPosition:
            //get x value
            while (true)
            {
                Console.Write("请输入行：");
                inputRow = Console.ReadLine();
                if (!int.TryParse(inputRow, out row))
                {
                    Console.WriteLine("请输入正确的行");
                    continue;
                }
                else
                {
                    if (row <= 0 || row >= _Map.GetLength(0) - 1)
                    {
                        Console.WriteLine("请输入正确的行");
                        continue;
                    }

                    _PlayerPos[0] = row;
                    break;
                }
            }

            //get y value
            while (true)
            {
                Console.Write("请输入列：");
                inputLine = Console.ReadLine();
                if (!int.TryParse(inputLine, out line))
                {
                    Console.WriteLine("请输入正确的列");
                    continue;
                }
                else
                {
                    if (line <= 0 || line >= _Map.GetLength(0) - 1)
                    {
                        Console.WriteLine("请输入正确的列");
                        continue;
                    }

                    _PlayerPos[1] = line;
                    break;
                }
            }

            //check availability
            if (CheckOverBoundry(_PlayerPos))
            {
                _Map[_PlayerPos[0], _PlayerPos[1]] = MazeElement.player;
                Console.Clear();
            }
            else
            {
                goto SetPosition;
            }
            #endregion
        }

        public void movePlayer(ConsoleKeyInfo keyInfo)
        {
            int[] _newPos = new int[_PlayerPos.Length];
            for (int i = 0; i < _newPos.Length; i++)
            {
                _newPos[i] = _PlayerPos[i];
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    _newPos[0] -= 1;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    _newPos[0] += 1;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    _newPos[1] -= 1;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    _newPos[1] += 1;
                    break;
                default:
                    return;
            }



            if (CheckOverBoundry(_newPos))
            {
                _Map[_PlayerPos[0], _PlayerPos[1]] = MazeElement.floor;
                _PlayerPos = _newPos;
                _Map[_PlayerPos[0], _PlayerPos[1]] = MazeElement.player;
            }
        }
    }
}
