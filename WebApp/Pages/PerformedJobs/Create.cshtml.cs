using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.PerformedJobs;

public class CreateModel : CreateModel<PerformedJob>
{
    public CreateModel(RepositoryContext ctx) : base(ctx)
    {
    }

    [BindProperty(SupportsGet = true)] public int? JobId { get; set; }
    public IEnumerable<SelectListItem> Jobs { get; set; } = default!;
    public Job? Job { get; set; }

    protected override PerformedJobRepository Repository => Ctx.PerformedJobs;

    private async Task InitializeJobs()
    {
        var result = new List<SelectListItem>();
        foreach (var job in await Ctx.Jobs.GetAllAsync())
        {
            result.Add(new SelectListItem(job.Name, job.Id.ToString(), job.Id == Entity?.JobId));
        }

        Jobs = result;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (JobId != null)
        {
            var job = await Ctx.Jobs.GetByIdAsync(JobId.Value);
            if (job == null) return NotFound();
            Job = job;
            Entity = new PerformedJob { JobId = JobId, Job = Job, Name = Job.Name, TotalCost = Job.TotalPrice };
        }

        await InitializeJobs();
        return Page();
    }

    public override async Task<IActionResult> OnPostAsync()
    {
        await InitializeJobs();

        var result = await base.OnPostAsync();
        if (!Success) return result;
        return RedirectToPage("./ManageItems", new { Entity.Id, AddJobItemsId = Entity.JobId });
    }
}