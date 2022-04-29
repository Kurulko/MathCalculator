using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Extensions;
using System.Text.RegularExpressions;

namespace MyCalculator.Calculator.Parts
{
    public class IsFigure : IsExpression<Figure>
    {
        public IsFigure(string expression) : base(expression) { }
        public void Is()
        {
            InputStringExpression = OutputStringExpression = OutputStringExpression.ReplaceEWithFigure();
            Regex regex = new Regex(@"\d+\,?\d*", RegexOptions.IgnoreCase);
            if (regex.IsMatch(InputStringExpression))
            {
                foreach (Match match in regex.Matches(InputStringExpression))
                {
                    uint index = (uint)match.Index;
                    decimal value = decimal.Parse(match.Value);
                    Figure figure = new Figure { Index = index, Value = value, Part = Part.Figure };
                    Expressions.Add(figure);
                }
                OutputStringExpression = regex.Replace(OutputStringExpression, m => new string('$', m.Length));
            }

        }
    }
}
