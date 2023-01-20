using DAL;
using DAL.Filters;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.PerformedJobs;

public class IndexModel : IndexModel<PerformedJob>
{
    public IndexModel(RepositoryContext ctx) : base(ctx)
    {
    }

    protected override PerformedJobRepository Repository => Ctx.PerformedJobs;

    [BindProperty(SupportsGet = true)]
    public PerformedJobCompletionFilter PerformedJobCompletionFilter { get; set; } = PerformedJobCompletionFilter.Default;

    [BindProperty(SupportsGet = true)] public string? ContextQuery { get; set; }

    public List<ItemWithQuantity> SpentItems { get; set; } = default!;

    protected override IEnumerable<FilterFunc<PerformedJob>> Filters
    {
        get
        {
            var result = new List<FilterFunc<PerformedJob>> { PerformedJobCompletionFilter.FilterFunc };
            result.AddRange(WebUtils.GetFiltersFromQuery<PerformedJob>(ContextQuery, pj => pj.Context));
            return result;
        }
    }

    private void InitializeSpentItems()
    {
        SpentItems = new List<ItemWithQuantity>();
        foreach (var performedJob in Entities)
        {
            foreach (var usedItem in performedJob.UsedItems!)
            {
                var spentItem = SpentItems.FirstOrDefault(i => i.Item.Id == usedItem.ItemId);
                if (spentItem == null)
                {
                    spentItem = new ItemWithQuantity(usedItem.Item!, usedItem.Quantity);
                    SpentItems.Add(spentItem);
                }
                else
                {
                    spentItem.Quantity += usedItem.Quantity;
                }
            }
        }
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        var result = await base.OnGetAsync();

        InitializeSpentItems();
        
        return result;
    }
}