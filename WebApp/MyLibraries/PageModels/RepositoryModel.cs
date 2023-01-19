using DAL;
using Domain;

namespace WebApp.MyLibraries.PageModels;

public abstract class RepositoryModel<T> : DbPageModel where T : AbstractDbEntity, new()
{
    protected RepositoryModel(RepositoryContext ctx) : base(ctx)
    {
    }

    protected abstract AbstractDbRepository<T> Repository { get; }
}