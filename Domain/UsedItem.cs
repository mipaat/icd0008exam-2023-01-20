namespace Domain;

public class UsedItem : AbstractDbEntity
{
    public int Quantity { get; set; }
    
    public int PerformedJobId { get; set; }
    public PerformedJob? PerformedJob { get; set; }
    public int ItemId { get; set; }
    public Item? Item { get; set; }
}