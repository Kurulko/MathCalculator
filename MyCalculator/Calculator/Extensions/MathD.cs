using System;

namespace MyCalculator.Calculator.Extensions
{
    public static class MathD
    {
        public const decimal E = 2.7182818284590451m;
        public const decimal PI = 3.1415926535897931m;

        static decimal CheckForDecimal(double value)
        {
            if (value > (double)Decimal.MaxValue)
            {
                string maxValueStr = "8E+28";
                throw new Exception($"This value is greater than {maxValueStr}");
            }
            else if (value < (double)Decimal.MinValue)
            {
                string minValueStr = "-8E+28";
                throw new Exception($"This value is less than {minValueStr}");
            }
            else
                return (decimal)value;
        }

        public static decimal Cos(decimal value)
            => CheckForDecimal(Math.Cos((double)value));
        public static decimal Sin(decimal value)
            => CheckForDecimal(Math.Sin((double)value));
        public static decimal Arcsin(decimal value)
            => CheckForDecimal(Math.Asin((double)value));
        public static decimal Arccos(decimal value)
            => CheckForDecimal(Math.Acos((double)value));
        public static decimal Tg(decimal value)
            => CheckForDecimal(Math.Tan((double)value));
        public static decimal Ctg(decimal value)
            => CheckForDecimal(1.0 / Math.Tan((double)value));
        public static decimal Arctg(decimal value)
            => CheckForDecimal(Math.Atan((double)value));
        public static decimal Arcctg(decimal value)
            => CheckForDecimal((1.0 / Math.Atan((double)value)));

        public static decimal Ch(decimal value)
            => CheckForDecimal(Math.Cosh((double)value));
        public static decimal Sh(decimal value)
            => CheckForDecimal(Math.Sinh((double)value));
        public static decimal Th(decimal value)
            => CheckForDecimal(Math.Tanh((double)value));
        public static decimal Cth(decimal value)
            => CheckForDecimal((1.0 / Math.Tanh((double)value)));

        public static decimal Arch(decimal value)
            => CheckForDecimal(Math.Acosh((double)value));
        public static decimal Arsh(decimal value)
            => CheckForDecimal(Math.Asinh((double)value));
        public static decimal Arth(decimal value)
            => CheckForDecimal(Math.Atanh((double)value));
        public static decimal Arcth(decimal value)
            => CheckForDecimal((1.0 / Math.Atanh((double)value)));

        public static decimal Extent(decimal value, decimal secondValue)
            => CheckForDecimal(Math.Pow((double)value, (double)secondValue));
        public static decimal Exp(decimal value)
            => Extent(E, value);
        public static decimal Log(decimal newBase, decimal value)
            => CheckForDecimal(Math.Log((double)value, (double)newBase));
        public static int Factorial(int value)
        {
            int result = 1;
            while (value > 0)
            {
                result *= value;
                --value;
            }
            return result;
        }
    }
}
