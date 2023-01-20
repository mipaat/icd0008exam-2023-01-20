using Domain;

namespace DAL.Filters;

public class PerformedJobCompletionFilter : SimpleFilter<PerformedJob>
{
    public PerformedJobCompletionFilter(string identifier, FilterFuncInner<PerformedJob> filterFunc,
        bool isConvertibleToDbQuery = true, string? displayString = null) : base(identifier, filterFunc,
        isConvertibleToDbQuery, displayString)
    {
    }

    public static readonly PerformedJobCompletionFilter Finished = new(nameof(Finished),
        iq => iq.Where(pj => pj.Performed != null));

    public static readonly PerformedJobCompletionFilter Pending = new(nameof(Pending),
        iq => iq.Where(pj => pj.Performed == null));

    public static readonly PerformedJobCompletionFilter All = new(nameof(All), iq => iq);

    public static PerformedJobCompletionFilter Default => Pending;

    public static readonly List<PerformedJobCompletionFilter> Values = new List<PerformedJobCompletionFilter>
        { Pending, Finished, All };
}