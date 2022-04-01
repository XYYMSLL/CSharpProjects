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

    class Controller
    {
        private Player _newPlayer;
        private Map _newMap;


        public Controller(params int[] dimensions)
        {
            _newPlayer = new Player();
            _newMap = new Map(dimensions);
        }

        public void SetPlayerPosition()
        {
            int row, line;
            string inputRow, inputLine;
            int[] position = new int[2];

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
                    if (row <= 0 || row >= _newMap.MapLength - 1)
                    {
                        Console.WriteLine("请输入正确的行");
                        continue;
                    }

                    position[0] = row;
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
                    if (line <= 0 || line >= _newMap.MapWidth - 1)
                    {
                        Console.WriteLine("请输入正确的列");
                        continue;
                    }

                    position[1] = line;
                    break;
                }
            }

            //check availability
            if (_newMap.SetPlayerPosition(position))
            {
                _newPlayer.Position = position;
            }
            else
            {
                goto SetPosition;
            }
            #endregion
        }

        public void MovePlayer(ConsoleKeyInfo keyInfo)
        {
            int[] _newPos = new int[_newPlayer.Position.Length];
            for (int i = 0; i < _newPos.Length; i++)
            {
                _newPos[i] = _newPlayer.Position[i];
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

            if (_newMap.MovePlayer(_newPlayer.Position, _newPos))
            {
                _newPlayer.Position[0] = _newPos[0];
                _newPlayer.Position[1] = _newPos[1];
            }
        }
    }

    class Player
    {
        public int[] Position { set; get; } = new int[] { 0, 0 };
    }

    public class Map
    {
        private MazeElement[,] _Map;

        public int MapLength { get => _Map.GetLength(0); }
        public int MapWidth { get => _Map.GetLength(1); }

        public Map(params int[] parameters)
        {
            if (parameters.Length == 0)
            {
                GenerateMaze(12, 12);
            }
            else if (parameters.Length == 1)
            {
                int length = Math.Max(parameters[0], 12);
                GenerateMaze(length, length);
            }
            else
            {
                int x = Math.Max(parameters[0], 12);
                int y = Math.Max(parameters[1], 12);
                GenerateMaze(x, y);
            }
        }

        //private MazeElement GetTile(int x, int y)
        //{
        //    if (x >= _Map.GetLength(0) || y > _Map.GetLength(1))
        //    {
        //        return MazeElement.voidTile;
        //    }
        //    return _Map[x, y];
        //}

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

            PrintMaze();
        }

        private void PrintMaze()
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

        public bool SetPlayerPosition(int[] position)
        {
            if (CheckOverBoundry(position))
            {
                _Map[position[0], position[1]] = MazeElement.player;
                Console.Clear();
                PrintMaze();

                return true;
            }
            else
            {
                return false;
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

        private void RedrawTile(int[] position, MazeElement tileType)
        {
            Console.SetCursorPosition(position[1], position[0]);

            if (tileType == MazeElement.wall)
            {
                Console.Write("■");
            }
            else if (tileType == MazeElement.player)
            {
                Console.Write("♀");
            }
            else
            {
                Console.Write(" ");
            }

            Console.SetCursorPosition(0, _Map.GetLength(0));
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, _Map.GetLength(0));
        }

        public bool MovePlayer(int[] oldPosition, int[] newPosition)
        {
            if (CheckOverBoundry(newPosition))
            {
                _Map[oldPosition[0], oldPosition[1]] = MazeElement.floor;
                RedrawTile(oldPosition, MazeElement.floor);
                _Map[newPosition[0], newPosition[1]] = MazeElement.player;
                RedrawTile(newPosition, MazeElement.player);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
