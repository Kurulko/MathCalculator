using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "[is-btn=true]")]
    [HtmlTargetElement("input", Attributes = "[is-btn=true]")]
    public class BtnTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }
        public BtnTagHelper(CalculatorContext context)
            => Db = context;

        public string UserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Color color = user?.ThemeColor ?? Color.Black;
            output.Attributes.Add("class", "btn btn-outline-" + (color == Color.Black ? "light" : "primary"));
        }
    }

}
