using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class JobItemRepository : AbstractDbRepository<JobItem>
{
    public JobItemRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<JobItem> ThisDbSet => DbContext.JobItems;
}