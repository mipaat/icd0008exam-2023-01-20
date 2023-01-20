using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ItemRepository : AbstractDbRepository<Item>
{
    public ItemRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<Item> ThisDbSet => DbContext.Items;
}