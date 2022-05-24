using System;

namespace NotationConverter
{
    class MainClass
    {

        const string codeList = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static void Main(string[] args)
        {
            int sourceNum;
        Input1:
            Console.WriteLine("请输入十进制数字：");
            string decNum = Console.ReadLine();

            if (!int.TryParse(decNum, out sourceNum))
            {
                Console.WriteLine("请输入正确的数字");
                goto Input1;
            }

            int targetNotation;
            string input;

        Input2:
            Console.WriteLine("请输入要转换的进制（2 - 35）");
            input = Console.ReadLine();
            if (!int.TryParse(input, out targetNotation))
            {
                Console.WriteLine("请输入正确的进制数");
                goto Input2;
            }

            if (targetNotation < 2 || targetNotation >= 36)
            {
                Console.WriteLine("请输入正确的进制数");
                goto Input2;
            }

            Console.WriteLine("\n\n结果为\n");

            if (targetNotation == 10)
            {
                Console.WriteLine(sourceNum);
                return;
            }

            Console.WriteLine(GetTargetNotation(sourceNum, targetNotation));
        }

        private static string GetTargetNotation(int sourceNum, int targetNotation)
        {
            int result = sourceNum % targetNotation;

            int next = sourceNum / targetNotation;

            if (next == 0)
            { return codeList[result].ToString(); }
            return GetTargetNotation(next, targetNotation) + codeList[result].ToString();
        }
    }
}
