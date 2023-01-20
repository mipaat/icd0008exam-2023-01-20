using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PerformedJobRepository : AbstractDbRepository<PerformedJob>
{
    public PerformedJobRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<PerformedJob> ThisDbSet => DbContext.PerformedJobs;

    protected override IQueryable<PerformedJob> DefaultIncludes(IQueryable<PerformedJob> queryable)
    {
        return queryable
            .Include(pj => pj.Job)
            .Include(pj => pj.UsedItems)!
            .ThenInclude(ui => ui.Item)
            .ThenInclude(i => i!.Category);
    }
}