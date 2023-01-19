namespace DAL;

public class RepositoryContext : IDisposable, IAsyncDisposable
{
    private readonly AppDbContext _dbContext;
    
    public RepositoryContext()
    {
        _dbContext = AppDbContextFactory.CreateDbContext();
    }

    // public EntityRepository Entities => new(_dbContext, this);

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