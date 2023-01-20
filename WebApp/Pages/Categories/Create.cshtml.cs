using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Categories;

public class CreateModel : CreateModel<Category>
{
    public CreateModel(RepositoryContext ctx) : base(ctx)
    {
    }
    
    [BindProperty(SupportsGet = true)] public bool ReturnToItemCreate { get; set; }

    protected override CategoryRepository Repository => Ctx.Categories;

    public override async Task<IActionResult> OnPostAsync()
    {
        var result = await base.OnPostAsync();
        if (Success && ReturnToItemCreate) return RedirectToPage("/Items/Create");
        return result;
    }
}