using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;
using WebApp.Pages.Shared;

namespace WebApp.Pages.PerformedJobs;

public class ManageItems : EntityModel<PerformedJob>, IItemSearch, IErrorView
{
    public ManageItems(RepositoryContext ctx) : base(ctx)
    {
    }

    [BindProperty(SupportsGet = true)] public int? AddJobItemsId { get; set; }
    [BindProperty(SupportsGet = true)] public string? ErrorMessage { get; set; }

    protected override PerformedJobRepository Repository => Ctx.PerformedJobs;

    public ICollection<Item> Items { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string? ItemNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public string? CategoryNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public int? MinQuantityQuery { get; set; }
    [BindProperty(SupportsGet = true)] public int? MaxQuantityQuery { get; set; }

    [BindProperty] public UsedItem UsedItem { get; set; } = default!;
    [BindProperty] public bool AddUsedItem { get; set; }

    [BindProperty] public int UsedItemId { get; set; }
    [BindProperty] public int? UpdatedQuantity { get; set; }
    [BindProperty] public bool UpdateUsedItem { get; set; }
    [BindProperty] public bool RemoveUsedItem { get; set; }

    private async Task InitializeItems()
    {
        Items = (await Ctx.Items.GetAllAsync(IItemSearch.Filters(this).ToArray()))
            .Where(i => Entity.UsedItems!.All(ui => ui.ItemId != i.Id))
            .ToList();
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        var result = await base.OnGetAsync();
        await InitializeItems();
        if (AddJobItemsId != null)
        {
            var job = await Ctx.Jobs.GetByIdAsync(AddJobItemsId.Value);
            if (job != null)
            {
                foreach (var jobItem in job.JobItems!)
                {
                    Ctx.UsedItems.Add(new UsedItem(jobItem, Id));
                }

                await Ctx.SaveChangesAsync();
            }

            return RedirectToPage("", new { Id });
        }

        return result;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Entity.Performed != null)
        {
            ErrorMessage = "Can't modify job that's already been performed!";
            return Reset;
        }

        if (AddUsedItem)
        {
            Ctx.UsedItems.Add(UsedItem);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        if (RemoveUsedItem)
        {
            await Ctx.UsedItems.RemoveAsync(UsedItemId);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        if (UpdateUsedItem)
        {
            var usedItem = await Ctx.UsedItems.GetByIdAsync(UsedItemId);
            if (usedItem == null) return Reset;
            if (UpdatedQuantity != null) usedItem.Quantity = UpdatedQuantity.Value;
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        return Reset;
    }
}