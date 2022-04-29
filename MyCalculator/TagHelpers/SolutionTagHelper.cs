using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Calculator.Show;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    public class SolutionTagHelper : TagHelper
    {
        public ExpressionAndSolution ExpressionAndSolution { get; set; }
        public string UserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ShowSolution show = new ShowSolution(ExpressionAndSolution);

            if(ExpressionAndSolution.Solutions.Any())
            {
                TagBuilder details = new TagBuilder("details");

                TagBuilder summary = new TagBuilder("summary");
                summary.Attributes.Add("user-id", UserId);
                summary.Attributes.Add("is-translate", "true");
                summary.InnerHtml.Append("Solution");

                TagBuilder p3 = new TagBuilder("p");
                p3.InnerHtml.AppendHtml(show.Solution());

                details.InnerHtml.AppendHtml(summary);
                details.InnerHtml.AppendHtml(p3);

                output.Content.AppendHtml(details);
            }
        }
    }
}
