namespace Domain;

public class Job : AbstractDbEntity
{
    public string Name { get; set; } = default!;
    
    public ICollection<JobItem>? JobItems { get; set; }
}