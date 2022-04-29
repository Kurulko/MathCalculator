using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    [HtmlTargetElement("input", Attributes = " [is-not-submit=true]")]
    [HtmlTargetElement("textarea")]
    public class InputTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }

        public InputTagHelper(CalculatorContext context)
            => Db = context;

        public string UserId { get; set; }
        //public string TranslateValue { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);

            //Language language = user?.Language ?? Language.English;
            //Dictionary dictionary = new Dictionary(language);
            //string translate = dictionary.Translate(TranslateValue);
            //output.Content.SetContent(translate);

            Color color = user?.ThemeColor ?? Color.Black;
            string style = color == Color.Black ? "background-color: #f5f6f7;"
                : "background-color:white;";
            output.Attributes.Add("style", style);
        }
    }
}
