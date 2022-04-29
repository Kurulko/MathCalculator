using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Extensions;
using MyCalculator.Calculator.Show;
using MyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyCalculator.Calculator.Calculate
{
    public class CalculateOperations
    {
        public List<Error> Errors { get; set; }
        public List<Warning> Warnings { get; set; }
        public CalculateOperations(List<Error> errors, List<Warning> warnings)
        {
            Errors = errors;
            Warnings = warnings;
        }


        bool IsFigure(string str, out decimal value)
        {
            bool isFigure = decimal.TryParse(str, out value);
            return isFigure;
        }
        decimal NotFigure(string expression)
        {
            FigureAndOperation fo = new FigureAndOperation(expression);
            Errors.AddRange(fo.ExpressionAndSolution.Errors);
            Warnings.AddRange(fo.ExpressionAndSolution.Warnings);
            return fo.ExpressionAndSolution.Answer;
        }

        decimal ResultWithChecked(Func<decimal, decimal, decimal> math, decimal firstValue, decimal secondValue,
            string fullExpression, ref bool isOkay)
        {
            decimal result = 0m;
            try
            {
                result = math(firstValue, secondValue);
            }
            catch (Exception ex)
            {
                Warnings.Add(new Warning { Message = ex.Message,Expression = fullExpression});
                isOkay = false;
            }
            return result;
        }
        decimal ResultWithChecked(Func<decimal, decimal> math, decimal value,
            string fullExpression, ref bool isOkay)
        {
            decimal result = 0m;
            try
            {
                result = math(value);
            }
            catch (Exception ex)
            {
                Warnings.Add(new Warning { Message = ex.Message, Expression = fullExpression });
                isOkay = false;
            }
            return result;
        }


        public decimal Trigonometry(string expression, Part part, ref bool isOkay, bool isDegree = false)
        {
            isOkay = true;
            decimal result = 0;

            decimal divider = 1;
            int length = expression.Length;

            if (isDegree)
                divider = 180m / MathD.PI;
            else if (expression.IsDegree())
            {
                isDegree = true;
                expression = expression.Substring(0, length - 1);
                divider = 180m / MathD.PI;
            }

            string fullExpression = EnumPartToString.WithoutHtml(part, expression);

            if (IsFigure(expression, out decimal value))
            {
                switch (part)
                {
                    case Part.Cos:
                        result = ResultWithChecked(MathD.Cos,value / divider,fullExpression, ref isOkay);
                        break;
                    case Part.Sin:
                        result = ResultWithChecked(MathD.Sin, value / divider, fullExpression, ref isOkay); ;
                        break;
                    case Part.Arccos:
                        if (Math.Abs(value) <= 1)
                            result = ResultWithChecked(MathD.Arccos, value / divider, fullExpression, ref isOkay);
                        else
                        {
                            Errors.Add(new Error { Message = $"The value is not between -1 and 1",Expression = $"arccos({expression})" });
                            isOkay = false;
                        }
                        break;
                    case Part.Arcsin:
                        if (Math.Abs(value) <= 1)
                            result = ResultWithChecked(MathD.Arcsin, value / divider, fullExpression, ref isOkay);
                        else
                        {
                            Errors.Add(new Error { Message = $"The value is not between -1 and 1", Expression = $"arcsin({expression})" });
                            isOkay = false;
                        }
                        break;
                    case Part.Tg:
                        result = ResultWithChecked(MathD.Tg, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Ctg:
                        result = ResultWithChecked(MathD.Ctg, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arctg:
                        result = ResultWithChecked(MathD.Arctg, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arcctg:
                        result = ResultWithChecked(MathD.Arcctg, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Sh:
                        result = ResultWithChecked(MathD.Sh, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Ch:
                        result = ResultWithChecked(MathD.Ch, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Th:
                        result = ResultWithChecked(MathD.Th, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Cth:
                        result = ResultWithChecked(MathD.Cth, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arsh:
                        result = ResultWithChecked(MathD.Arsh, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arch:
                        result = ResultWithChecked(MathD.Arch, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arth:
                        result = ResultWithChecked(MathD.Arth, value / divider, fullExpression, ref isOkay);
                        break;
                    case Part.Arcth:
                        result = ResultWithChecked(MathD.Arcth, value / divider, fullExpression, ref isOkay);
                        break;
                }
            }
            else
            {
                expression = NotFigure(expression).ToString();
                result = Trigonometry(expression, part, ref isOkay, isDegree);
            }
            return result;
        }

        public decimal Factorial(string expression, ref bool isOkay)
        {
            isOkay = true;
            decimal result = 0;
            if (IsFigure(expression, out decimal value))
            {
                if (expression.IsMoreThenZeroAndInt())
                {
                    int intValue = Convert.ToInt32(value);
                    result = MathD.Factorial(intValue);
                }
                else
                {
                    Errors.Add(new Error { Message = $"The value is not integer or less than zero", Expression = $"({expression})!" });
                    isOkay = false;
                }
            }
            else
            {
                expression = NotFigure(expression).ToString();
                result = Factorial(expression, ref isOkay);
            }
            return result;
        }
        public decimal Extent(string expression, string secondExpression, ref bool isOkay)
        {
            isOkay = true;
            decimal result;
            if (IsFigure(expression, out decimal value) && IsFigure(secondExpression, out decimal secondValue))
            {
                string fullExpression = EnumPartToString.WithoutHtml(Part.Extent,expression,secondExpression);
                result = ResultWithChecked(MathD.Extent,value, secondValue, fullExpression,ref isOkay);
            }
            else if (IsFigure(expression, out decimal _value))
            {
                secondExpression = NotFigure(secondExpression).ToString();
                result = Extent(expression, secondExpression, ref isOkay);
            }
            else if (IsFigure(secondExpression, out decimal _secondValue))
            {
                expression = NotFigure(expression).ToString();
                result = Extent(expression, secondExpression, ref isOkay);
            }
            else
            {
                expression = NotFigure(expression).ToString();
                secondExpression = NotFigure(secondExpression).ToString();
                result = Extent(expression, secondExpression, ref isOkay);
            }
            return result;
        }
        public decimal Exp(string expression, ref bool isOkay)
        => Extent(Math.E.ToString(), expression, ref isOkay);

        public decimal Sqrt(string expression, ref bool isOkay)
        {
            isOkay = true;
            decimal result = 0;
            decimal sqrt = 0.5m;
            if (IsFigure(expression, out decimal value))
            {
                if (value >= 0)
                {
                    string fullExpression = EnumPartToString.WithoutHtml(Part.Sqrt, expression);
                    result = ResultWithChecked(MathD.Extent, value, sqrt, fullExpression, ref isOkay);
                }
                else
                {
                    Errors.Add(new Error { Message = $"The value is less than zero", Expression = $"&radic;({expression})" });
                    isOkay = false;
                }
            }
            else
            {
                expression = NotFigure(expression).ToString();
                result = Sqrt(expression, ref isOkay);
            }
            return result;
        }

        public decimal Log(string expression, string secondExpression, ref bool isOkay)
        {
            isOkay = true;
            decimal result = 0m;
            if (IsFigure(expression, out decimal value) && IsFigure(secondExpression, out decimal secondValue))
            {
                if (value > 0 && secondValue > 0)
                {
                    string fullExpression = EnumPartToString.WithoutHtml(Part.Log, expression, secondExpression);
                    result = ResultWithChecked(MathD.Log, value, secondValue, fullExpression, ref isOkay);
                }
                else
                {
                    Errors.Add(new Error { Message = $"The value is less than zero", 
                        Expression = EnumPartToString.WithHtml(Part.Log, expression, secondExpression) });
                    isOkay = false;
                }
            }
            else if (IsFigure(expression, out decimal _value))
            {
                if (_value > 0)
                {
                    secondExpression = NotFigure(secondExpression).ToString();
                    result = Log(expression, secondExpression, ref isOkay);
                }
                else
                {
                    Errors.Add(new Error
                    {
                        Message = $"The value is less than zero",
                        Expression = EnumPartToString.WithHtml(Part.Log, expression, secondExpression)
                    });
                    isOkay = false;
                }
            }
            else if (IsFigure(secondExpression, out decimal _secondValue))
            {
                if (_secondValue > 0)
                {
                    expression = NotFigure(expression).ToString();
                    result = Log(expression, secondExpression, ref isOkay);
                }
                else
                {
                    Errors.Add(new Error
                    {
                        Message = $"The value is less than zero",
                        Expression = EnumPartToString.WithHtml(Part.Log, expression, secondExpression)
                    });
                    isOkay = false;
                }
            }
            else
            {
                expression = NotFigure(expression).ToString();
                secondExpression = NotFigure(secondExpression).ToString();
                result = Extent(expression, secondExpression, ref isOkay);
            }
            return result;
        }
        public decimal Ln(string expression, ref bool isOkay)
            => Log(Math.E.ToString(), expression, ref isOkay);

        public decimal Brackets(string expression, ref bool isOkay)
        {
            isOkay = true;
            decimal result = 0;
            if (IsFigure(expression, out decimal value))
                result = value;
            else
            {
                expression = NotFigure(expression).ToString();
                result = Brackets(expression, ref isOkay);
            }
            return result;
        }
        public decimal Abs(string expression, ref bool isOkay)
        {
            isOkay = true;
            decimal result = 0;
            if (IsFigure(expression, out decimal value))
                 result = Math.Abs(value);
            else
            {
                expression = NotFigure(expression).ToString();
                result = Abs(expression, ref isOkay);
            }
            return result;
        }
    }
}
