﻿using Domain;

namespace DAL.Filters;

public interface IFilter
{
    public string Identifier { get; }
    public string? DisplayString { get; }
}

public interface IFilter<T> : IFilter where T : AbstractDbEntity
{
    public FilterFunc<T> FilterFunc { get; }
}