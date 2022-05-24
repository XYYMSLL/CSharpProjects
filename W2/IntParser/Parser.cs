using System;
using System.Collections.Generic;

namespace IntParser
{
    static public class Parser
    {
        static public bool Parse(string inputStr, out double result)
        {
            if (inputStr.Length == 0)
            {
                ThrowError("Empty string");
            }

            Queue<int> intQueue = new Queue<int>();
            bool negated = false;
            bool dotted = false;

            for (int i = 0; i < inputStr.Length; i++)
            {
                if (!Char.IsDigit(inputStr[i]))
                {
                    if (inputStr[i] == '-' && negated == false)
                    {
                        negated = true;
                    }
                    else
                    {
                        result = default;
                        return false;
                    }

                    if (inputStr[i] == '.' && dotted == false)
                    {
                        dotted = true;
                    }
                    else
                    {
                        result = default;
                        return false;
                    }
                }

                intQueue.Enqueue(inputStr[i]);
            }

            string resultStr = intQueue.ToString();
            result = int.Parse(resultStr);
            if (negated)
            {
                result *= -1;
            }

            return true;
        }

        static private void ThrowError(string msg)
        {
            throw new Exception(msg);
        }
    }
}
