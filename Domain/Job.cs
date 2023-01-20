using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Job : AbstractDbEntity
{
    public string Name { get; set; } = default!;

    public ICollection<JobItem>? JobItems { get; set; }
    [NotMapped] public ICollection<Item>? Items => JobItems?.Select(ji => ji.Item!).ToList();
    [DisplayName("Total price")] [NotMapped] public decimal? TotalCost => JobItems?.Sum(ji => ji.TotalCost);

    public ICollection<ItemWithQuantity> MissingItems(ICollection<Item> items)
    {
        var result = new List<ItemWithQuantity>();
        foreach (var jobItem in JobItems!)
        {
            if (jobItem.Quantity <= 0) continue;
            var item = items.FirstOrDefault(i => i.Id == jobItem.ItemId);
            var quantity = item?.Quantity ?? 0;
            var quantityDifference = quantity - jobItem.Quantity;
            if (quantityDifference < 0)
            {
                result.Add(new ItemWithQuantity(jobItem.Item!, -quantityDifference));
            }
        }

        return result;
    }
}