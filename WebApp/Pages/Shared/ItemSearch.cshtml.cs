using DAL.Filters;
using Domain;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries;

namespace WebApp.Pages.Shared;

public interface IItemSearch
{
    [BindProperty(SupportsGet = true)] public string? ItemNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public string? CategoryNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public int? MinQuantityQuery { get; set; }
    public int MinQuantity => MinQuantityQuery ?? 0;
    [BindProperty(SupportsGet = true)] public int? MaxQuantityQuery { get; set; }
    public int MaxQuantity => MaxQuantityQuery ?? int.MaxValue;

    public static IEnumerable<FilterFunc<Item>> Filters(IItemSearch itemSearch)
    {
        var result = new List<FilterFunc<Item>>
        {
            new(iq => iq.Where(i => i.Quantity >= itemSearch.MinQuantity), false),
            new(iq => iq.Where(i => i.Quantity <= itemSearch.MaxQuantity), false)
        };
        result.AddRange(WebUtils.GetFiltersFromQuery<Item>(itemSearch.ItemNameQuery, i => i.Name));
        result.AddRange(WebUtils.GetFiltersFromQuery<Item>(itemSearch.CategoryNameQuery, i => i.Category!.Name));
        return result;
    }
}