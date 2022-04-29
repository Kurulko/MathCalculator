using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;

namespace MyCalculator.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "[is-submit=true]")]
    public class SubmitTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }
        public SubmitTagHelper(CalculatorContext context)
            => Db = context;

        public string TranslateValue { get; set; }
        public string UserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Language language = user?.Language ?? Language.English;
            Dictionary dictionary = new Dictionary(language, Db);
            string translate = dictionary.Translate(TranslateValue);
            output.Attributes.Add("value", translate);
        }
    }
}
