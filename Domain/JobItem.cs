namespace Domain;

public class JobItem : AbstractDbEntity
{
    public int Quantity { get; set; }
    
    public int JobId { get; set; }
    public Job? Job { get; set; }
    public int ItemId { get; set; }
    public Item? Item { get; set; }
}