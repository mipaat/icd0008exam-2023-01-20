using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Items;

public class CreateModel : CreateModel<Item>
{
    public CreateModel(RepositoryContext ctx) : base(ctx)
    {
    }

    [BindProperty(SupportsGet = true)] public int? ReturnToJobId { get; set; }
    [BindProperty(SupportsGet = true)] public int? SelectedCategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = default!;
    [BindProperty] public string PriceString { get; set; } = "0";

    protected override ItemRepository Repository => Ctx.Items;

    private async Task InitializeCategories()
    {
        var categories = new List<SelectListItem>();
        foreach (var category in await Ctx.Categories.GetAllAsync())
        {
            categories.Add(new SelectListItem(category.Name, category.Id.ToString(),
                Entity?.CategoryId == category.Id));
        }

        Categories = categories;
    }

    public async Task OnGetAsync()
    {
        if (SelectedCategoryId != null)
        {
            Entity = new Item
            {
                CategoryId = SelectedCategoryId.Value
            };
        }
        await InitializeCategories();
    }

    public override async Task<IActionResult> OnPostAsync()
    {
        await InitializeCategories();
        Entity.Price = decimal.Parse(PriceString);
        var result = await base.OnPostAsync();
        if (ReturnToJobId != null) return RedirectToPage("/Jobs/ManageItems", new { Id = ReturnToJobId });
        return result;
    }
}