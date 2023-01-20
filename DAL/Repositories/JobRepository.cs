using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class JobRepository : AbstractDbRepository<Job>
{
    public JobRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<Job> ThisDbSet => DbContext.Jobs;
}