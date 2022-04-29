using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Calculator.Show;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    public class AnswerTagHelper : TagHelper
    {
        public ExpressionAndSolution ExpressionAndSolution { get; set; }
        public string UserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ShowSolution show = new ShowSolution(ExpressionAndSolution);

            TagBuilder p1 = new TagBuilder("p");
            p1.InnerHtml.AppendHtml(show.Answer());
            p1.AddCssClass("without_oblique");
            output.Content.AppendHtml(p1);
        }
    }
}
