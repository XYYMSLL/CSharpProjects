using System;
using System.Linq;

namespace Day2
{
    class MainClass
    {
        private static void BubbleSort(ref int[] nums)
        {
            int shorten = 0;
            while (true)
            {
                for (int i = 0; i < nums.Length - shorten; i++)
                {
                    if (i < nums.Length - 1)
                    {
                        if (nums[i] > nums[i + 1])
                        {
                            nums[i] = nums[i] ^ nums[i + 1];
                            nums[i + 1] = nums[i] ^ nums[i + 1];
                            nums[i] = nums[i] ^ nums[i + 1];
                        }
                    }
                }
                if (shorten >= nums.Length)
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

        /// <summary>
        /// Main loop of the demo
        /// </summary>
        /// <param name="maze">Map object</param>
        private static void MainLoop(Map maze)
        {
            while (true)
            {
                maze.printMaze();
                ConsoleKeyInfo info = Console.ReadKey();

                maze.movePlayer(info);
            }
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

            Map newMap = new Map();
            newMap.printMaze();

            newMap.SetPlayerPosition();

            MainLoop(newMap);
        }
    }
}
