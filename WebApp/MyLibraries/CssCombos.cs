using Microsoft.AspNetCore.Html;

namespace WebApp.MyLibraries;

public static class CssCombos
{
    public static HtmlString ExternalContainer => new("text-center rounded-3 border border-1 border-info gap-1 p-3");
    public static HtmlString InnerContainer => new("rounded-3 bg-info bg-opacity-25 p-3");
}