using System;

namespace Day1HW
{
    class MainClass
    {

        #region calculater

        static bool CheckNumeric(string str)
        {
            bool doted = false;
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    if (c.Equals('.') && doted == false)
                    {
                        doted = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        static void Calculator()
        {
            Console.Write("输入第一个数：");
            string num1Str = Console.ReadLine();
            Console.Write("输入第二个数：");
            string num2Str = Console.ReadLine();

            if (!CheckNumeric(num1Str) || !CheckNumeric(num2Str))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            double num1 = double.Parse(num1Str);
            double num2 = double.Parse(num2Str);

            Console.Write("输入运算方法(+ - * /)：");
            string sign = Console.ReadLine();

            switch (sign)
            {
                case "+":
                    Console.WriteLine(num1 + num2);
                    break;
                case "-":
                    Console.WriteLine(num1 - num2);
                    break;
                case "*":
                    Console.WriteLine(num1 * num2);
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Infinity");
                    }
                    else
                    {
                        Console.WriteLine(num1 / num2);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid calculation method");
                    break;
            }
        }

        #endregion

        #region Login

        static byte Login(string correctUsername, string correctPassword)
        {
            string inputUsername;
            string inputPassword;
            Console.Write("请输入用户名：");
            inputUsername = Console.ReadLine();
            Console.Write("请输入密码：");
            inputPassword = Console.ReadLine();
            if (correctUsername == "")
            {
                Console.WriteLine("请先注册");
                return 2;
            }
            else if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(inputUsername))
            {
                Console.WriteLine("请输入账户名或密码");
                return 3;
            }
            else if (inputPassword != correctUsername || inputUsername != correctPassword)
            {
                Console.WriteLine("账户名或密码不正确");
                return 3;
            }

            return 1;
        }

        static bool Register(ref string username, ref string password)
        {

            string inputUsername;
            string inputPassword;
            string inputPassword2;
            Console.Write("请输入用户名：");
            inputUsername = Console.ReadLine();
            Console.Write("请输入密码：");
            inputPassword = Console.ReadLine();
            Console.Write("请确认密码：");
            inputPassword2 = Console.ReadLine();

            if (inputPassword == inputPassword2 && !string.IsNullOrEmpty(inputPassword))
            {
                Console.WriteLine("注册成功");
                username = inputUsername;
                password = inputPassword;
                return true;
            }
            else
            {
                Console.WriteLine("确认密码必须与输入的密码一致");
                return false;
            }
        }

        static void LoginSimulator()
        {
            string username = "";
            string password = "";
            string input;

            bool registed = false;

            byte result = 0;

            while (result != 1)
            {
                if (registed)
                {
                    input = "1";
                    Console.WriteLine("请登录");
                }
                else
                {
                    Console.Write("输入\n1.登录\n2.注册\n>");
                    input = Console.ReadLine();
                }

                if (input == "1")
                {
                    result = Login(username, password);
                }
                else if (input == "2")
                {
                    registed = Register(ref username, ref password);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            if (result == 1)
            {
                Console.WriteLine("登录成功");
                return;
            }
        }

        #endregion

        //public R Add<T, V, R>(T num1, V num2)
        //{
        //    R result;
        //    result = num1 + num2;

        //    return result;
        //}

        public static void Main(string[] args)
        {
            /**
             * 1.制作简单的计算器，提示输入第一个数，输入后提示输入第二个数，然后提示输入符号+ - * /
             * 2.完成一个简单的登录注册功能，提示输入1登录，输入2注册，注册时提示输入账号，输入密码，输入确认密码，缺人两次密码相同在提示注册成功，返回登陆界面，提示输入账号输入密码，正确提示登录成功，错误提示登录失败
             */
            //Console.WriteLine("输入1运行1计算器，输入2运行2登录");
            //string choice = Console.ReadLine();

            //if (int.Parse(choice) == 1)
            //{
            //    Calculator();
            //}
            //else if (int.Parse(choice) == 2)
            //{
            //    LoginSimulator();
            //}
            //else
            //{
            //    Console.WriteLine("Invalid input");
            //}

            //Console.WriteLine((typeof (Add<int, double, float>(2, 2.5))) + " " + Add<int, double, float>(2, 2.5));

            return;
        }
    }
}
