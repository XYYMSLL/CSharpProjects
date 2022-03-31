using System;

namespace PushBox
{

    enum MapTiles
    {
        voidFloor,
        floor,
        wall,
        goal,
        player,
        box
    }

    enum Direction
    {
        up,
        down,
        left,
        right
    }

    class MainClass
    {

        private static void GenerateMap(out MapTiles[,] map, out int[,] goals)
        {
            map = new MapTiles[,] {
                { MapTiles.voidFloor, MapTiles.wall, MapTiles.wall, MapTiles.wall, MapTiles.wall, MapTiles.voidFloor, MapTiles.voidFloor},
                { MapTiles.wall, MapTiles.goal, MapTiles.floor, MapTiles.player, MapTiles.floor, MapTiles.wall, MapTiles.voidFloor},
                { MapTiles.wall, MapTiles.box, MapTiles.floor, MapTiles.floor, MapTiles.box, MapTiles.goal, MapTiles.wall},
                { MapTiles.wall, MapTiles.floor, MapTiles.floor,MapTiles.floor,MapTiles.floor,MapTiles.floor, MapTiles.wall},
                { MapTiles.voidFloor, MapTiles.wall, MapTiles.floor, MapTiles.box, MapTiles.floor, MapTiles.wall, MapTiles.voidFloor},
                { MapTiles.voidFloor, MapTiles.wall, MapTiles.floor, MapTiles.goal, MapTiles.floor, MapTiles.wall, MapTiles.voidFloor},
                { MapTiles.voidFloor, MapTiles.wall, MapTiles.wall, MapTiles.wall, MapTiles.wall, MapTiles.voidFloor, MapTiles.voidFloor}
            };

            goals = new int[,] {
                {1, 1 },
                {2, 5 },
                {5, 3 }
            };
        }

        private static void PrintMap(in MapTiles[,] map, int[,] goals)
        {
            // clear screen
            Console.Clear();

            // reset goal tile
            for (int i = 0; i < goals.GetLength(0); i++)
            {
                if (map[goals[i, 0], goals[i, 1]] == MapTiles.floor)
                {
                    map[goals[i, 0], goals[i, 1]] = MapTiles.goal;
                }
            }

            // main print loop
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == MapTiles.voidFloor || map[i, j] == MapTiles.floor)
                    {
                        Console.Write(" ");
                    }
                    else if (map[i, j] == MapTiles.player)
                    {
                        Console.Write("♂");
                    }
                    else if (map[i, j] == MapTiles.box)
                    {
                        Console.Write("●");
                    }
                    else if (map[i, j] == MapTiles.goal)
                    {
                        Console.Write("◎");
                    }
                    else
                    {
                        Console.Write("■");
                    }
                }

                Console.WriteLine();
            }
        }

        private static bool CheckOverBoundry(int[] checkPos, MapTiles[,] map)
        {
            if (checkPos[0] <= 0 || checkPos[0] >= map.GetLength(0) - 1 || checkPos[1] <= 0 || checkPos[1] >= map.GetLength(1) - 1)
            {
                return false;
            }
            else if (map[checkPos[0], checkPos[1]] == MapTiles.wall)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool CheckWin(MapTiles[,] map, int[,] goals)
        {
            for (int i = 0; i < goals.GetLength(0); i++)
            {
                if (map[goals[i, 0], goals[i, 1]] != MapTiles.box)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryMove(int[] pos, Direction dir, MapTiles[,] map)
        {
            int x = pos[0];
            int y = pos[1];
            switch (dir)
            {
                case Direction.up:
                    x--;
                    break;
                case Direction.down:
                    x++;
                    break;
                case Direction.left:
                    y--;
                    break;
                case Direction.right:
                    y++;
                    break;
            }

            if (CheckOverBoundry(new int[] { x, y }, map))
            {
                if (map[x, y] == MapTiles.box)
                {
                    return TryMove(new int[] { x, y }, dir, map);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private static void MovePlyer(ref MapTiles[,] map, ref int[] pos, Direction dir)
        {
            if (TryMove(pos, dir, map))
            {
                int[] newPos = new int[2];
                newPos[0] = pos[0];
                newPos[1] = pos[1];
                switch (dir)
                {
                    case Direction.up:
                        newPos[0]--;
                        break;
                    case Direction.down:
                        newPos[0]++;
                        break;
                    case Direction.left:
                        newPos[1]--;
                        break;
                    case Direction.right:
                        newPos[1]++;
                        break;
                }

                if (map[newPos[0], newPos[1]] == MapTiles.box)
                {
                    MoveBox(ref newPos, dir, ref map);
                }

                map[pos[0], pos[1]] = MapTiles.floor;
                pos = newPos;
                map[newPos[0], newPos[1]] = MapTiles.player;
            }
        }

        private static void MoveBox(ref int[] pos, Direction dir, ref MapTiles[,] map)
        {
            int[] newPos = new int[2];
            newPos[0] = pos[0];
            newPos[1] = pos[1];
            switch (dir)
            {
                case Direction.up:
                    newPos[0]--;
                    break;
                case Direction.down:
                    newPos[0]++;
                    break;
                case Direction.left:
                    newPos[1]--;
                    break;
                case Direction.right:
                    newPos[1]++;
                    break;
            }

            if (map[newPos[0], newPos[1]] == MapTiles.box)
            {
                MoveBox(ref newPos, dir, ref map);
            }


            map[pos[0], pos[1]] = MapTiles.floor;
            map[newPos[0], newPos[1]] = MapTiles.box;
        }

        public static void Main(string[] args)
        {
            MapTiles[,] map;
            int[,] goals;
            int[] playerPos = { 1, 3 };

            GenerateMap(out map, out goals);

            MainGame(ref map, goals, ref playerPos);
        }

        private static void MainGame(ref MapTiles[,] map, int[,] goals, ref int[] playerPos)
        {
            while (true)
            {
                PrintMap(map, goals);

                if (CheckWin(map, goals))
                {
                    Console.WriteLine("You Win");
                    break;
                }

                Direction dir;

                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        // move up
                        dir = Direction.up;
                        MovePlyer(ref map, ref playerPos, dir);
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        // move left
                        dir = Direction.left;
                        MovePlyer(ref map, ref playerPos, dir);
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        // move down
                        dir = Direction.down;
                        MovePlyer(ref map, ref playerPos, dir);
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        // move right
                        dir = Direction.right;
                        MovePlyer(ref map, ref playerPos, dir);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
