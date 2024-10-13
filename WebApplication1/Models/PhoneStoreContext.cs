using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class PhoneStoreContext : DbContext
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Order> Orders { get; set; }
    public PhoneStoreContext(DbContextOptions<PhoneStoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}