using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Jobs;

public class CreateModel : CreateModel<Job>
{
    public CreateModel(RepositoryContext ctx) : base(ctx)
    {
    }

    protected override JobRepository Repository => Ctx.Jobs;

    public override async Task<IActionResult> OnPostAsync()
    {
        var result = await base.OnPostAsync();
        if (Success) return RedirectToPage("./ManageItems", new { Entity.Id });
        return result;
    }
}