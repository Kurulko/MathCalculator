using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    public class TableTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }
        public TableTagHelper(CalculatorContext context)
            => Db = context;

        public string UserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Color color = user?.ThemeColor ?? Color.Black;
            output.Attributes.Add("class", "table " + (color == Color.Black ? "table-dark" : "") + " table-hover");
        }
    }
}
