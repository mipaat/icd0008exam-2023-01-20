using DAL;
using DAL.Filters;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.MyLibraries.PageModels;

public abstract class IndexModel<T> : RepositoryModel<T> where T : AbstractDbEntity, new()
{
    protected IndexModel(RepositoryContext ctx) : base(ctx)
    {
    }

    public IList<T> Entities { get; set; } = default!;

    protected virtual IEnumerable<FilterFunc<T>>? Filters => null;

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Entities = (Filters != null ? await Repository.GetAllAsync(Filters.ToArray()) : await Repository.GetAllAsync())
            .ToList();
        return Page();
    }
}