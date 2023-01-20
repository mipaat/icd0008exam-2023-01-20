using DAL.Filters;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ItemInStockRepository : AbstractDbRepository<ItemInStock>
{
    public ItemInStockRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<ItemInStock> ThisDbSet => DbContext.ItemInStocks;

    public async Task<ICollection<ItemInStock>> GetAllByItemId(int itemId)
    {
        return await GetAllAsync(new FilterFunc<ItemInStock>(iq => iq
            .Where(iis => iis.ItemId == itemId), true));
    }

    public async Task AddAsync(int itemId, int amount)
    {
        var items = await GetAllByItemId(itemId);
        var item = items.FirstOrDefault();
        if (item == null)
        {
            Add(new ItemInStock { ItemId = itemId, Quantity = amount });
            return;
        }

        item.Quantity += amount;
        Update(item);
    }

    public async Task<int> RemoveAsync(int itemId, int amount)
    {
        var items = await GetAllByItemId(itemId);

        foreach (var item in items)
        {
            var amountToRemove = Math.Min(amount, item.Quantity);
            item.Quantity -= amountToRemove;
            amount -= amountToRemove;
        }

        return amount;
    }
}