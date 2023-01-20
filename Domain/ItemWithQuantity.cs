namespace Domain;

public record ItemWithQuantity(Item Item, int Quantity)
{
    public Item Item { get; set; } = Item;
    public int Quantity { get; set; } = Quantity;
}