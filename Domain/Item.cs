using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Item : AbstractDbEntity
{
    public string Name { get; set; } = default!;
    public int OptimalQuantity { get; set; }
    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<ItemInStock>? ItemInStocks { get; set; }
    [NotMapped] public int? Quantity => ItemInStocks?.Sum(iis => iis.Quantity);
    
    public ICollection<JobItem>? JobItems { get; set; }
    [NotMapped] public ICollection<Job>? Jobs => JobItems?.Select(ji => ji.Job!).ToList();
}