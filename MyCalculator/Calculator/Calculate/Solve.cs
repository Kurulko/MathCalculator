using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Show;
using MyCalculator.Models;

namespace MyCalculator.Calculator.Calculate
{
    public class Solve
    {
        List<Operation> _operations;
        List<Figure> _figures;
        public List<string> ResultHtml { get; private set; } = new();
        public List<Error> Errors { get; set; } = new();
        public List<Warning> Warnings { get; set; } = new();
        public bool IsThereErrors { get; private set; }

        public Solve(List<Operation> operations, List<Figure> figures)
        {
            _operations = operations;
            _figures = figures;
        }

        bool IsValid()
            => Errors.Count == 0 && Warnings.Count == 0;

        void DifficultOperations(CalculateOperations calculateOperations)
        {
            Operation[] copyOperations = new Operation[_operations.Count];
            _operations.CopyTo(copyOperations);
            foreach (var operation in copyOperations)
            {
                decimal nowResult = 0;
                bool isOkay = true;
                switch (operation.Part)
                {
                    case Part.Cos:
                    case Part.Sin:
                    case Part.Arccos:
                    case Part.Arcsin:
                    case Part.Tg:
                    case Part.Ctg:
                    case Part.Arctg:
                    case Part.Sh:
                    case Part.Ch:
                    case Part.Th:
                    case Part.Cth:
                    case Part.Arsh:
                    case Part.Arch:
                    case Part.Arth:
                    case Part.Arcth:
                        nowResult = calculateOperations.Trigonometry(operation.Expression, operation.Part, ref isOkay);
                        break;
                    case Part.Factorial:
                        nowResult = calculateOperations.Factorial(operation.Expression, ref isOkay);
                        break;
                    case Part.Extent:
                        nowResult = calculateOperations.Extent(operation.Expression, operation.SecondExpression, ref isOkay);
                        break;
                    case Part.Brackets:
                        nowResult = calculateOperations.Brackets(operation.Expression, ref isOkay);
                        break;
                    case Part.Exp:
                        nowResult = calculateOperations.Exp(operation.Expression, ref isOkay);
                        break;
                    case Part.Sqrt:
                        nowResult = calculateOperations.Sqrt(operation.Expression, ref isOkay);
                        break;
                    case Part.Abs:
                        nowResult = calculateOperations.Abs(operation.Expression, ref isOkay);
                        break;
                    case Part.Log:
                        nowResult = calculateOperations.Log(operation.Expression,operation.SecondExpression, ref isOkay);
                        break;
                    case Part.Ln:
                        nowResult = calculateOperations.Ln(operation.Expression, ref isOkay);
                        break;
                }

                Errors.AddRange(calculateOperations.Errors);
                Warnings.AddRange(calculateOperations.Warnings);

                if (isOkay && IsValid())
                {
                    if (operation.Part != Part.Minus && operation.Part != Part.Plus
                        && operation.Part != Part.Multiply && operation.Part != Part.Divide 
                        && operation.Part != Part.Modulo)
                    {
                        _operations.Remove(operation);
                        _figures.Add(new Figure { Index = operation.Index, Part = Part.Figure, Value = nowResult });
                        ResultHtml.Add($"<hr>{UnionCollection.ShowExpression(_operations, _figures)}<hr>");
                    }
                }
                else
                {
                    IsThereErrors = true;
                    break;
                }
            }
        }
        decimal SimpleOperations()
        {
            while (_operations.Count != 0 && _figures.Count != 1)
            {
                var expressions = _operations.Select(o => new { Index = o.Index, Part = o.Part, Value = (decimal)0.0, Priority = o.Priority })
                    .Union(_figures.Select(f => new { Index = f.Index, Part = f.Part, Value = f.Value, Priority = (Priority)5 }))
                    .OrderBy(e => e.Index).ToList();

                Priority maxPriority = expressions.Min(e => e.Priority);
                for (int i = 1; i < expressions.Count; ++i)
                {
                    if (expressions[i].Priority == maxPriority
                        && i != expressions.Count - 1
                            && expressions[i - 1].Part == Part.Figure
                            && expressions[i + 1].Part == Part.Figure)
                    {
                        decimal firstValue = expressions[i - 1].Value;
                        decimal secondValue = expressions[i + 1].Value;
                        decimal nowResult = 0;
                        switch (expressions[i].Part)
                        {
                            case Part.Plus:
                                nowResult = firstValue + secondValue;
                                break;
                            case Part.Minus:
                                nowResult = firstValue - secondValue;
                                break;
                            case Part.Multiply:
                                nowResult = firstValue * secondValue;
                                break;
                            case Part.Divide:
                                if (secondValue == 0)
                                {
                                    IsThereErrors = true;
                                    Errors.Add(new Error { Message = "Second value can't be equal 0 in expression " +
                                        $"{firstValue}/0"});
                                    return default(decimal);
                                }
                                else
                                    nowResult = firstValue / secondValue;
                                break;
                            case Part.Modulo:
                                if (secondValue == 0)
                                {
                                    IsThereErrors = true;
                                    Errors.Add(new Error
                                    {
                                        Message = "Second value can't be equal 0 in expression " +
                                        $"{firstValue}%0"
                                    });
                                    return default(decimal);
                                }
                                else
                                    nowResult = firstValue % secondValue;
                                break;
                        }
                        _operations.Remove(_operations.FirstOrDefault(o => o.Index == expressions[i].Index));
                        _figures.Remove(_figures.FirstOrDefault(f => f.Index == expressions[i - 1].Index));
                        _figures.Remove(_figures.FirstOrDefault(f => f.Index == expressions[i + 1].Index));
                        _figures.Add(new Figure { Index = expressions[i - 1].Index, Value = nowResult, Part = Part.Figure });
                        ResultHtml.Add($"<hr>{UnionCollection.ShowExpression(_operations, _figures)}<hr>");
                        break;
                    }
                }
            }
            return _figures.FirstOrDefault().Value;
        }

        public decimal Result(CalculateOperations calculateOperations)
        {
            decimal result = 0;

            DifficultOperations(calculateOperations);

            if (!IsThereErrors)
            {
                //ResultHtml += UnionCollection.ShowExpression(_operations, _figures);

                if (_operations.Select(o => o.Part == Part.Plus || o.Part == Part.Minus ||
                  o.Part == Part.Multiply || o.Part == Part.Divide || o.Part == Part.Modulo).Count() == _operations.Count)
                {
                    result += SimpleOperations();
                }
                else if (_operations.Count == 0 && _figures.Count == 1)
                {
                    result += _figures.FirstOrDefault().Value;
                }
            }

            return Math.Round(result, 5);
        }
    }
}
