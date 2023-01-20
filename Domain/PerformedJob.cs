using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class PerformedJob : AbstractDbEntity
{
    public string Name { get; set; } = default!;
    public decimal? TotalCost { get; set; }
    
    public int? JobId { get; set; }
    public Job? Job { get; set; }

    public ICollection<UsedItem>? UsedItems { get; set; }
    [NotMapped] public ICollection<Item>? Items => UsedItems?.Select(ui => ui.Item!).ToList();
}