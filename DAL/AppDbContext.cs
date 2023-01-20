using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<Job> Jobs { get; set; } = default!;
    public DbSet<PerformedJob> PerformedJobs { get; set; } = default!;

    public DbSet<JobItem> JobItems { get; set; } = default!;
    public DbSet<UsedItem> UsedItems { get; set; } = default!;
    public DbSet<ItemInStock> ItemInStocks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().Property(i => i.Price).HasPrecision(12, 10);
        modelBuilder.Entity<PerformedJob>().Property(pj => pj.TotalCost).HasPrecision(12, 10);
        
        base.OnModelCreating(modelBuilder);
    }
}