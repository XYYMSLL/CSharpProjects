using System;
namespace Main
{
    public class Grid
    {
        private TileStruct[,] MyGrid;
        private int MineCount;
        private int[,] MineArr;

        public Grid(int mineCount, params int[] dimensions)
        {
            MineCount = mineCount;
            MineArr = new int[MineCount, 2];

            if (dimensions.Length == 0)
            {
                GenerateGrid(9);
            }
            else if (dimensions.Length == 1)
            {
                GenerateGrid(Math.Max(dimensions[0], 9));
            }
            else
            {
                GenerateGrid(Math.Max(dimensions[0], 9), Math.Max(dimensions[1], 9));
            }
        }

        private void GenerateGrid(params int[] dimensions)
        {
            int row, col;
            if (dimensions.Length == 1)
            {
                row = dimensions[0] + 2;
                col = row;
            }
            else
            {
                row = dimensions[0] + 2;
                col = dimensions[1] + 2;
            }

            MyGrid = new TileStruct[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    MyGrid[i, j].MyStatus = TileStatus.Covered;
                    if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                    {
                        MyGrid[i, j].MyStatus = TileStatus.Revealed;
                        MyGrid[i, j].MyTileID = TileID.Wall;
                    }
                    else
                    {
                        MyGrid[i, j].MyTileID = TileID.Safe;
                    }
                }
            }

            SetMines();
            SetNums();

            PrintGrid();
        }

