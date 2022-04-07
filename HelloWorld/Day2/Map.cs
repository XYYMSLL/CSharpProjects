using System;

namespace Day2
{
    public enum MazeElement
    {
        floor,
        wall,
        player,
        monster,
        voidTile,
        woodenSword,
        steelSword,
        mithrilSword
    }

    class Controller
    {
        private Player _newPlayer;
        private Monster _newMonster;
        private Map _newMap;

        private int[] _monsterPos = new int[2];

        public Controller(params int[] dimensions)
        {
            _newPlayer = new Player(100, "Player1", 6, 5);
            _newMonster = new Monster(50, "小龙虾", 12, 3);
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

        public void SetMonsterPosition()
        {
            _monsterPos = _newMap.MonsterPos;
        }

        public bool MovePlayer(ConsoleKeyInfo keyInfo)
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
                    return false;
            }

            MazeElement nextTile = _newMap.MovePlayer(_newPlayer.Position, _newPos);

            if (nextTile != MazeElement.wall && nextTile != MazeElement.monster && nextTile != MazeElement.voidTile)
            {
                _newPlayer.Position[0] = _newPos[0];
                _newPlayer.Position[1] = _newPos[1];

                Console.SetCursorPosition(0, _newMap.MapLength + 1);
                if (nextTile == MazeElement.woodenSword)
                {
                    WoodenSword woodenSword = new WoodenSword();
                    _newPlayer.SetWeapon(woodenSword);
                }
                if (nextTile == MazeElement.steelSword)
                {
                    SteelSword steelSword = new SteelSword();
                    _newPlayer.SetWeapon(steelSword);
                }
                if (nextTile == MazeElement.mithrilSword)
                {
                    MithrilSword mithrilSword = new MithrilSword();
                    _newPlayer.SetWeapon(mithrilSword);
                }

                return false;
            }
            else
            {
                if (nextTile == MazeElement.monster)
                {
                    Console.SetCursorPosition(0, _newMap.MapLength + 1);
                    while (_newPlayer.HP > 0 && _newMonster.HP > 0)
                    {
                        _newPlayer.RandomMove(_newMonster);
                        if (_newMonster.HP <= 0)
                        {
                            Console.WriteLine("{0} Win", _newPlayer.Name);
                            break;
                        }
                        _newMonster.RandomMove(_newPlayer);
                        if (_newPlayer.HP <= 0)
                        {
                            Console.WriteLine("{0} Win", _newMonster.Name);
                        }
                    }
                    return true;
                }

                return false;
            }
        }
    }

    public class Map
    {
        private MazeElement[,] _Map;

        public int[] MonsterPos = new int[2];

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

        private void SetWeapon()
        {
            Random random = new Random();
            while (true)
            {
                int rRow = random.Next(1, MapLength - 1);
                int rCol = random.Next(1, MapWidth - 1);

                if (_Map[rRow, rCol] == MazeElement.floor)
                {
                    _Map[rRow, rCol] = MazeElement.woodenSword;
                    break;
                }
            }
            while (true)
            {
                int rRow = random.Next(1, MapLength - 1);
                int rCol = random.Next(1, MapWidth - 1);

                if (_Map[rRow, rCol] == MazeElement.floor)
                {
                    _Map[rRow, rCol] = MazeElement.steelSword;
                    break;
                }
            }
            while (true)
            {
                int rRow = random.Next(1, MapLength - 1);
                int rCol = random.Next(1, MapWidth - 1);

                if (_Map[rRow, rCol] == MazeElement.floor)
                {
                    _Map[rRow, rCol] = MazeElement.mithrilSword;
                    break;
                }
            }
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

            SetWeapon();
            SetMonsterPos();
            PrintMaze();
        }

        private void SetMonsterPos()
        {
            int rRow, rCol;
            Random random = new Random();
            while (true)
            {
                rRow = random.Next(1, MapLength);
                rCol = random.Next(1, MapWidth);

                if (_Map[rRow, rCol] == MazeElement.floor)
                {
                    _Map[rRow, rCol] = MazeElement.monster;
                    break;
                }
            }

            MonsterPos[0] = rRow;
            MonsterPos[1] = rCol;
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
                    else if (_Map[i, j] == MazeElement.woodenSword)
                    {
                        Console.Write("W");
                    }
                    else if (_Map[i, j] == MazeElement.steelSword)
                    {
                        Console.Write("S");
                    }
                    else if (_Map[i, j] == MazeElement.mithrilSword)
                    {
                        Console.Write("M");
                    }
                    else if (_Map[i, j] == MazeElement.monster)
                    {
                        Console.Write("X");
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

        public MazeElement MovePlayer(int[] oldPosition, int[] newPosition)
        {
            if (CheckOverBoundry(newPosition))
            {
                switch (_Map[newPosition[0], newPosition[1]])
                {
                    case MazeElement.floor:
                        _Map[oldPosition[0], oldPosition[1]] = MazeElement.floor;
                        RedrawTile(oldPosition, MazeElement.floor);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.player;
                        RedrawTile(newPosition, MazeElement.player);

                        return MazeElement.floor;
                    case MazeElement.monster:
                        return MazeElement.monster;
                    case MazeElement.woodenSword:
                        _Map[oldPosition[0], oldPosition[1]] = MazeElement.floor;
                        RedrawTile(oldPosition, MazeElement.floor);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.player;
                        RedrawTile(newPosition, MazeElement.player);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.floor;
                        return MazeElement.woodenSword;
                    case MazeElement.steelSword:
                        _Map[oldPosition[0], oldPosition[1]] = MazeElement.floor;
                        RedrawTile(oldPosition, MazeElement.floor);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.player;
                        RedrawTile(newPosition, MazeElement.player);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.floor;
                        return MazeElement.steelSword;
                    case MazeElement.mithrilSword:
                        _Map[oldPosition[0], oldPosition[1]] = MazeElement.floor;
                        RedrawTile(oldPosition, MazeElement.floor);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.player;
                        RedrawTile(newPosition, MazeElement.player);
                        _Map[newPosition[0], newPosition[1]] = MazeElement.floor;
                        return MazeElement.mithrilSword;
                }
                return MazeElement.voidTile;
            }
            else
            {
                return MazeElement.wall;
            }
        }
    }
}
