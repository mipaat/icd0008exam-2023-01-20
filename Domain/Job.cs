using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Job : AbstractDbEntity
{
    public string Name { get; set; } = default!;

    public ICollection<JobItem>? JobItems { get; set; }
    [NotMapped] public ICollection<Item>? Items => JobItems?.Select(ji => ji.Item!).ToList();
    [DisplayName("Total price")] [NotMapped] public decimal? TotalPrice => Items?.Sum(i => i.Price);
}