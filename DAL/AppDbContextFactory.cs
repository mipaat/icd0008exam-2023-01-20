using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public static AppDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        ConfigureOptions(optionsBuilder);

        return new AppDbContext(optionsBuilder.Options);
    }
    
    public AppDbContext CreateDbContext(string[] args)
    {
        return CreateDbContext();
    }

    public static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
    {
        ConfigureOptions(optionsBuilder, false);
    }

    public static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder, bool debug)
    {
        var directorySeparator = Path.DirectorySeparatorChar;
        var sqliteRepoDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                  directorySeparator + "icd0008exam" + directorySeparator + "exam2" + directorySeparator;
        Directory.CreateDirectory(sqliteRepoDirectory);
        optionsBuilder.UseSqlite($"Data source={sqliteRepoDirectory}exam.db");
        if (debug) optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder
            .AddFilter("Microsoft", LogLevel.Debug)
            .AddFilter("System", LogLevel.Debug)
            .AddConsole();
    });
}