using System.Collections;
using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;
using WebApp.Pages.Shared;

namespace WebApp.Pages.PerformedJobs;

public class Perform : EntityModel<PerformedJob>, IErrorView
{
    public Perform(RepositoryContext ctx) : base(ctx)
    {
    }

    protected override PerformedJobRepository Repository => Ctx.PerformedJobs;

    [BindProperty(SupportsGet = true)] public string? ErrorMessage { get; set; }

    [BindProperty] public DateTime Performed { get; set; } = DateTime.UtcNow;
    [BindProperty] public decimal TotalCost { get; set; }

    public ICollection<Item> Items { get; set; } = default!;
    public ICollection<ItemWithQuantity> MissingItems { get; set; } = default!;

    private async Task InitializeItems()
    {
        Items = await Ctx.Items.GetAllAsync();
        MissingItems = Entity.MissingItems(Items);
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        var result = await base.OnGetAsync();
        if (Success)
        {
            Performed = Entity.Performed ?? DateTime.UtcNow;
            TotalCost = Entity.Cost!.Value;
        }
        await InitializeItems();
        if (MissingItems.Count > 0)
        {
            ErrorMessage = "Can't perform job! Missing items: " + string.Join(", ",
                MissingItems.Select(mi => $"'{mi.Item.Name}' (Missing amount: {mi.Quantity})"));
        }
        return result;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await base.OnGetAsync();
        if (!Success) return result;
        await InitializeItems();
        if (MissingItems.Count > 0)
        {
            return Reset;
        }

        foreach (var usedItem in Entity.UsedItems!)
        {
            var remaining = await Ctx.ItemInStocks.RemoveAsync(usedItem.ItemId, usedItem.Quantity);
            if (remaining > 0)
            {
                var insufficientItemMessage = $"Can't perform job! Missing item: '{usedItem.Item!.Name}' (Missing amount: {remaining})";
                return RedirectToPage("", new { Id, ErrorMessage = insufficientItemMessage });
            }
        }

        Entity.Performed = Performed;
        Entity.TotalCost = TotalCost;

        await Ctx.SaveChangesAsync();
        return RedirectToPage("./ManageItems", new { Id });
    }
}