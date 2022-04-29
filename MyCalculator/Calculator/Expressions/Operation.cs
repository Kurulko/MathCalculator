using MyCalculator.Calculator.Enums;
using System;
using System.Collections.Generic;

namespace MyCalculator.Calculator.Expressions
{
    public class Operation : Expression
    {
        public Priority Priority { get; set; }
        public bool IsTwoExpression { get; set; }
        public string FullStr { get; set; }
        public string Expression { get; set; }
        public string SecondExpression { get; set; }
    }
}
