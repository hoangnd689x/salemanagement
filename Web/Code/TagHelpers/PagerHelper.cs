using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text;
using Web.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Web.Code.TagHelpers
{

    [HtmlTargetElement("pager")]
    public class PagerHelper : AnchorTagHelper
    {
        protected readonly IHttpContextAccessor HttpContextAccessor;
        public PagerHelper(IHtmlGenerator generator, IHttpContextAccessor _HttpContextAccessor) : base(generator)
        {
            HttpContextAccessor = _HttpContextAccessor;
        }

        public string PageParam { get; set; } = "page";
        public PagerViewModel Pager { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var RouteValues = HttpContextAccessor.HttpContext.Request.Query.Where(m => m.Key != PageParam).ToList(); ;


            int adjacents = 3;
            var prevtag = new TagBuilder("i");
            prevtag.AddCssClass("fa fa-angle-left");

            var nexttag = new TagBuilder("i");
            nexttag.AddCssClass("fa fa-angle-right");

            var result = new StringBuilder();

            if (Pager.TotalPages > 1)
            {
                var page = Pager.Page;
                var tpages = Pager.TotalPages;
                var pmin = (page > adjacents) ? (page - adjacents) : 1;
                var pmax = (page < (tpages - adjacents)) ? (page + adjacents) : tpages;


                if (Pager.Page > 1)
                {

                    var liback = new TagBuilder("li");

                    var backrouteValues = RouteValues.ToDictionary(
                      kvp => kvp.Key,
                      kvp => (object)kvp.Value,
                      StringComparer.OrdinalIgnoreCase);

                    if (!backrouteValues.ContainsKey(PageParam))
                    {
                        backrouteValues.Add(PageParam, Pager.Page - 1);
                    }



                    var tagBack = Generator.GenerateActionLink(
                          ViewContext,
                          linkText:  "",
                          actionName: Action,
                          controllerName: Controller,
                          protocol: Protocol,
                          hostname: Host,
                          fragment: Fragment,
                          routeValues: backrouteValues,
                          htmlAttributes: null);
                    tagBack.InnerHtml.AppendHtml(prevtag.GetString());
                    liback.InnerHtml.AppendHtml(tagBack.GetString());

                    result.AppendLine(liback.GetString());
                }
                for (var i = pmin; i <= pmax; i++)
                {
                    var li = new TagBuilder("li");
                    var routeValues = RouteValues.ToDictionary(
                      kvp => kvp.Key,
                      kvp => (object)kvp.Value,
                      StringComparer.OrdinalIgnoreCase);

                    if (!routeValues.ContainsKey(PageParam))
                    {
                        routeValues.Add(PageParam, i);
                    }

                    var tag = Generator.GenerateActionLink(
                          ViewContext,
                          linkText: i.ToString(),
                          actionName: Action,
                          controllerName: Controller,
                          protocol: Protocol,
                          hostname: Host,
                          fragment: Fragment,
                          routeValues: routeValues,
                          htmlAttributes: null);

                    li.InnerHtml.AppendHtml(tag.GetString());
                    if (i == Pager.Page)
                    {
                        li.AddCssClass("active");
                    }
                    var item = li.GetString();
                    result.AppendLine(item);
                }

                if (Pager.Page < Pager.TotalPages)
                {

                    var li = new TagBuilder("li");

                    var nextrouteValues = RouteValues.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (object)kvp.Value,
                    StringComparer.OrdinalIgnoreCase);

                    if (!nextrouteValues.ContainsKey(PageParam))
                    {
                        nextrouteValues.Add(PageParam, Pager.Page + 1);
                    }

                    var tagNext = Generator.GenerateActionLink(
                          ViewContext,
                          linkText: "",
                          actionName: Action,
                          controllerName: Controller,
                          protocol: Protocol,
                          hostname: Host,
                          fragment: Fragment,
                          routeValues: nextrouteValues,
                          htmlAttributes: null);
                    tagNext.InnerHtml.AppendHtml(nexttag.GetString());
                    li.InnerHtml.AppendHtml(tagNext.GetString());

                    result.AppendLine(li.GetString());
                }
            }
            var html = result.ToString();
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");
            output.Content.SetHtmlContent(html);
            output.TagMode = TagMode.StartTagAndEndTag;

        }

        /*
        public static MvcHtmlString PagerLinks(this HtmlHelper html, Model Model, Func<int, string> PageUrl, int adjacents = 3, )
        {
            var result = new StringBuilder();

            if (Pager.TotalPages > 1)
            {
                var page = Pager.Page;
                var tpages = Pager.TotalPages;
                var pmin = (page > adjacents) ? (page - adjacents) : 1;
                var pmax = (page < (tpages - adjacents)) ? (page + adjacents) : tpages;

                result.Append("<ul class='pagination'>");

                if (Pager.Page > 1 && !string.IsNullOrEmpty(prevhtml))
                {

                    var tagBack = new TagBuilder("a");
                    tagBack.MergeAttribute("href", PageUrl(Pager.Page - 1));
                    tagBack.Attributes.Add("class", "prev");
                    tagBack.InnerHtml = prevhtml;
                    result.AppendLine(string.Format("<li>{0}</li>", tagBack.ToString()));
                }


                for (var i = pmin; i <= pmax; i++)
                {

                    var tag = new TagBuilder("a");
                    tag.MergeAttribute("href", PageUrl(i));
                    tag.InnerHtml = i.ToString();
                    string _spanFormat = i == Pager.Page ? "<li class=\"active\">{0}</li>" : "<li>{0}</li>";

                    result.AppendLine(string.Format(_spanFormat, tag.ToString()));
                }

                if (Pager.Page < Pager.TotalPages && !string.IsNullOrEmpty(nexthtml))
                {
                    var tagNext = new TagBuilder("a");
                    tagNext.MergeAttribute("href", PageUrl(Pager.Page + 1));
                    tagNext.InnerHtml = nexthtml;
                    tagNext.Attributes.Add("class", "next");
                    result.AppendLine(string.Format("<li>{0}</li>", tagNext.ToString()));
                }
                result.Append("</ul>");
            }


            return MvcHtmlString.Create(result.ToString());
        }
        */
    }
}