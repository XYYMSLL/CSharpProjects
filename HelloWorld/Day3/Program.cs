using System;

namespace Day3
{
    class MainClass
    {

        #region Add
        private static float Add() {
            return 0.0f;
        }
        private static float Add(int num)
        {
            return num;
        }
        private static float Add(int num1, int num2)
        {
            return num1 + num2;
        }
        private static float Add(int num1, float num2)
        {
            return num1 + num2;
        }
        private static float Add(float num1, int num2)
        {
            return num1 + num2;
        }
        private static float Add(float num1, float num2)
        {
            return num1 + num2;
        }
        private static float Add(double num1, double num2)
        {
            return (float)(num1 + num2);
        }
        #endregion

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
