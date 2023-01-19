using Domain;

namespace DAL.Filters;

public delegate IQueryable<T> FilterFuncInner<T>(IQueryable<T> arg) where T : AbstractDbEntity;

public static class FilterFunc
{
    public static IEnumerable<FilterFunc<T>> GetQueryConvertible<T>(IEnumerable<FilterFunc<T>> filters)
        where T : AbstractDbEntity =>
        filters.Where(f => f.IsConvertibleToDbQuery);

    public static IEnumerable<FilterFunc<T>> GetNonQueryConvertible<T>(IEnumerable<FilterFunc<T>> filters)
        where T : AbstractDbEntity =>
        filters.Where(f => !f.IsConvertibleToDbQuery);
}

public class FilterFunc<T> where T : AbstractDbEntity
{
    public FilterFunc(FilterFuncInner<T> func, bool isConvertibleToDbQuery)
    {
        Func = func;
        IsConvertibleToDbQuery = isConvertibleToDbQuery;
    }

    public readonly FilterFuncInner<T> Func;
    public bool IsConvertibleToDbQuery { get; }
    public IQueryable<T> Filter(IQueryable<T> queryable) => Func.Invoke(queryable);
}