namespace Domain;

public class Category : AbstractDbEntity
{
    public string Name { get; set; } = default!;
    
    public ICollection<Item>? Items { get; set; }
}