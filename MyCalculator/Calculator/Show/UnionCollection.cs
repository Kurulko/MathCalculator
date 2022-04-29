using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Show.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCalculator.Calculator.Show
{
    public static class UnionCollection
    {
        public static List<FigureAndOperationCollection> GetFigureAndOperationCollection(List<Operation> operations
            , List<Figure> figures)
        {
            List<FigureAndOperationCollection> collection = figures.Select(f => new FigureAndOperationCollection
            { Index = f.Index, Value = f.Value < 0? $"({f.Value})" : $"{f.Value}" })
                .Union(operations.Select(o =>
                {
                    if (o.IsTwoExpression)
                        return new FigureAndOperationCollection
                        { Index = o.Index, Value = EnumPartToString.WithHtml(o.Part, o.Expression, o.SecondExpression) };
                    else
                        return new FigureAndOperationCollection
                        { Index = o.Index, Value = EnumPartToString.WithHtml(o.Part, o.Expression) };
                }))
                .OrderBy(ft => ft.Index).ToList();
            return collection;
        }
        public static string ShowExpression(List<Operation> operations
            , List<Figure> figures)
        {
            string result = string.Empty;
            var collection = GetFigureAndOperationCollection(operations, figures);
            foreach (var item in collection)
                result += item.Value + " ";
            return result;
        }
    }
}
