using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService;

public class AppDbContext : DbContext
{
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CutterCNCLog> CutterCNCLogs => Set<CutterCNCLog>();
    public DbSet<LatheLog> LatheLogs => Set<LatheLog>();
    public DbSet<AssemblyLineLog> AssemblyLineLogs => Set<AssemblyLineLog>();
    public DbSet<TestLineLog> TestLineLogs => Set<TestLineLog>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation(
        "Relational:HistoryTableSchema", "coffee_mek"
    );

        modelBuilder.HasDefaultSchema("coffee_mek");

        modelBuilder.Entity<Location>().ToTable("Locations");
        modelBuilder.Entity<Lot>().ToTable("Lots");
        modelBuilder.Entity<Customer>().ToTable("Customers");

        modelBuilder.Entity<CutterCNCLog>().UseTpcMappingStrategy().ToTable("CutterCNCLogs");
        modelBuilder.Entity<LatheLog>().UseTpcMappingStrategy().ToTable("LatheLogs");
        modelBuilder.Entity<AssemblyLineLog>().UseTpcMappingStrategy().ToTable("AssemblyLineLogs");
        modelBuilder.Entity<TestLineLog>().UseTpcMappingStrategy().ToTable("TestLineLogs");
    }

    
}



