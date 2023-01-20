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

    public IEnumerable<SelectListItem> Categories { get; set; } = default!;
    [BindProperty] public string PriceString { get; set; } = "0";

    protected override ItemRepository Repository => Ctx.Items;

    private async Task InitializeCategories()
    {
        var categories = new List<SelectListItem>();
        foreach (var category in await Ctx.Categories.GetAllAsync())
        {
            categories.Add(new SelectListItem(category.Name, category.Id.ToString(), Entity?.CategoryId == category.Id));
        }

        Categories = categories;
    }
    
    public async Task OnGetAsync()
    {
        await InitializeCategories();
    }
    
    public override async Task<IActionResult> OnPostAsync()
    {
        await InitializeCategories();
        Entity.Price = decimal.Parse(PriceString);
        return await base.OnPostAsync();
    }
}