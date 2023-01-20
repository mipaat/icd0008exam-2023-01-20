using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class UsedItem : AbstractDbEntity
{
    public int Quantity { get; set; } = 1;
    
    public int PerformedJobId { get; set; }
    public PerformedJob? PerformedJob { get; set; }
    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public UsedItem()
    {
    }

    public UsedItem(JobItem jobItem, int performedJobId)
    {
        Quantity = jobItem.Quantity;
        ItemId = jobItem.ItemId;
        Item = jobItem.Item;
        PerformedJobId = performedJobId;
    }

    [NotMapped] public decimal? TotalCost => Quantity * Item?.Price;
}