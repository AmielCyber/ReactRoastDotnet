using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data.Configurations;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.Data;

public class AppDbContext : IdentityDbContext<User, CustomRole, int>
{
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ProductItemConfiguration());
    }
}