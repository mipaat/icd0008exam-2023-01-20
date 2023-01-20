using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DAL.Filters;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.MyLibraries;

public static class WebUtils
{
    public static IEnumerable<SelectListItem> GetSelectOptions<T>(IEnumerable<T> items, T selectedItem)
        where T : IFilter
    {
        return GetSelectOptions(items, selectedItem, filter => filter.Identifier,
            filter => filter.DisplayString ?? filter.Identifier);
    }

    public static IEnumerable<SelectListItem> GetSelectOptions<T>(IEnumerable<T> items, T selectedItem,
        Func<T, string> identifierFunc, Func<T, string>? customToString = null)
    {
        var result = new List<SelectListItem>();
        foreach (var item in items)
            result.Add(new SelectListItem(customToString?.Invoke(item) ?? item?.ToString() ?? "",
                identifierFunc.Invoke(item), item?.Equals(selectedItem) ?? false));

        return result;
    }

    private static ModelExpressionProvider? GetExpressionProvider<TModel>(IHtmlHelper<TModel> htmlHelper)
    {
        return htmlHelper.ViewContext.HttpContext.RequestServices
            .GetService<ModelExpressionProvider>();
    }

    private static string? GetExpressionText<TModel, TProperty>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression)
    {
        return GetExpressionProvider(htmlHelper)?.GetExpressionText(expression);
    }

    private static string? GetPropertyName<TModel, TProperty>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression)
    {
        return htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(GetExpressionText(htmlHelper,
            expression));
    }

    private static string SingleSpaces(string s)
    {
        return Regex.Replace(s, @" +", " ");
    }

    public static IHtmlContent HiddenFor<TModel, TProperty>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty value)
        where TModel : PageModel
    {
        var propertyName = GetPropertyName(htmlHelper, expression);

        return htmlHelper.Hidden(propertyName, value);
    }

    public static IHtmlContent TextBoxFor<TModel, TProperty>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty value,
        string @class = "", bool appendValidationMessage = true)
    {
        var propertyName = GetPropertyName(htmlHelper, expression);

        @class = SingleSpaces(@class);
        var htmlAttributes = new { @class, type = typeof(TProperty) == typeof(int) ? "number" : "text" };

        var contentBuilder = new HtmlContentBuilder()
            .AppendHtml(htmlHelper.TextBox(propertyName, value, htmlAttributes));
        if (appendValidationMessage) contentBuilder.AppendHtml(ValidationMessageFor(htmlHelper, expression));
        return contentBuilder;
    }

    public static IHtmlContent TextBoxForFloat<TModel, TProperty>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, double value,
        int decimals = 2, string @class = "", bool appendValidationMessage = true)
    {
        var propertyName = GetPropertyName(htmlHelper, expression);

        @class = SingleSpaces(@class);
        string step;
        if (decimals < 0)
        {
            step = "any";
        }
        else if (decimals == 0)
        {
            step = "1";
            value = Math.Round(value, decimals);
        }
        else
        {
            step = "0." + new string('0', decimals - 1) + "1";
            value = Math.Round(value, decimals);
        }

        var htmlAttributes = new { @class, type = "number", step };

        var contentBuilder = new HtmlContentBuilder()
            .AppendHtml(htmlHelper.TextBox(propertyName, value, htmlAttributes));
        if (appendValidationMessage)
            contentBuilder = contentBuilder.AppendHtml(ValidationMessageFor(htmlHelper, expression));
        return contentBuilder;
    }

    public static IHtmlContent TextBoxForDecimal<TModel, TProperty>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, decimal? value,
        int decimals = 2, string @class = "", bool appendValidationMessage = true)
    {
        var propertyName = GetPropertyName(htmlHelper, expression);
        
        @class = SingleSpaces(@class);
        string step;
        if (decimals < 0)
        {
            step = "any";
        }
        else if (decimals == 0)
        {
            step = "1";
            value = Math.Round(value.GetValueOrDefault(), decimals);
        }
        else
        {
            step = "0." + new string('0', decimals - 1) + "1";
            value = Math.Round(value.GetValueOrDefault(), decimals);
        }

        var htmlAttributes = new { @class, type = "number", step };

        var contentBuilder = new HtmlContentBuilder()
            .AppendHtml(htmlHelper.TextBox(propertyName, value, htmlAttributes));
        if (appendValidationMessage)
            contentBuilder = contentBuilder.AppendHtml(ValidationMessageFor(htmlHelper, expression));
        return contentBuilder;
    }

    private static IHtmlContent ValidationMessageFor<TModel, TProperty>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string tagName = "span")
    {
        var result = new TagBuilder(tagName);
        result.AddCssClass("text-danger");
        result.InnerHtml.AppendHtml(htmlHelper.ValidationMessageFor(expression));
        return result;
    }

    public static IHtmlContent CheckBoxFor<TModel>(
        IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, bool value,
        string @class = "")
    {
        var propertyName = GetPropertyName(htmlHelper, expression);

        @class = SingleSpaces(@class);
        var htmlAttributes = new { @class };

        return htmlHelper.CheckBox(propertyName, value, htmlAttributes);
    }

    public static IHtmlContent ButtonFor<TModel>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, bool>> expression, string? text = null, string @class = "")
    {
        var propertyName = GetPropertyName(htmlHelper, expression);

        @class += " btn";
        @class = SingleSpaces(@class);

        return new HtmlString(
            $"<button type=\"submit\" name={propertyName} value={true.ToString()} class=\"{@class}\">{text ?? propertyName}</button>");
    }

    public static IHtmlContent ButtonPrimaryFor<TModel>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, bool>> expression, string? text = null, string @class = "")
    {
        return ButtonFor(htmlHelper, expression, text, @class + " btn-primary");
    }

    public static IHtmlContent ButtonDangerFor<TModel>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, bool>> expression, string? text = null, string @class = "")
    {
        return ButtonFor(htmlHelper, expression, text, @class + " btn-danger");
    }

    public static IHtmlContent ButtonSuccessFor<TModel>(IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, bool>> expression, string? text = null, string @class = "")
    {
        return ButtonFor(htmlHelper, expression, text, @class + " btn-success");
    }
}