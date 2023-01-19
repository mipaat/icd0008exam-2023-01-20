using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.MyLibraries.PageExamples;

public interface IExampleEntity
{
    public int HiddenProperty1 { get; set; }
    public string HiddenProperty2 { get; set; }
    public string Property { get; set; }
    public bool BooleanProperty { get; set; }
}

public class Edit : PageModel
{
    [BindProperty(SupportsGet = true)] public int Id { get; set; }
    [BindProperty] public IExampleEntity Entity { get; set; } = default!;
}