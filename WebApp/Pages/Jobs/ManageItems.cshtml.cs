using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Jobs;

public class ManageItems : EntityModel<Job>, IItemSearch
{
    public ManageItems(RepositoryContext ctx) : base(ctx)
    {
    }

    public ICollection<Item> Items { get; set; } = default!;

    protected override JobRepository Repository => Ctx.Jobs;

    [BindProperty] public JobItem JobItem { get; set; } = default!;
    [BindProperty] public bool AddJobItem { get; set; }

    [BindProperty] public int? JobItemId { get; set; }
    [BindProperty] public int? JobItemQuantity { get; set; }
    [BindProperty] public bool UpdateJobItem { get; set; }
    [BindProperty] public bool RemoveJobItem { get; set; }

    
    [BindProperty(SupportsGet = true)] public string? ItemNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public string? CategoryNameQuery { get; set; }
    [BindProperty(SupportsGet = true)] public int? MinQuantityQuery { get; set; }
    public int MinQuantity => MinQuantityQuery ?? 0;
    [BindProperty(SupportsGet = true)] public int? MaxQuantityQuery { get; set; }
    public int MaxQuantity => MaxQuantityQuery ?? int.MaxValue;

    private async Task InitializeItems()
    {
        Items = (await Ctx.Items.GetAllAsync(IItemSearch.Filters(this).ToArray()))
            .Where(i => Entity.JobItems!.All(ji => ji.ItemId != i.Id))
            .ToList();
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        var result = await base.OnGetAsync();
        if (Success) await InitializeItems();
        return result;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (RemoveJobItem)
        {
            Console.WriteLine("HERE");
            Console.WriteLine(JobItemId);
            if (JobItemId == null) return Reset;
            await Ctx.JobItems.RemoveAsync(JobItemId.Value);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        if (UpdateJobItem)
        {
            if (JobItemId == null) return Reset;
            var jobItem = await Ctx.JobItems.GetByIdAsync(JobItemId.Value);
            if (jobItem == null) return Reset;
            if (JobItemQuantity != null) jobItem.Quantity = JobItemQuantity.Value;
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        if (AddJobItem)
        {
            Ctx.JobItems.Add(JobItem);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        return Reset;
    }
}