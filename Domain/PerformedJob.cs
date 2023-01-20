using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class PerformedJob : AbstractDbEntity
{
    public string Name { get; set; } = default!;
    public decimal? TotalCost { get; set; }
    public DateTime? Performed { get; set; }
    public string? Context { get; set; }

    public int? JobId { get; set; }
    [DisplayName("Base job")] public Job? Job { get; set; }

    public ICollection<UsedItem>? UsedItems { get; set; }
    [NotMapped] public ICollection<Item>? Items => UsedItems?.Select(ui => ui.Item!).ToList();

    public ICollection<ItemWithQuantity> MissingItems(ICollection<Item> items)
    {
        var result = new List<ItemWithQuantity>();
        foreach (var usedItem in UsedItems!)
        {
            if (usedItem.Quantity <= 0) continue;
            var item = items.FirstOrDefault(i => i.Id == usedItem.ItemId);
            var quantity = item?.Quantity ?? 0;
            var quantityDifference = quantity - usedItem.Quantity;
            if (quantityDifference < 0)
            {
                result.Add(new ItemWithQuantity(usedItem.Item!, -quantityDifference));
            }
        }

        return result;
    }

    [NotMapped] public decimal? TotalItemsCost => UsedItems?.Sum(ui => ui.TotalCost);
    [NotMapped] public decimal? Cost => TotalCost ?? TotalItemsCost;
}