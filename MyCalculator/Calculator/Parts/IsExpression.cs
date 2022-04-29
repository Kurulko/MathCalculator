using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;

namespace MyCalculator.Calculator.Parts
{
    public class IsExpression<T> where T : Expression
    {
        public List<T> Expressions { get; set; } = new();

        public string InputStringExpression { get; set; }
        public string OutputStringExpression { get; /*protected*/ set; }
        public IsExpression(string expression)
            => OutputStringExpression = InputStringExpression = expression;
    }
}
