using DAL.Filters;

namespace WebApp.MyLibraries.ModelBinders;

public class PerformedJobCompletionFilterModelBinder : CustomModelBinder<PerformedJobCompletionFilter>
{
    protected override PerformedJobCompletionFilter DefaultValue => PerformedJobCompletionFilter.Default;

    protected override Func<string?, PerformedJobCompletionFilter?> Construct =>
        s => PerformedJobCompletionFilter.Construct(s, PerformedJobCompletionFilter.Values);
}