using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PerformedJobRepository : AbstractDbRepository<PerformedJob>
{
    public PerformedJobRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<PerformedJob> ThisDbSet => DbContext.PerformedJobs;
}