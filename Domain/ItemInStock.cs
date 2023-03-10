namespace Domain;

public class ItemInStock : AbstractDbEntity
{
    public string? Location { get; set; }
    public int Quantity { get; set; }

    public int ItemId { get; set; }
    public Item? Item { get; set; }
}