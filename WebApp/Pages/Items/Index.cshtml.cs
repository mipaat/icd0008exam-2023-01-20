using DAL;
using DAL.Filters;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Items;

public class IndexModel : IndexModel<Item>, IItemSearch, IMissingItems
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

    public ICollection<ItemWithQuantity> MissingItems
    {
        get
        {
            var result = new List<ItemWithQuantity>();
            foreach (var item in Entities)
            {
                var quantityDiff = item.Quantity!.Value - item.OptimalQuantity;
                if (quantityDiff < 0)
                {
                    result.Add(new ItemWithQuantity(item, -quantityDiff));
                }
            }

            return result;
        }
    }
}