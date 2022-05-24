using System;

namespace LargeNumberAdd
{
    class MainClass
    {
        static bool CheckIntegral(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;

                }
            }

            return true;
        }


        public static void Main(string[] args)
        {
        Input:
            Console.WriteLine("请输入第一个数");
            string num1Str = Console.ReadLine();
            Console.WriteLine("请输入第二个数");
            string num2Str = Console.ReadLine();

            if (!CheckIntegral(num1Str) || !CheckIntegral(num2Str))
            {
                Console.Clear();
                Console.WriteLine("请输入正确的数字");
                goto Input;
            }

            Console.WriteLine("\n\n结果为\n");

            Console.WriteLine(GetLargeResultStr(num1Str, num2Str));
        }

        private static string GetLargeResultStr(string num1Str, string num2Str)
        {
            char[] arr1 = num1Str.ToCharArray();
            char[] arr2 = num2Str.ToCharArray();

            string result = "";

            Array.Reverse(arr1);
            Array.Reverse(arr2);

            int increament = 0;
            int temp = 0;

            for (int i = 0; i < Math.Max(arr1.Length, arr2.Length); i++)
            {
                if (i >= arr1.Length)
                {
                    temp = (int.Parse(arr2[i].ToString()) + increament);
                    result =  temp % 10 + result;
                    increament = temp / 10;
                }
                else if (i >= arr2.Length)
                {
                    temp = (int.Parse(arr1[i].ToString()) + increament);
                    result = temp % 10 + result;
                    increament = temp / 10;
                }
                else
                {
                    temp = int.Parse(arr1[i].ToString()) + int.Parse(arr2[i].ToString()) + increament;
                    increament = temp / 10;
                    result = (temp % 10) + result;
                }
            }

            if (increament != 0)
            {
                result = '1' + result;
            }

            return result;
        }
    }
}
