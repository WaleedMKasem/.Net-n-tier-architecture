
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Arabia.Core.Common;

namespace Arabia.Web.Common
{
    public static class CustomHtmlHelpers
    {
        private static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }
        public static IHtmlString Image(this HtmlHelper helper, string actionName, string controllerName, int id, int width = 100, int height = 100)
        {
            //<img src='@Url.Action( "GetImage", "EmpQualification", new { id =Model.Id} )' />
            //<img alt="" src="/EmpQualification/GetImage/1" width="100">
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", "/" + controllerName + "/" + actionName + "/" + id);
            tb.Attributes.Add("alt", "");
            tb.Attributes.Add("style", "width:" + width.ToString() + "px" + ";height:" + height + "px");
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString UploadFile(this HtmlHelper helper,  string accept = "*", string name = "uploadedFile")
        {
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("accept", accept);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt", alt);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString ImageUrl(this HtmlHelper helper, string imgUrl, int width = 100, int height = 100)
        { 
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", imgUrl);
            tb.Attributes.Add("alt", "");
            tb.Attributes.Add("style", "width:" + width.ToString() + "px" + ";height:" + height + "px");
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
 
        public static IHtmlString UMQDate<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            TagBuilder tb = new TagBuilder("input");
            var member = GetMemberInfo(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            var reqMessage = member.Member.GetCustomAttributes(typeof(ArabiaRequired), false).FirstOrDefault() as ArabiaRequired; 
            tb.Attributes.Add("type", "text");
            tb.Attributes.Add("id", member.Member.Name);
            tb.Attributes.Add("name", ExpressionHelper.GetExpressionText(expression));
            tb.Attributes.Add("focusInvalid", "false");
            tb.Attributes.Add("class", "UMQDate");
            tb.Attributes.Add("readonly", null);
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                tb.Attributes.Add("value", date == DateTime.MinValue ? "" : date.ToString("yyyy/MM/dd"));
            }
            if (metadata.IsRequired)
            {
                tb.Attributes.Add("data-val", "true");
                tb.Attributes.Add("data-val-required", reqMessage == null?"":reqMessage.RequiredMessage);
                
            }
            string output = tb.ToString(TagRenderMode.SelfClosing);
            return new MvcHtmlString(output);
        }
        public static IHtmlString UMQLabel<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string output = string.Empty;
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                output = date == DateTime.MinValue ? string.Empty : date.ToString("yyyy/MM/dd");
            }
            return new MvcHtmlString(output);
        }
        public static IHtmlString GRGDate<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        { 
            TagBuilder tb = new TagBuilder("input");
            var member = GetMemberInfo(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            var reqMessage = member.Member.GetCustomAttributes(typeof(ArabiaRequired), false).FirstOrDefault() as ArabiaRequired; 
            tb.Attributes.Add("type", "text");
            tb.Attributes.Add("id", member.Member.Name);
            tb.Attributes.Add("name", ExpressionHelper.GetExpressionText(expression));
            tb.Attributes.Add("class", "GRGDate valid");
            tb.Attributes.Add("readonly", null);
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                tb.Attributes.Add("value", date == DateTime.MinValue ? "" : date.ToString("yyyy/MM/dd", new CultureInfo("ar-EG")));
            }
            if (metadata.IsRequired)
            {
                tb.Attributes.Add("data-val", "true");
                tb.Attributes.Add("data-val-required", reqMessage == null ? "" : reqMessage.RequiredMessage);
            }
            string output = tb.ToString(TagRenderMode.SelfClosing);
            return new MvcHtmlString(output);
        }
        public static IHtmlString GRGLabel<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string output = string.Empty;
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                output = date == DateTime.MinValue ? "" : date.ToString("dd/MM/yyyy", new CultureInfo("ar-EG"));
            }
            return new MvcHtmlString(output);
        }
        public static string DisplayGRGDate<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                return date.ToString("dd/MM/yyyy", new CultureInfo("ar-EG"));
            }
            return "";
        }
        public static string DisplayUMQDate<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;
            if (value != null)
            {
                var date = DateTime.Parse(value.ToString(), new CultureInfo("ar-SA"));
                return date.ToString("yyyy/MM/dd");
            }
            return "";
        }

        public static IHtmlString Pager<TModel, TValue>(this HtmlHelper<TModel> html, PagingHelper<TValue> model,int PagerSize = 5)
        {
            TagBuilder divPrev = new TagBuilder("div");
            int cGroupSize = PagerSize;
            var pageCount = (int)Math.Ceiling(model.TotalCount / (double)model.PageSize);
            int CurrentPage = model.CurrentPage;
            // cleanup any out bounds page number passed    
            CurrentPage = Math.Max(CurrentPage, 1);
            CurrentPage = Math.Min(CurrentPage, pageCount);


            var urlHelper = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);
            var actionName = html.ViewContext.RouteData.GetRequiredString("Action");  

            var lastGroupNumber = CurrentPage;
            while ((lastGroupNumber % cGroupSize != 0)) lastGroupNumber++;
            var groupEnd = Math.Min(lastGroupNumber, pageCount);
            var groupStart = lastGroupNumber - (cGroupSize - 1);
            // next     
            if (CurrentPage < pageCount)
            {
                var next = new TagBuilder("a");
                next.SetInnerText("التالى");
                next.AddCssClass("next");
                var routingValues = new RouteValueDictionary(new { page = (model.PageIndex + 1) });
                next.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                divPrev.InnerHtml += next.ToString();     
            }
            // next page    
            if (pageCount > groupEnd)
            {
                var nextPage = new TagBuilder("a");
                nextPage.SetInnerText("...");
                nextPage.AddCssClass("next-dots");
                var routingValues = new RouteValueDictionary(new { page = ((groupEnd + 1).ToString()) });
                nextPage.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                divPrev.InnerHtml += nextPage.ToString();     
            }
            if (pageCount > 1)
            {
                for (var i = groupStart; i <= groupEnd; i++)
                {
                    var pageNumber = new TagBuilder("a");
                    pageNumber.AddCssClass(((i == CurrentPage)) ? "selected-page" : "page");
                    pageNumber.SetInnerText((i).ToString());
                    var routingValues = new RouteValueDictionary(new { page = (i) });
                    pageNumber.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                    divPrev.InnerHtml += pageNumber.ToString();

                }
            }
            // prev page     
            if (CurrentPage > cGroupSize)
            {
                var previousPage = new TagBuilder("a");
                previousPage.SetInnerText("...");
                previousPage.AddCssClass("previous-dots");
                var routingValues = new RouteValueDictionary(new { page = ((groupStart - cGroupSize).ToString()) });
                previousPage.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                divPrev.InnerHtml += previousPage.ToString();  
            }
            //prev
            if (CurrentPage > 1)
            {
                var previous = new TagBuilder("a");
                previous.SetInnerText("السابق");
                previous.AddCssClass("previous");
                var routingValues = new RouteValueDictionary(new { page = (model.PageIndex - 1) });
                previous.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                divPrev.InnerHtml += previous.ToString(); 

            }
            string output = divPrev.ToString();
            return new MvcHtmlString(output);
        }
        public static IHtmlString AjaxPager<TModel, TValue>(this AjaxHelper<TModel> ajaxHelper, PagingHelper<TValue> model, string actionName, AjaxOptions ajaxOptions, string search, int PagerSize = 5)
        {
            if (model.TotalCount == 0) return new MvcHtmlString("");
            TagBuilder divPrev = new TagBuilder("div");
            int cGroupSize = PagerSize;
            var pageCount = (int)Math.Ceiling(model.TotalCount / (double)model.PageSize);
            int CurrentPage = model.CurrentPage;
            // cleanup any out bounds page number passed    
            CurrentPage = Math.Max(CurrentPage, 1);
            CurrentPage = Math.Min(CurrentPage, pageCount);


            var urlHelper = new UrlHelper(ajaxHelper.ViewContext.RequestContext, ajaxHelper.RouteCollection);
            //var actionName = ajaxHelper.ViewContext.RouteData.GetRequiredString("Action");

            var lastGroupNumber = CurrentPage;
            while ((lastGroupNumber % cGroupSize != 0)) lastGroupNumber++;
            var groupEnd = Math.Min(lastGroupNumber, pageCount);
            var groupStart = lastGroupNumber - (cGroupSize - 1);




            // next     
            if (CurrentPage < pageCount)
            {
                var routingValues = new RouteValueDictionary(new { page = (model.PageIndex + 1), Search = search });
                var next = ajaxHelper.ActionLink("التالى", actionName, routingValues, ajaxOptions, new Dictionary<string, object> { { "class", "next" } });
                divPrev.InnerHtml += next.ToString();

            }
            // next page    
            if (pageCount > groupEnd)
            {
                var routingValues = new RouteValueDictionary(new { page = ((groupEnd + 1).ToString()), Search = search });
                var next = ajaxHelper.ActionLink("...", actionName, routingValues, ajaxOptions, new Dictionary<string, object> { { "class", "next-dots" } });
                divPrev.InnerHtml += next.ToString();
            }
            if (pageCount > 1)
            {
                for (var i = groupStart; i <= groupEnd; i++)
                {
                    var routingValues = new RouteValueDictionary(new { page = (i), Search = search });
                    var next = ajaxHelper.ActionLink((i).ToString(), actionName, routingValues, ajaxOptions, new Dictionary<string, object> { { "class", ((i == CurrentPage)) ? "selected-page" : "page" } });
                    divPrev.InnerHtml += next.ToString();

                }
            }
            // prev page     
            if (CurrentPage > cGroupSize)
            {
                var routingValues = new RouteValueDictionary(new { page = ((groupStart - cGroupSize).ToString()), Search = search });
                var next = ajaxHelper.ActionLink("...", actionName, routingValues, ajaxOptions, new Dictionary<string, object> { { "class", "previous-dots" } });
                divPrev.InnerHtml += next.ToString();
            }
            //prev
            if (CurrentPage > 1)
            {
                var routingValues = new RouteValueDictionary(new { page = (model.PageIndex - 1), Search = search });
                var next = ajaxHelper.ActionLink("السابق", "GetData", routingValues, ajaxOptions, new Dictionary<string, object> { { "class", "previous" } });
                divPrev.InnerHtml += next.ToString();

            }
            string output = divPrev.ToString();
            return new MvcHtmlString(output);
        }
    }

}