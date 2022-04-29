using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Models;

namespace MyCalculator.Calculator.Check
{
    public class CheckExpression
    {
        public List<Error> Errors { get; set; } = new();
        public List<Warning> Warnings { get; set; } = new();
        public string Expression { get; set; }
        public CheckExpression(string expression)
            => Expression = expression;
        public bool IsValid()
        {
            bool result = true;

            Func<bool>[] funcs = { IsEqualNumberOfBrackets, IsMathematicalExpression };
            foreach (var func in funcs)
                if (!func())
                    result = false;

            return result;
        }
        bool IsEqualNumberOfBrackets()
        {
            int countOfLeftBrackets = Expression.Where(e => e == '(').Count();
            int countOfRightBrackets = Expression.Where(e => e == ')').Count();
            bool result = countOfLeftBrackets == countOfRightBrackets;
            if (!result)
                Errors.Add(new Error { Message = "Count of left brackets != count of right brackets", Expression = Expression });
            return result;
        }
        bool IsMathematicalExpression()
        {
            string withoutMathExprStr = Expression;

            List<string> expressions = new List<string>
                {@"\|",@"\^",@"\!",@"\,",@"\.",@"\*",@"\-",@"\+",@"\/",@"\%",@"\(",@"\)",@"\d"
                ,"arcsin","acosh","asinh","arccos","arcctg","arctg","asin","acos","atanh","sinh","cosh","tanh","arsh","atan","arcth"
                ,"arch","arth","exp","e","pi","log","sin","cos","ctg","tg","tan","exp","sqrt","abs","ln","sh","ch","cth","th" };
            foreach (string expression in expressions)
                ReplaceStringEmpty(expression, ref withoutMathExprStr);

            bool result = withoutMathExprStr == string.Empty;
            if (!result)
                Errors.Add(new Error { Message = "It is not a mathematical expression", Expression = Expression });
            return result;   
        }
        string ReplaceStringEmpty(string regexStr, ref string withoutMathExprStr)
        {
            Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase);

            if (regex.IsMatch(withoutMathExprStr))
                foreach (Match match in regex.Matches(withoutMathExprStr))
                    withoutMathExprStr = withoutMathExprStr.Replace(match.Value, "");

            return withoutMathExprStr;
        }
    }
}
