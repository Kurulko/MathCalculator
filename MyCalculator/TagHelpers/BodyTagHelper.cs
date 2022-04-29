using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    public class BodyTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }

        public BodyTagHelper(CalculatorContext context)
            => Db = context;
        public string UserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Color color = user?.ThemeColor ?? Color.Black;

            string style = color == Color.Black ? "color:#C0C0C0; background-color:black;"
                : "color:black; background-color:white;";
            output.Attributes.Add("style", style);
        }
    }
}
