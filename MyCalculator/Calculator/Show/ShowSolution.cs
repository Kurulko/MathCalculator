using MyCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCalculator.Calculator.Show
{
    public class ShowSolution
    {
        public ExpressionAndSolution ExpressionAndSolution { get; set; }
        public ShowSolution(ExpressionAndSolution expressionAndSolution)
            => ExpressionAndSolution = expressionAndSolution;

        string empty = "<br/>";
        bool IsError()
            => !ExpressionAndSolution.Errors.Any() && !ExpressionAndSolution.Warnings.Any();

        public string Answer()
            => ExpressionAndSolution.ExpressionHtml + (IsError() ? (empty + $"<h2>{ExpressionAndSolution.Answer}</h2>") : "");
        public string Solution()
            => FromListToStringHtml(ExpressionAndSolution.Solutions);
        //public string OnlyRestrictions()
        //    => MessagesHtml(ExpressionAndSolution.Warnings, "warning") + empty
        //        + MessagesHtml(ExpressionAndSolution.Errors, "error");
        //string MessagesHtml(IEnumerable<Restriction> messages, string classCss)
        //{
        //    string result = string.Empty;
        //    int count = messages.Count();
        //    if (count != 0)
        //    {
        //        if (count > 1)
        //        {
        //            result = "<ul>";
        //            foreach (var message in messages)
        //            {
        //                string expression = message.Expression;
        //                result += $"<li class={classCss}>" +
        //                    $"<span user-id={ExpressionAndSolution.UserId} is-translate=true>{message.Message}</span>" +
        //                    (expression != null ? $"<span user-id=\"{ExpressionAndSolution.UserId}\" is-translate=\"true\">in expression</span>{expression}" : "")
        //                     + "</li>";
        //            }
        //            result += "</ul>";
        //        }
        //        else
        //            result = $"<p user-id=\"{ExpressionAndSolution.UserId}\" is-translate=\"true\" class=\"{classCss}\">{messages.FirstOrDefault().Message}</p>";
        //    }
        //    return result;
        //}
        string FromListToStringHtml(List<Solution> collection)
        {
            string result = string.Empty;
            foreach (var item in collection)
                result += item.Decision + empty;
            return result;
        }
    }
}
