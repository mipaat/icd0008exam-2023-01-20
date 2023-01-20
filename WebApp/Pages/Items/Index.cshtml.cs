using DAL;
using DAL.Filters;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries;
using WebApp.MyLibraries.PageModels;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Items;

public class IndexModel : IndexModel<Item>, IItemSearch
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

    protected override IEnumerable<FilterFunc<Item>> Filters => IItemSearch.Filters(this);

    protected override ItemRepository Repository => Ctx.Items;
}