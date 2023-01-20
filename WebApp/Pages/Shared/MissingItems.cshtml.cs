using Domain;

namespace WebApp.Pages.Shared;

public interface IMissingItems
{
    public ICollection<ItemWithQuantity> MissingItems { get; }
}