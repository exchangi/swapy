#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace swapy.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; } 
    public DbSet<Product> Products { get; set; } 
    public DbSet<Category> Categories { get; set; } 
    public DbSet<Association> Associations { get; set; }
    public DbSet<Order> Orders { get; set; } 
    public DbSet<Swap> Swaps { get; set; } 


}
