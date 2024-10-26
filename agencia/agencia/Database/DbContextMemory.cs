using agencia.Models;
using Microsoft.EntityFrameworkCore;

namespace agencia.Database;

public class DbContextMemory : DbContext
{
    public DbContextMemory(DbContextOptions<DbContextMemory> options)
        : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Travel> Travels { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TravelPackage> TravelPackages { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("AgenciaDb");
        }
    }
    
}