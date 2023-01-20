using DAL.Repositories;

namespace DAL;

public class RepositoryContext : IDisposable, IAsyncDisposable
{
    private readonly AppDbContext _dbContext;
    
    public RepositoryContext()
    {
        _dbContext = AppDbContextFactory.CreateDbContext();
    }

    public CategoryRepository Categories => new(_dbContext, this);
    public ItemInStockRepository ItemInStocks => new(_dbContext, this);
    public ItemRepository Items => new(_dbContext, this);
    public JobItemRepository JobItems => new(_dbContext, this);
    public JobRepository Jobs => new(_dbContext, this);
    public PerformedJobRepository PerformedJobs => new(_dbContext, this);
    public UsedItemRepository UsedItems => new(_dbContext, this);

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RepositoryContext()
    {
        Dispose(false);
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}