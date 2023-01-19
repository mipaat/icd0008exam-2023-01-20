using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.MyLibraries.PageModels;

public abstract class EntityModel<T> : RepositoryModel<T> where T : AbstractDbEntity, new()
{
    protected bool Success { get; set; }
    
    protected EntityModel(RepositoryContext ctx) : base(ctx)
    {
    }

    [BindProperty(SupportsGet = true)] public int Id { get; set; }
    [BindProperty] public T Entity { get; set; } = default!;

    public virtual RedirectToPageResult Reset => RedirectToPage("", new { Id });

    public virtual async Task<IActionResult> OnGetAsync()
    {
        if (Id == default) return NotFound();

        var entity = await Repository.GetByIdAsync(Id);
        if (entity == null) return NotFound();

        Entity = entity;
        Success = true;
        return Page();
    }
}