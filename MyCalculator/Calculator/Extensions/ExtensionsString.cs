using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyCalculator.Calculator.Extensions
{
    public static class ExtensionsString
    {
        public static string ReplaceOneTime(this string str, string oldValue, string newValue)
        {
            string result = str;
            if (str.Contains(oldValue))
            {
                int index = str.IndexOf(oldValue);
                int lastIndex = index + oldValue.Length;
                result = str.Substring(0, index) + newValue + str.Substring(lastIndex, str.Length - lastIndex);
            }
            return result;
        }
        public static bool IsMoreThenZeroAndInt(this string value)
        {
            bool result = false;
            Regex regex = new Regex(@"(\d)+(\,0)*");
            if (regex.IsMatch(value))
            {
                var item = regex.Matches(value).FirstOrDefault(c => c.Value == value);
                if (item != null)
                    result = true;
            }
            return result;
        }
        public static string ReplaceCommaWithDot(this string str)
            => str.Replace(".", ",");

        public static string RemoveSpaces(this string str)
            => str.Replace(" ", "");

        public static string ReplacePiWithFigure(this string str)
            => str.Replace("pi", Math.PI.ToString(), true, null);

        public static string ReplaceEWithFigure(this string str)
            => str.Replace("e", Math.E.ToString(), true, null);

        public static bool IsDegree(this string str)
            => str[str.Length - 1] == '*';
    }
}
