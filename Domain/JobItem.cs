namespace Domain;

public class JobItem : AbstractDbEntity
{
    public int Quantity { get; set; } = 1;
    
    public int JobId { get; set; }
    public Job? Job { get; set; }
    public int ItemId { get; set; }
    public Item? Item { get; set; }
}