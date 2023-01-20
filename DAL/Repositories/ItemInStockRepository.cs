using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ItemInStockRepository : AbstractDbRepository<ItemInStock>
{
    public ItemInStockRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<ItemInStock> ThisDbSet => DbContext.ItemInStocks;
}