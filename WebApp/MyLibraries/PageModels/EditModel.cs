using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.MyLibraries.PageModels;

public abstract class EditModel<T> : EntityModel<T> where T : AbstractDbEntity, new()
{
    protected EditModel(RepositoryContext ctx) : base(ctx)
    {
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        Repository.Update(Entity);
        await Ctx.SaveChangesAsync();
        Success = true;

        return RedirectToPage("./Index");
    }
}