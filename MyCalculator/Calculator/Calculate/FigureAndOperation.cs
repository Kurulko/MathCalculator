using MyCalculator.Calculator.Calculate.Models;
using MyCalculator.Calculator.Check;
using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Extensions;
using MyCalculator.Calculator.Parts;
using MyCalculator.Calculator.Show;
using MyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyCalculator.Calculator.Calculate
{
    public class FigureAndOperation
    {
        public IsFigure IsFigure { get; private set; }
        public IsOperation IsOperation { get; private set; }
        public ExpressionAndSolution ExpressionAndSolution { get; set; } = new();
        CheckExpression _checkExpression;

        List<Figure> figures;
        List<Operation> operations;
        bool IsErrors = false;
        public FigureAndOperation(string expression)
        {
            ExpressionAndSolution.ExpressionStr = expression;
            expression = expression.RemoveSpaces().ReplaceCommaWithDot();
            _checkExpression = new CheckExpression(expression);
            if (_checkExpression.IsValid())
            {
                string newExpression = expression.ReplacePiWithFigure();
                ReplaceEWithFigure(ref newExpression);

                IsOperation = new IsOperation(newExpression);
                Result();
                figures = IsFigure.Expressions.OrderBy(e => e.Index).ToList();
                operations = IsOperation.Expressions.OrderBy(e => e.Index).ToList();

                IsNotTwoCharactersInRow();
                if (!IsErrors)
                {
                    AddFigureLessThanZero();
                    AddMultiplyBetweenTwoFigures();
                    FillData();
                }
            }
            else
            {
                ExpressionAndSolution.Errors = _checkExpression.Errors;
                ExpressionAndSolution.Warnings = _checkExpression.Warnings;
            }
        }

        void ReplaceEWithFigure(ref string expression)
        {
            Regex regex = new Regex("e(?!xp)",RegexOptions.IgnoreCase);
            if(regex.IsMatch(expression))
            {
                var collection = regex.Matches(expression).OrderByDescending(m => m.Index);
                foreach (Match match in collection)
                {
                    int index = expression.IndexOf(match.Value, match.Index);
                    expression = expression.Remove(index, 1);
                    expression = expression.Insert(index, Math.E.ToString());
                }
            }
        }

        bool CanBeFigure(Part part)
            => part != Part.Multiply && part != Part.Minus &&
            part != Part.Plus && part != Part.Divide &&
            part != Part.Modulo && part != Part.Figure &&
            part != Part.Extent;
        List<Expression> GetExpressions()
            => operations.Select(o => new Expression { Index = o.Index, Part = o.Part })
                .Union(figures.Select(f => new Expression { Index = f.Index, Part = f.Part }))
                .OrderBy(e => e.Index).ToList();
        void AddMultiplyBetweenTwoFigures()
        {
            List<Expression> expressions = GetExpressions();
            int count = expressions.Count;
            for (int i = 0; i < count; i++)
            {
                if (i != count - 1)
                {
                    if ((CanBeFigure(expressions[i].Part) && CanBeFigure(expressions[i + 1].Part)) ||
                        (expressions[i].Part == Part.Figure && CanBeFigure(expressions[i + 1].Part)))
                    {
                        Operation nextOp = operations.FirstOrDefault(o => o.Index == expressions[i + 1].Index);
                        var operationsMoreNextIndex = operations.Where(o => o.Index >= nextOp.Index).ToList();
                        operations.RemoveAll(o => o.Index >= nextOp.Index);
                        operations.Add(new Operation { Index = nextOp.Index, Priority = (Priority)3, Part = Part.Multiply, IsTwoExpression = true });
                        operations.AddRange(operationsMoreNextIndex.Select(o =>
                        {
                            o.Index++;
                            return o;
                        }).ToList());

                        var figuresMoreNextIndex = figures.Where(f => f.Index >= nextOp.Index).ToList();
                        figures.RemoveAll(f => f.Index >= nextOp.Index);
                        figures.AddRange(figuresMoreNextIndex.Select(f =>
                        {
                            f.Index++;
                            return f;
                        }).ToList());


                        expressions = GetExpressions();
                        count = expressions.Count;
                        ++i;
                    }
                    else if (CanBeFigure(expressions[i].Part) && expressions[i + 1].Part == Part.Figure)
                    {
                        Figure nextFigure = figures.FirstOrDefault(f => f.Index == expressions[i + 1].Index);
                        var figuresMoreNextIndex = figures.Where(f => f.Index >= nextFigure.Index).ToList();
                        figures.RemoveAll(f => f.Index >= nextFigure.Index);
                        figures.AddRange(figuresMoreNextIndex.Select(f =>
                        {
                            f.Index++;
                            return f;
                        }).ToList());


                        Operation operation = operations.FirstOrDefault(o => o.Index == expressions[i].Index);
                        var operationsMoreNextIndex = operations.Where(o => o.Index > operation.Index).ToList();
                        operations.RemoveAll(o => o.Index >= operation.Index);
                        operations.Add(operation);
                        uint indexOperation = figures.Where(f => f.Index > operation.Index).OrderBy(f => f.Index).FirstOrDefault().Index - 1;
                        operations.Add(new Operation { Index = indexOperation, Priority = (Priority)3, Part = Part.Multiply, IsTwoExpression = true });
                        operations.AddRange(operationsMoreNextIndex.Select(o =>
                        {
                            o.Index++;
                            return o;
                        }).ToList());

                        expressions = GetExpressions();
                        count = expressions.Count;
                        ++i;
                    }
                }
            }
        }

        void AddFigureLessThanZero()
        {
            Operation firstOperation = operations.FirstOrDefault(o => o.Index == 0 && o.Part == Part.Minus);
            Figure firstFigure = figures.FirstOrDefault(f => f.Index == 1);
            if (firstOperation != null && firstFigure != null)
            {
                operations.Remove(firstOperation);
                figures.Remove(firstFigure);
                firstFigure.Value = decimal.Parse("-" + firstFigure.Value.ToString());
                figures.Add(firstFigure);
                figures = figures.OrderBy(f => f.Index).ToList();
            }
        }


        bool IsSimpleOperation(Part part)
            => part == Part.Minus || part == Part.Plus || part == Part.Divide ||
            part == Part.Multiply || part == Part.Modulo;
        void IsNotTwoCharactersInRow()
        {
            int count = operations.Count;
            for (int i = 0; i < count; i++)
            {
                Operation operation = operations[i];
                if (i != count - 1 && IsSimpleOperation(operation.Part)
                    && IsSimpleOperation(operations[i + 1].Part)
                    && (operation.Index + 1 == operations[i + 1].Index))
                {
                    string firstOperation = EnumPartToString.WithHtml(operation.Part, "", "");
                    string secondOperation = EnumPartToString.WithHtml(operations[i + 1].Part, "", "");
                    ExpressionAndSolution.Errors.Add(new Error { Message = $"Cannot have two characters in a row", Expression = firstOperation + secondOperation });
                    IsErrors = true;
                }
            }
        }
        void FillData()
        {
            ExpressionAndSolution.ExpressionHtml = UnionCollection.ShowExpression(operations, figures);
            ExpressionAndSolution.Time = DateTime.Now;

            List<Error> errors = new List<Error>();
            List<Warning> warnings = new List<Warning>();
            CalculateOperations calculateOperations = new CalculateOperations(errors, warnings);

            Solve solve = new Solve(operations, figures);
            ExpressionAndSolution.Answer = solve.Result(calculateOperations);
            ExpressionAndSolution.Errors = solve.Errors;
            ExpressionAndSolution.Warnings = solve.Warnings;

            ExpressionAndSolution.Solutions = solve.ResultHtml.Select(r => new Solution { Decision = r }).ToList();
        }

        void Result()
        {
            List<Action> funcs = new List<Action> {
                IsOperation.IsArsh,IsOperation.IsArch,IsOperation.IsArth,IsOperation.IsArcth,IsOperation.IsSh,
                IsOperation.IsCh,IsOperation.IsTh,IsOperation.IsCth,IsOperation.IsArccos, IsOperation.IsArcsin,
                IsOperation.IsArctg,IsOperation.IsArcctg, IsOperation.IsCos, IsOperation.IsSin,IsOperation.IsCtg, IsOperation.IsTg,
                IsOperation.IsExp, IsOperation.IsFactorial,IsOperation.IsExtent,
                IsOperation.IsSqrt,IsOperation.IsAbs,IsOperation.IsLog,IsOperation.IsLn,IsOperation.IsBrackets,
                IsOperation.IsPlus,IsOperation.IsMinus,IsOperation.IsMultiply,IsOperation.IsDivide, IsOperation.IsModulo};
            foreach (Action func in funcs)
                func();

            IsFigure = new(IsOperation.OutputStringExpression);
            IsFigure.Is();
        }
    }
}
