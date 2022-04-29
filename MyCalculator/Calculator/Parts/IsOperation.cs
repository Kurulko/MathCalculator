using MyCalculator.Calculator.Enums;
using MyCalculator.Calculator.Expressions;
using MyCalculator.Calculator.Extensions;
using MyCalculator.Calculator.Parts.Models;
using MyCalculator.Calculator.Show;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyCalculator.Calculator.Parts
{
    public class IsOperation : IsExpression<Operation>
    {
        string figureStr = @"(\d+\,?\d*)";
        string figureWithMinusStr = @"(-?\d+\,?\d*)";
        public IsOperation(string expression) : base(expression) { }

        void ReplacePart(int startIndex, ref string partStr)
        {
            Regex regex = new Regex(@"(\$+)");
            if (regex.IsMatch(partStr))
            {
                string result = partStr;
                foreach (Match match in regex.Matches(partStr))
                {
                    uint _index = (uint)(OutputStringExpression.IndexOf(result, startIndex) + match.Index);
                    Operation op = Expressions.FirstOrDefault(e => e.Index == _index);
                    if (op != null)
                    {
                        Expressions.Remove(op);
                        partStr = partStr.ReplaceOneTime(match.Value,op.FullStr);
                    }
                }
            }
        }
        string FullStr(Part part, string mainPart, string firstPart, string secondPart)
        {
            string fullStr = string.Empty;
            switch (part)
            {
                case Part.Cos:
                case Part.Sin:
                case Part.Arccos:
                case Part.Arcsin:
                case Part.Tg:
                case Part.Ctg:
                case Part.Arctg:
                case Part.Arcctg:
                case Part.Exp:
                case Part.Sqrt:
                case Part.Sh:
                case Part.Ch:
                case Part.Th:
                case Part.Cth:
                case Part.Arsh:
                case Part.Arch:
                case Part.Arth:
                case Part.Arcth:
                    fullStr = mainPart + firstPart;
                    break;
                case Part.Factorial:
                    fullStr = firstPart + mainPart;
                    break;
                case Part.Extent:
                    fullStr = firstPart + mainPart + secondPart;
                    break;
                case Part.Abs:
                    if (mainPart == "|")
                        fullStr = mainPart + firstPart + mainPart;
                    else
                        fullStr = mainPart + firstPart;
                    break;
                case Part.Brackets:
                    fullStr = firstPart;
                    break;
                case Part.Log:
                    fullStr = mainPart + firstPart + secondPart;
                    break;
                case Part.Ln:
                    fullStr = mainPart + firstPart;
                    break;
            }
            return fullStr;
        }

        void IsWithFigures(Priority priority, Part part, params RegexAndReplaces[] regexes)
        {
            for (int i = 0; i < regexes.Length; ++i)
            {
                Regex regex = new Regex(regexes[i].RegexStr, RegexOptions.IgnoreCase);

                bool resultOfThisRegex = regex.IsMatch(OutputStringExpression);
                if (resultOfThisRegex)
                {
                    foreach (Match match in regex.Matches(OutputStringExpression))
                    {
                        uint index = (uint)match.Index;
                        string value = match.Value;
                        string expression = regex.Replace(value, regexes[i].ReplaceStr);
                        bool isTwoExpression = regexes[i].IsTwoExpressions;

                        Operation op = new Operation { Index = index, Part = part, Priority = priority, Expression = expression, IsTwoExpression = isTwoExpression, FullStr = value };
                        if (isTwoExpression)
                        {
                            string secondExpression = regex.Replace(value, regexes[i].SecondReplaceStr);
                            op.SecondExpression = secondExpression;
                        }
                        Expressions.Add(op);
                    }
                    OutputStringExpression = regex.Replace(OutputStringExpression, m => new string('$', m.Length));
                }
            }
        }
        void IsWithBrackets(Priority priority, Part part, params RegexAndReplacesWithBrackets[] regexes)
        {
            for (int i = 0; i < regexes.Length; i++)
            {
                string regexStr = regexes[i].RegexStr;
                Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
                    /*| RegexOptions.RightToLeft*/);
                bool stop = false;
                while (!stop)
                {
                    stop = false;
                    if (regex.IsMatch(OutputStringExpression))
                    {
                        MatchCollection collection = regex.Matches(OutputStringExpression);
                        foreach (Match item in collection)
                        {
                            string value = item.Value.ToString();
                            bool isTwoExpressions = regexes[i].IsTwoExpressions;

                            string mainPart = regex.Replace(value, regexes[i].MainReplaceStr);
                            string firstPart = regex.Replace(value, regexes[i].ReplaceStr);

                            string secondPart = string.Empty;
                            if (isTwoExpressions)
                                secondPart = regex.Replace(value, regexes[i].SecondReplaceStr);

                            bool resultOfThisRegex = (part == Part.Factorial || part == Part.Extent) ?
                                Brackets.IsBrackets(ref firstPart, true) : Brackets.IsBrackets(ref firstPart);
                            bool isNotThereBrackets = !firstPart.Contains('(') && !firstPart.Contains(')');

                            bool resultOfThisRegexSecondExpression = true;
                            bool isNotThereBracketSecondExpression = true;
                            if (isTwoExpressions)
                            {
                                isNotThereBracketSecondExpression = !secondPart.Contains('(') && !secondPart.Contains(')');
                                resultOfThisRegexSecondExpression = Brackets.IsBrackets(ref secondPart);
                            }

                            if (
                                (resultOfThisRegex && !isNotThereBrackets && !isTwoExpressions)
                                ||
                                (isTwoExpressions && (
                                        (resultOfThisRegex && resultOfThisRegexSecondExpression)
                                        || (resultOfThisRegex && isNotThereBracketSecondExpression)
                                        || (isNotThereBrackets && resultOfThisRegexSecondExpression)
                                    )
                                )
                            )
                            {
                                string fullStr = FullStr(part, mainPart, firstPart, secondPart);

                                Operation op = new Operation
                                {
                                    Index = (uint)OutputStringExpression.IndexOf(fullStr),
                                    Part = part,
                                    Priority = priority,
                                    IsTwoExpression = regexes[i].IsTwoExpressions
                                };

                                ReplacePart(item.Index, ref firstPart);
                                isNotThereBrackets = !IsThereBrackets(firstPart);
                                //isNotThereBrackets = !firstPart.Contains('(') && !firstPart.Contains(')');
                                op.Expression = isNotThereBrackets ? firstPart : firstPart.Substring(1, firstPart.Length - 2);

                                if (isTwoExpressions)
                                {
                                    ReplacePart(item.Index, ref secondPart);
                                    isNotThereBracketSecondExpression = !IsThereBrackets(secondPart);
                                    //isNotThereBracketSecondExpression = !secondPart.Contains('(') && !secondPart.Contains(')');
                                    op.SecondExpression = isNotThereBracketSecondExpression ? secondPart : secondPart.Substring(1, secondPart.Length - 2);
                                }

                                op.FullStr = FullStr(part, mainPart, firstPart, secondPart);
                                Expressions.Add(op);

                                OutputStringExpression = OutputStringExpression.ReplaceOneTime(fullStr, new string('$', fullStr.Length));
                            }
                        }
                    }
                    else
                        stop = true;
                }
            }
        }
        bool IsThereBrackets(string value)
            => value[0] == '(' && value[value.Length - 1] == ')';

        public void IsTrigonometry(Part part, string synonyms = null)
        {
            string regexStr = $@"({part.ToString().ToLower()}{synonyms})(.+)";
            string replaceStr = "$2";
            string mainReplaceStr = "$1";
            RegexAndReplacesWithBrackets regex = new RegexAndReplacesWithBrackets
            {
                RegexStr = regexStr,
                MainReplaceStr = mainReplaceStr,
                ReplaceStr = replaceStr,
                IsTwoExpressions = false
            };
            IsWithBrackets((Priority)1, part, regex);
        }
        public void IsCos() => IsTrigonometry(Part.Cos);
        public void IsSin() => IsTrigonometry(Part.Sin);
        public void IsArcsin() => IsTrigonometry(Part.Arcsin, "|asin");
        public void IsArccos() => IsTrigonometry(Part.Arccos, "|acos");
        public void IsTg() => IsTrigonometry(Part.Tg, "|tan");
        public void IsCtg() => IsTrigonometry(Part.Ctg);
        public void IsArctg() => IsTrigonometry(Part.Arctg, "|atan");
        public void IsArcctg() => IsTrigonometry(Part.Arcctg);
        public void IsSh() => IsTrigonometry(Part.Sh,"|sinh");
        public void IsCh() => IsTrigonometry(Part.Ch,"|cosh");
        public void IsTh() => IsTrigonometry(Part.Th,"|tanh");
        public void IsCth() => IsTrigonometry(Part.Cth);
        public void IsArsh() => IsTrigonometry(Part.Arsh,"|asinh");
        public void IsArch() => IsTrigonometry(Part.Arch,"|acosh");
        public void IsArth() => IsTrigonometry(Part.Arth,"|atanh");
        public void IsArcth() => IsTrigonometry(Part.Arcth);

        public void IsBrackets()
        {
            string regexStr = @"(\(.+\))";
            string replaceStr = "$1";
            RegexAndReplacesWithBrackets regexWithBrackets =
                new RegexAndReplacesWithBrackets { RegexStr = regexStr, ReplaceStr = replaceStr, IsTwoExpressions = false, MainReplaceStr = "" };
            IsWithBrackets((Priority)2, Part.Brackets, regexWithBrackets);
        }
        public void IsFactorial()
        {
            Priority priority = (Priority)1;
            Part part = Part.Factorial;
            string replaceStr = "$1";

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"{figureStr}(!)", ReplaceStr = replaceStr, IsTwoExpressions = false };
            IsWithFigures(priority, part, regex);

            string mainReplaceStr = "$2";
            RegexAndReplacesWithBrackets regexWithBrackets =
               new RegexAndReplacesWithBrackets { RegexStr = $@"(.+)(!)", ReplaceStr = replaceStr, MainReplaceStr = mainReplaceStr, IsTwoExpressions = false };
            IsWithBrackets(priority, part, regexWithBrackets);

        }
        public void IsExp()
        {
            Priority priority = (Priority)1;
            Part part = Part.Exp;

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"(e\^){figureStr}", ReplaceStr = "$2", IsTwoExpressions = false };
            IsWithFigures(priority, part, regex);

            RegexAndReplacesWithBrackets[] regexesWithBrackets =
            {
                new RegexAndReplacesWithBrackets{RegexStr = @"(exp)(.+)",ReplaceStr = "$2",MainReplaceStr = "$1",IsTwoExpressions = false},
                new RegexAndReplacesWithBrackets{RegexStr = @"(e\^)(.+)",ReplaceStr = "$2",MainReplaceStr = "$1",IsTwoExpressions = false}
            };
            IsWithBrackets(priority, part, regexesWithBrackets);
        }
        public void IsAbs()
        {
            Priority priority = (Priority)2;
            Part part = Part.Abs;

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"(\|){figureWithMinusStr}(\|)", ReplaceStr = "$2", IsTwoExpressions = false };
            IsWithFigures(priority, part, regex);

            RegexAndReplacesWithBrackets[] regexesWithBrackets =
            {
                new RegexAndReplacesWithBrackets{RegexStr = @"(\|)(.+)(\|)",ReplaceStr = "$2",MainReplaceStr = "$1",IsTwoExpressions = false},
                new RegexAndReplacesWithBrackets{RegexStr = @"(abs)(.+)",ReplaceStr = "$2",MainReplaceStr = "$1",IsTwoExpressions = false}
            };
            IsWithBrackets(priority, part, regexesWithBrackets);
        }
        public void IsSqrt()
        {
            Priority priority = (Priority)1;
            Part part = Part.Sqrt;

            RegexAndReplacesWithBrackets regexWithBrackets =
                new RegexAndReplacesWithBrackets { RegexStr = @"(sqrt)(.+)", ReplaceStr = "$2", MainReplaceStr = "$1", IsTwoExpressions = false };
            IsWithBrackets(priority, part, regexWithBrackets);
        }

        public void IsLog()
        {
            Priority priority = (Priority)1;
            Part part = Part.Log;

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"(log){figureStr}(\(){figureStr}(\))", ReplaceStr = "$2", SecondReplaceStr = "$4", IsTwoExpressions = true };
            IsWithFigures(priority, part, regex);

            RegexAndReplacesWithBrackets regexWithBrackets = 
                new RegexAndReplacesWithBrackets { RegexStr = $@"(log){figureStr}(.*)", ReplaceStr = "$2", SecondReplaceStr = "$3", MainReplaceStr = "$1", IsTwoExpressions = true };
            IsWithBrackets(priority, part, regexWithBrackets);
        }
        public void IsLn()
        {
            Priority priority = (Priority)1;
            Part part = Part.Ln;

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"(ln)(\(){figureStr}(\))", ReplaceStr = "$3", IsTwoExpressions = false };
            IsWithFigures(priority, part, regex);

            RegexAndReplacesWithBrackets regexWithBrackets = 
                new RegexAndReplacesWithBrackets { RegexStr = $@"(ln)(.*)", ReplaceStr = "$2", MainReplaceStr = "$1", IsTwoExpressions = false };
            IsWithBrackets(priority, part, regexWithBrackets);
        }


        public void IsExtent()
        {
            Priority priority = (Priority)3;
            Part part = Part.Extent;

            RegexAndReplaces regex =
                new RegexAndReplaces { RegexStr = $@"{figureStr}(\^){figureStr}", ReplaceStr = "$1", SecondReplaceStr = "$3", IsTwoExpressions = true };
            IsWithFigures(priority, part, regex);

            RegexAndReplacesWithBrackets[] regexesWithBrackets =
            {
                new RegexAndReplacesWithBrackets{RegexStr = $@"{figureStr}(\^)(.+)",  ReplaceStr = "$1",SecondReplaceStr = "$3",MainReplaceStr = "$2",IsTwoExpressions = true},
                new RegexAndReplacesWithBrackets{RegexStr = $@"(.+)(\^){figureStr}",  ReplaceStr = "$1",SecondReplaceStr = "$3",MainReplaceStr = "$2",IsTwoExpressions = true},
                new RegexAndReplacesWithBrackets{RegexStr = $@"(.+)(\^)(.+)",ReplaceStr = "$1",SecondReplaceStr = "$3",MainReplaceStr = "$2",IsTwoExpressions = true}
            };
            IsWithBrackets(priority, part, regexesWithBrackets);
        }

        public void IsPlus()
        {
            string regexStr = $@"(\+)";
            RegexAndReplaces regexAndReplaces = new RegexAndReplaces { RegexStr = regexStr, ReplaceStr = "", SecondReplaceStr = "", IsTwoExpressions = true };
            IsWithFigures((Priority)4, Part.Plus, regexAndReplaces);
        }
        public void IsMinus()
        {
            string regexStr = $@"(\-)";
            RegexAndReplaces regexAndReplaces = new RegexAndReplaces { RegexStr = regexStr, ReplaceStr = "", SecondReplaceStr = "", IsTwoExpressions = true };
            IsWithFigures((Priority)4, Part.Minus, regexAndReplaces);
        }
        public void IsMultiply()
        {
            string regexStr = $@"(\*)";
            RegexAndReplaces regexAndReplaces = new RegexAndReplaces { RegexStr = regexStr, ReplaceStr = "", SecondReplaceStr = "", IsTwoExpressions = true };
            IsWithFigures((Priority)3, Part.Multiply, regexAndReplaces);
        }
        public void IsDivide()
        {
            string regexStr = $@"(\/)";
            RegexAndReplaces regexAndReplaces = new RegexAndReplaces { RegexStr = regexStr, ReplaceStr = "", SecondReplaceStr = "", IsTwoExpressions = true };
            IsWithFigures((Priority)3, Part.Divide, regexAndReplaces);
        }
        public void IsModulo()
        {
            string regexStr = $@"(%)";
            RegexAndReplaces regexAndReplaces = new RegexAndReplaces { RegexStr = regexStr, ReplaceStr = "", SecondReplaceStr = "", IsTwoExpressions = true };
            IsWithFigures((Priority)3, Part.Modulo, regexAndReplaces);
        }
    }
}
