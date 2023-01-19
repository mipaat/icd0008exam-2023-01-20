namespace DAL;

using Filters;
using Domain;
using Microsoft.EntityFrameworkCore;

public abstract class AbstractDbRepository<T> where T : AbstractDbEntity, new()
{
    protected readonly AppDbContext DbContext;
    protected readonly RepositoryContext RepositoryContext;

    protected AbstractDbRepository(AppDbContext dbContext, RepositoryContext repoContext)
    {
        DbContext = dbContext;
        RepositoryContext = repoContext;
    }

    protected abstract DbSet<T> ThisDbSet { get; }
    protected IQueryable<T> Queryable => ThisDbSet;
    public IQueryable<T> QueryableDefault => DefaultIncludes(Queryable);

    protected async Task<ICollection<T>> GetAllAsync(IQueryable<T> queryable, params FilterFunc<T>[] filters)
    {
        var preQueryFilters = FilterFunc.GetQueryConvertible(filters).Select(f => f.Func);
        var postQueryFilters = FilterFunc.GetNonQueryConvertible(filters).Select(f => f.Func).ToList();
        var result = await RunFilters(queryable, preQueryFilters).ToListAsync();
        return RunFilters(result, postQueryFilters).ToList();
    }

    public virtual async Task<ICollection<T>> GetAllAsync(params FilterFunc<T>[] filters)
    {
        return await GetAllAsync(QueryableDefault, filters);
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return QueryableDefault.FirstOrDefaultAsync(t => id.Equals(t.Id));
    }

    public T? GetById(int id)
    {
        return QueryableDefault.FirstOrDefault(t => id.Equals(t.Id));
    }

    public virtual void Add(T entity)
    {
        ThisDbSet.Add(RunPreSaveActions(entity));
    }

    public void Update(params T[] entities)
    {
        ThisDbSet.UpdateRange(RunPreSaveActions(entities));
    }

    public T? Remove(int id)
    {
        var entity = GetById(id);
        return entity == null ? entity : Remove(entity);
    }
    
    public async Task<T?> RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity == null ? entity : Remove(entity);
    }

    public virtual T Remove(T entity)
    {
        return ThisDbSet.Remove(RunPreSaveActions(entity)).Entity;
    }

    public bool Exists(int id)
    {
        return Queryable.Any(t => id.Equals(t.Id));
    }

    public bool ExistsAsync(int id)
    {
        return Queryable.Any(t => id.Equals(t.Id));
    }

    public Type EntityType => typeof(T);

    private static T RunActions(T entity, IEnumerable<Action<T>> actions)
    {
        foreach (var action in actions) action(entity);

        return entity;
    }

    private static ICollection<T> RunActions(ICollection<T> entities, ICollection<Action<T>> actions)
    {
        foreach (var entity in entities) RunActions(entity, actions);
        return entities;
    }

    private T RunPreSaveActions(T entity)
    {
        return RunActions(entity, PreSaveActions);
    }

    private ICollection<T> RunPreSaveActions(params T[] entities)
    {
        return RunActions(entities, PreSaveActions);
    }

    protected IEnumerable<T> RunFilters(IEnumerable<T> enumerable, IEnumerable<FilterFuncInner<T>> filters)
    {
        return RunFilters(enumerable.AsQueryable(), filters);
    }

    protected IQueryable<T> RunFilters(IQueryable<T> queryable, IEnumerable<FilterFuncInner<T>> filters)
    {
        foreach (var filter in filters)
        {
            queryable = filter.Invoke(queryable);
        }

        return queryable;
    }

    protected virtual ICollection<Action<T>> PreSaveActions => new List<Action<T>>();

    protected virtual IQueryable<T> DefaultIncludes(IQueryable<T> queryable)
    {
        return queryable;
    }
}