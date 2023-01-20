using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ItemRepository : AbstractDbRepository<Item>
{
    public ItemRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<Item> ThisDbSet => DbContext.Items;

    protected override IQueryable<Item> DefaultIncludes(IQueryable<Item> queryable)
    {
        return queryable
            .Include(i => i.Category)
            .Include(i => i.ItemInStocks)
            .Include(i => i.JobItems)!
            .ThenInclude(ji => ji.Job);
    }
}