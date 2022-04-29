using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    public class HeaderTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }

        public HeaderTagHelper(CalculatorContext context)
            => Db = context;
        public string UserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Color color = user?.ThemeColor ?? Color.Black;

            string style = color == Color.Black ? "background-color:#191970; color:#F0FFF0;"
                : "color:white; background-color:black;";
            output.Attributes.Add("style", style);
        }
    }
}
