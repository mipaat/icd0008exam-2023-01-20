using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UsedItemRepository : AbstractDbRepository<UsedItem>
{
    public UsedItemRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<UsedItem> ThisDbSet => DbContext.UsedItems;
}