        private void SetNums()
        {
            for (int i = 1; i < MyGrid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < MyGrid.GetLength(1) - 1; j++)
                {
                    MyGrid[i, j].MyNum = GetNum(i, j);
                }
            }
        }

        private int GetNum(int i, int j)
        {
            int result = 0;
            if (CheckOverBoundry(i - 1, j - 1))
            {
                if (MyGrid[i - 1, j - 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i - 1, j))
            {
                if (MyGrid[i - 1, j].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i - 1, j + 1))
            {
                if (MyGrid[i - 1, j + 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i, j + 1))
            {
                if (MyGrid[i, j + 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i + 1, j + 1))
            {
                if (MyGrid[i + 1, j + 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i + 1, j))
            {
                if (MyGrid[i + 1, j].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i + 1, j - 1))
            {
                if (MyGrid[i + 1, j - 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }
            if (CheckOverBoundry(i, j - 1))
            {
                if (MyGrid[i, j - 1].MyTileID == TileID.Mine)
                {
                    result++;
                }
            }

            return result;
        }

        private bool CheckOverBoundry(int x, int y)
        {
            if (x <= 0 || x >= MyGrid.GetLength(0) - 1 || y <= 0 || y >= MyGrid.GetLength(1) - 1)
            {
                return false;
            }

            return true;
        }

        private void SetMines()
        {
            int iterator = 0;
            Random random = new Random();
            while (iterator < MineCount)
            {
                int x = random.Next(1, MyGrid.GetLength(0) - 1);
                int y = random.Next(1, MyGrid.GetLength(1) - 1);

                if (MyGrid[x, y].MyTileID == TileID.Mine)
                {
                    continue;
                }
                else
                {
                    MyGrid[x, y].MyTileID = TileID.Mine;
                    MineArr[iterator, 0] = x;
                    MineArr[iterator, 1] = y;
                    iterator++;
                }
            }
        }

        private bool CheckWin()
        {
            int revealed = 0;
            for (int i = 0; i < MyGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MyGrid.GetLength(1); j++)
                {
                    if (MyGrid[i, j].MyStatus == TileStatus.Revealed && MyGrid[i, j].MyTileID == TileID.Safe && MyGrid[i, j].MyTileID != TileID.Wall)
                    {
                        revealed++;
                    }
                }
            }

            if (revealed == (MyGrid.GetLength(0) - 2) * (MyGrid.GetLength(1) - 2) - MineCount)
            {
                Console.SetCursorPosition(0, MyGrid.GetLength(0) + 1);
                Console.WriteLine("You win");
                return true;
            }
            return false;
        }

        private void GameOver()
        {
            foreach (int i in MineArr)
            {
                MyGrid[MineArr[i, 0], MineArr[i, 1]].MyStatus = TileStatus.Revealed;
            }

            Console.Clear();
            PrintGrid();
            Console.SetCursorPosition(0, MyGrid.GetLength(0) + 1);
            Console.WriteLine("You lose");
        }

        private void PrintGrid()
        {
            Console.Clear();
            for (int i = 0; i < MyGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MyGrid.GetLength(1); j++)
                {
                    if (MyGrid[i, j].MyTileID == TileID.Wall)
                    {
                        Console.Write("○");
                    }
                    else if (MyGrid[i, j].MyStatus == TileStatus.Covered)
                    {
                        Console.Write("■");
                    }
                    else if (MyGrid[i, j].MyStatus == TileStatus.Marked)
                    {
                        Console.Write("▶");
                    }
                    else if (MyGrid[i, j].MyStatus == TileStatus.Revealed && MyGrid[i, j].MyTileID == TileID.Mine)
                    {
                        Console.Write("●");
                    }
                    else
                    {
                        switch (MyGrid[i, j].MyNum)
                        {
                            case 0:
                                Console.Write("□");
                                break;
                            case 1:
                                Console.Write("①");
                                break;
                            case 2:
                                Console.Write("②");
                                break;
                            case 3:
                                Console.Write("③");
                                break;
                            case 4:
                                Console.Write("④");
                                break;
                            case 5:
                                Console.Write("⑤");
                                break;
                            case 6:
                                Console.Write("⑥");
                                break;
                            case 7:
                                Console.Write("⑦");
                                break;
                            case 8:
                                Console.Write("⑧");
                                break;
                        }
                    }
                }

                Console.WriteLine("");
            }
        }

        public bool Reveal(int x, int y)
        {
            if (!CheckOverBoundry(x, y))
            {
                return true;
            }
            if (MyGrid[x, y].MyStatus == TileStatus.Covered)
            {
                MyGrid[x, y].MyStatus = TileStatus.Revealed;
                PrintGrid();
                if (MyGrid[x, y].MyTileID == TileID.Mine)
                {
                    GameOver();
                    return false;
                }
                else if (MyGrid[x, y].MyNum == 0)
                {
                    RevealAround(x, y);
                    PrintGrid();
                    if (CheckWin())
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    PrintGrid();
                    if (CheckWin())
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void RevealAround(int i, int j)
        {
            if (CheckOverBoundry(i - 1, j - 1))
            {
                Reveal(i - 1, j - 1);
            }
            if (CheckOverBoundry(i - 1, j))
            {
                Reveal(i - 1, j);
            }
            if (CheckOverBoundry(i - 1, j + 1))
            {
                Reveal(i - 1, j + 1);
            }
            if (CheckOverBoundry(i, j + 1))
            {
                Reveal(i, j + 1);
            }
            if (CheckOverBoundry(i + 1, j + 1))
            {
                Reveal(i + 1, j + 1);
            }
            if (CheckOverBoundry(i + 1, j))
            {
                Reveal(i + 1, j);
            }
            if (CheckOverBoundry(i + 1, j - 1))
            {
                Reveal(i + 1, j - 1);
            }
            if (CheckOverBoundry(i, j - 1))
            {
                Reveal(i, j - 1);
            }
        }

        public void Marking(int x, int y)
        {
            if (!CheckOverBoundry(x, y))
            {
                return;
            }
            if (MyGrid[x, y].MyStatus == TileStatus.Marked)
            {
                MyGrid[x, y].MyStatus = TileStatus.Covered;
            }
            else if (MyGrid[x, y].MyStatus == TileStatus.Covered)
            {
                MyGrid[x, y].MyStatus = TileStatus.Marked;
            }
            PrintGrid();
        }
    }
}
