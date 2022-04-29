using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalculator.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "[is-translate=true]")]
    public class LanguageTagHelper : TagHelper
    {
        public CalculatorContext Db { get; set; }
        public LanguageTagHelper(CalculatorContext context)
            => Db = context;

        public string UserId { get; set; }
        public string IsTranslate { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            User user = Db.Users.FirstOrDefault(u => u.Id == UserId);
            Language language = user?.Language ?? Language.English;
            Dictionary dictionary = new Dictionary(language, Db);
            string content = (await output.GetChildContentAsync())?.GetContent() ?? string.Empty;
            string translate = dictionary.Translate(content);
            output.Content.SetContent(translate);
        }
    }
}
