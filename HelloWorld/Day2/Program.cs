using System;
using System.Linq;

namespace Day2
{
    class MainClass
    {
        private static void BubbleSort(ref int[] nums)
        {
            bool moved;
            int shorten = 0;
            while (true)
            {
                moved = false;
                for (int i = 0; i < nums.Length - shorten; i++)
                {
                    if (i < nums.Length - 1)
                    {
                        if (nums[i] > nums[i + 1])
                        {
                            nums[i] = nums[i] ^ nums[i + 1];
                            nums[i + 1] = nums[i] ^ nums[i + 1];
                            nums[i] = nums[i] ^ nums[i + 1];
                            moved = true;
                        }
                    }
                }
                if (!moved)
                {
                    break;
                }
                shorten++;
            }
        }

        private static void PrintSecondOrderArray(in int[,] nums)
        {
            for (int i = 0; i < nums.GetLength(0); i++)
            {
                for (int j = 0; j < nums.GetLength(1); j++)
                {
                    Console.WriteLine(nums[i, j]);
                }
            }
        }

        private static void PrintMaze(in int[,] maze)
        {
            Console.Clear();
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else if (maze[i, j] == 2)
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

        private static void GenerateMaze(ref int[,] maze)
        {
            int block = 0;
            Random random = new Random();
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == 0 || i == maze.GetLength(0) - 1 || j == 0 || j == maze.GetLength(1) - 1)
                    {
                        maze[i, j] = 1;
                    }
                    else if (block <= 30 && random.Next(0, 10) >= 7)
                    {
                        maze[i, j] = 1;
                        block++;
                    }
                }
            }
        }

        private static bool CheckOverBoundry(int[] checkPos, int[,] maze)
        {
            if (checkPos[0] <= 0 || checkPos[0] >= maze.GetLength(0) - 1 || checkPos[1] <= 0 || checkPos[1] >= maze.GetLength(1) - 1)
            {
                return false;
            }
            else if (maze[checkPos[0], checkPos[1]] == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void MainGame(int[] position, int[,] maze) {
            int[] newPos = new int[position.Length];
            for (int i = 0; i < position.Length; i++) {
                newPos[i] = position[i];
            }
            PrintMaze(in maze);
            ConsoleKeyInfo info = Console.ReadKey();

            switch (info.Key)
            {
                case ConsoleKey.W:
                    // move up
                    newPos[0] -= 1;
                    break;
                case ConsoleKey.A:
                    // move left
                    newPos[1] -= 1;
                    break;
                case ConsoleKey.S:
                    // move down
                    newPos[0] += 1;
                    break;
                case ConsoleKey.D:
                    // move right
                    newPos[1] += 1;
                    break;
                default:
                    break;
            }

            if (CheckOverBoundry(newPos, maze))
            {
                maze[position[0], position[1]] = 0;
                position = newPos;
                maze[position[0], position[1]] = 2;
            }

            MainGame(position, maze);
        }

        public static void Main(string[] args)
        {
            #region find odd element
            //int[] nums = { 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4 };

            //int index = 0;
            //int element = nums[index];
            //int lastIndex;

            //Array.Sort(nums);

            //while (true)
            //{
            //    lastIndex = Array.LastIndexOf(nums, element);
            //    if ((lastIndex - index + 1) % 2 == 1)
            //    {
            //        Console.WriteLine(element);
            //        return;
            //    }

            //    index = lastIndex + 1;
            //    if (index == nums.Length)
            //    {
            //        return;
            //    }

            //    element = nums[index];
            //}

            #endregion

            #region sort and print
            //int[] nums = { 3, 5, 4, 23, 4, 5, 647, 4, 234, 231, 4532, 6453, 763, 4523, 245, 6 };
            //BubbleSort(ref nums);
            //foreach (var num in nums)
            //{
            //    Console.Write(num + " ");
            //}

            //Console.WriteLine();

            //int[,] numss = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            //PrintSecondOrderArray(in numss);
            #endregion

            int[,] maze = new int[12, 12];

            GenerateMaze(ref maze);

            PrintMaze(in maze);

            int[] position = { -1, -1 };
            int row, line;
            string inputRow, inputLine;

            while (true)
            {
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
                        if (row <= 0 || row >= maze.GetLength(0) - 1)
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
                        if (line <= 0 || line >= maze.GetLength(0) - 1)
                        {
                            Console.WriteLine("请输入正确的列");
                            continue;
                        }

                        position[1] = line;
                        break;
                    }
                }

                //check availability
                if (CheckOverBoundry(position, maze))
                {
                    maze[position[0], position[1]] = 2;
                    break;
                }
            }


            MainGame(position, maze);
        }
    }
}
