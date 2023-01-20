using DAL;
using DAL.Filters;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Items;

public class IndexModel : IndexModel<Item>
{
    public IndexModel(RepositoryContext ctx) : base(ctx)
    {
    }
    
    [BindProperty(SupportsGet = true)] public string? ItemNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public string? CategoryNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public int? MinQuantityQuery { get; set; }
    public int MinQuantity => MinQuantityQuery ?? 0;
    [BindProperty(SupportsGet = true)] public int? MaxQuantityQuery { get; set; }
    public int MaxQuantity => MaxQuantityQuery ?? int.MaxValue;

    protected override IEnumerable<FilterFunc<Item>> Filters
    {
        get
        {
            var result = new List<FilterFunc<Item>>
            {
                new(iq => iq.Where(i => i.Quantity >= MinQuantity), false),
                new(iq => iq.Where(i => i.Quantity <= MaxQuantity), false)
            };
            result.AddRange(WebUtils.GetFiltersFromQuery<Item>(ItemNameQuery, i => i.Name));
            result.AddRange(WebUtils.GetFiltersFromQuery<Item>(CategoryNameQuery, i => i.Category!.Name));
            return result;
        }
    }

    protected override ItemRepository Repository => Ctx.Items;
}