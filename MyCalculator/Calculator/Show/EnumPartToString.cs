using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Extensions;

namespace MyCalculator.Calculator.Show
{
    public static class EnumPartToString
    {
        public static string WithoutHtml(Part part, string value)
        {
            string res = "";
            switch (part)
            {
                case Part.Cos:
                    res += $"cos({value})";
                    break;
                case Part.Sin:
                    res += $"sin({value})";
                    break;
                case Part.Arccos:
                    res += $"arccos({value})";
                    break;
                case Part.Arcsin:
                    res += $"arcsin({value})";
                    break;
                case Part.Tg:
                    res += $"tg({value})";
                    break;
                case Part.Ctg:
                    res += $"ctg({value})";
                    break;
                case Part.Arctg:
                    res += $"arctg({value})";
                    break;
                case Part.Arcctg:
                    res += $"arcctg({value})";
                    break;
                case Part.Factorial:
                    res += $"({value})!";
                    break;
                case Part.Brackets:
                    res += $"({value})";
                    break;
                case Part.Exp:
                    res += $"exp({value})";
                    break;
                case Part.Sqrt:
                    res += $"sqrt({value})";
                    break;
                case Part.Abs:
                    res += $"|{value}|";
                    break;
                case Part.Sh:
                    res += $"sh({value})";
                    break;
                case Part.Ch:
                    res += $"ch({value})";
                    break;
                case Part.Th:
                    res += $"th({value})";
                    break;
                case Part.Cth:
                    res += $"cth({value})";
                    break;
                case Part.Arsh:
                    res += $"arsh({value})";
                    break;
                case Part.Arch:
                    res += $"arch({value})";
                    break;
                case Part.Arth:
                    res += $"arth({value})";
                    break;
                case Part.Arcth:
                    res += $"arcth({value})";
                    break;
                case Part.Ln:
                    res += $"ln({value})";
                    break;
                default:
                    res += $"(не то...)";
                    break;
            }
            return res;
        }
        public static string WithoutHtml(Part part, string firstValue, string secondValue)
        {
            string res = "";
            switch (part)
            {
                case Part.Plus:
                    res += $"{firstValue}+{secondValue}";
                    break;
                case Part.Minus:
                    res += $"{firstValue}-{secondValue}";
                    break;
                case Part.Multiply:
                    res += $"{firstValue}*{secondValue}";
                    break;
                case Part.Divide:
                    res += $"{firstValue}/{secondValue}";
                    break;
                case Part.Modulo:
                    res += $"{firstValue}%{secondValue}";
                    break;
                case Part.Extent:
                    res += $"({firstValue})^({secondValue})";
                    break;
                case Part.Log:
                    res += $"log{firstValue}({secondValue})";
                    break;
                default:
                    res += $"(не то...)";
                    break;
            }
            return res;
        }
        public static string WithHtml(Part part, string value)
        {
            string res = "";
            switch (part)
            {
                case Part.Cos:
                    res += "cos(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Sin:
                    res += "sin(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Arccos:
                    res += "arccos(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Arcsin:
                    res += "arcsin(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Tg:
                    res += "tg(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Ctg:
                    res += "ctg(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Arctg:
                    res += "arctg(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Arcctg:
                    res += "arcctg(" + (value.IsDegree() ? $"{value.Substring(0, value.Length - 1)}&deg" : $"{value} rad") + ")";
                    break;
                case Part.Factorial:
                    res += $"({value})!";
                    break;
                case Part.Brackets:
                    res += $"({value})";
                    break;
                case Part.Exp:
                    res += $"e<sup>{value}</sup>";
                    break;
                case Part.Sqrt:
                    res += $"&radic;({value})";
                    break;
                case Part.Abs:
                    res += $"|{value}|";
                    break;
                case Part.Sh:
                    res += $"sh({value})";
                    break;
                case Part.Ch:
                    res += $"ch({value})";
                    break;
                case Part.Th:
                    res += $"th({value})";
                    break;
                case Part.Cth:
                    res += $"cth({value})";
                    break;
                case Part.Arsh:
                    res += $"arsh({value})";
                    break;
                case Part.Arch:
                    res += $"arch({value})";
                    break;
                case Part.Arth:
                    res += $"arth({value})";
                    break;
                case Part.Arcth:
                    res += $"arcth({value})";
                    break;
                case Part.Ln:
                    res += $"ln({value})";
                    break;
                default:
                    res += $"(не то...)";
                    break;
            }
            return res;
        }
        public static string WithHtml(Part part, string firstValue, string secondValue)
        {
            string res = "";
            switch (part)
            {
                case Part.Plus:
                    res += $"{firstValue}+{secondValue}";
                    break;
                case Part.Minus:
                    res += $"{firstValue}-{secondValue}";
                    break;
                case Part.Multiply:
                    res += $"{firstValue}*{secondValue}";
                    break;
                case Part.Divide:
                    res += $"{firstValue}/{secondValue}";
                    break;
                case Part.Modulo:
                    res += $"{firstValue}%{secondValue}";
                    break;
                case Part.Extent:
                    res += $"({firstValue})<sup>{secondValue}</sup>";
                    break;
                case Part.Log:
                    res += $"log<sub>{firstValue}</sub>({secondValue})";
                    break;
                default:
                    res += $"(не то...)";
                    break;
            }
            return res;
        }
    }
}
