using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Jobs;

public class IndexModel : IndexModel<Job>
{
    public IndexModel(RepositoryContext ctx) : base(ctx)
    {
    }

    protected override JobRepository Repository => Ctx.Jobs;
}