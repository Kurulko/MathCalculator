using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalculator.TagHelpers
{
    //public class RestrictionsTagHelper : TagHelper
    //{
    //    public List<Warning> Warnings { get; set; }
    //    public List<Error> Errors { get; set; }
    //    public string UserId { get; set; }

    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        TagBuilder warnings = MessagesHtml(Warnings, "warning");
    //        if (warnings != null)
    //            output.Content.AppendHtml(warnings);

    //        TagBuilder errors = MessagesHtml(Errors, "error");
    //        if (errors != null)
    //        {
    //            if (warnings != null)
    //                output.Content.AppendHtml(new TagBuilder("br"));
    //            output.Content.AppendHtml(errors);
    //        }
    //    }
    //    TagBuilder MessagesHtml(IEnumerable<Restriction> messages, string classCss)
    //    {
    //        TagBuilder tag = null;
    //        int count = messages.Count();
    //        if (count != 0)
    //        {
    //            if (count > 1)
    //            {
    //                tag = new TagBuilder("<ul>");
    //                foreach (var message in messages)
    //                {
    //                    string expression = message.Expression;

    //                    tag = new TagBuilder("li");
    //                    tag.Attributes.Add("class", classCss);

    //                    TagBuilder span1 = new TagBuilder("span");
    //                    span1.Attributes.Add("user-id", UserId);
    //                    span1.Attributes.Add("is-translate", "true");
    //                    span1.InnerHtml.Append(message.Message);

    //                    tag.InnerHtml.AppendHtml(span1);
    //                    if(expression != null)
    //                    {
    //                        TagBuilder span2 = new TagBuilder("span");
    //                        span2.InnerHtml.Append("in expression");
    //                        span2.Attributes.Add("user-id", UserId);
    //                        span2.Attributes.Add("is-translate", "true");

    //                        tag.InnerHtml.AppendHtml(span2);
    //                        tag.InnerHtml.Append(expression);
    //                    }

    //                }
    //            }
    //            else
    //            {
    //                tag = new TagBuilder("p");
    //                tag.InnerHtml.Append(messages.FirstOrDefault().Message);
    //                tag.Attributes.Add("user-id", UserId);
    //                tag.Attributes.Add("is-translate", "true");
    //                tag.Attributes.Add("class", classCss);
    //            }
    //        }
    //        return tag;
    //    }

    //}
}
