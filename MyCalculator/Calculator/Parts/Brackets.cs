using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace MyCalculator.Calculator.Parts
{
    public static class Brackets
    {
        static bool IsEqualNumberOfBrackets(ref string str)
        {
            bool result = false;
            int leftBracketsCount = 0, rightBracketsCount = 0;
            bool isFindLeftBracket = false;
            int indexOfFirstLeftBracket = 0;
            for (int i = 0; i < str.Length; ++i)
            {
                char c = str[i];
                if (c == '(')
                {
                    ++leftBracketsCount;
                    if (!isFindLeftBracket)
                    {
                        indexOfFirstLeftBracket = i;//индекс с какого будет начинатся строка
                        isFindLeftBracket = true;
                    }
                }
                else if (c == ')') ++rightBracketsCount;

                if (leftBracketsCount == rightBracketsCount && leftBracketsCount != 0)
                {
                    str = str.Substring(indexOfFirstLeftBracket, i + 1);
                    result = true;
                    break;
                }
            }
            return result;
        }
        static bool IsEqualNumberOfBracketsRightToLeft(ref string str)
        {
            bool result = false;
            int leftBracketsCount = 0, rightBracketsCount = 0;
            bool isFindRightBracket = false;
            int indexOfLastRigthBracket = 0;
            for (int i = str.Length - 1; i >= 0; --i)
            {
                char c = str[i];
                if (c == ')')
                {
                    ++rightBracketsCount;
                    if (!isFindRightBracket)
                    {
                        indexOfLastRigthBracket = i;//индекс с какого будет заканчиватся строка
                        isFindRightBracket = true;
                    }
                }
                else if (c == '(') ++leftBracketsCount;

                if (leftBracketsCount == rightBracketsCount && rightBracketsCount != 0)
                {
                    str = str.Substring(i,indexOfLastRigthBracket-i+1);
                    result = true;
                    break;
                }
            }
            return result;
        }
        public static bool IsBrackets(ref string input, bool rightToLeft = false)
        {
            Regex regex = new Regex($@"(\()(.+)(\))");
            bool result = false;
            MatchCollection collection = regex.Matches(input);
            foreach (Match match in collection)
            {
                string str = match.Value.ToString();
                result = rightToLeft ? IsEqualNumberOfBracketsRightToLeft(ref str)
                    : IsEqualNumberOfBrackets(ref str);
                if (result)
                {
                    input = str;
                    break;
                }
            }
            return result;
        }
    }
}
