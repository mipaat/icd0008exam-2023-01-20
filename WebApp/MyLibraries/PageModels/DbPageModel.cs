using DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.MyLibraries.PageModels;

public abstract class DbPageModel : PageModel
{
    protected readonly RepositoryContext Ctx;

    protected DbPageModel(RepositoryContext ctx)
    {
        Ctx = ctx;
    }
}