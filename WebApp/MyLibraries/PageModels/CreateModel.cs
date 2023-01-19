using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.MyLibraries.PageModels;

public abstract class CreateModel<T> : RepositoryModel<T> where T : AbstractDbEntity, new()
{
    protected CreateModel(RepositoryContext ctx) : base(ctx)
    {
    }

    [BindProperty] public T Entity { get; set; } = default!;

    public bool Success { get; set; }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Success = false;
            return Page();
        }

        Repository.Add(Entity);
        await Ctx.SaveChangesAsync();

        Success = true;
        
        return RedirectToPage("./Index");
    }
}