using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Jobs;

public class IndexModel : IndexModel<Job>
{
    public IndexModel(RepositoryContext ctx) : base(ctx)
    {
    }

    public ICollection<Item> Items { get; set; } = default!;
    public ICollection<ItemWithQuantity> SpentItems { get; set; } = default!;

    protected override JobRepository Repository => Ctx.Jobs;

    public async Task InitializeItems()
    {
        Items = await Ctx.Items.GetAllAsync();
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        await InitializeItems();
        return await base.OnGetAsync();
    }
}