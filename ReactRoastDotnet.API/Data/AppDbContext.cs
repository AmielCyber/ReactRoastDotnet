using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.API.Entities;

namespace ReactRoastDotnet.API.Data;

public class AppDbContext: DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
   public DbSet<ProductItem> ProductItems { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Cart> Carts { get; set; }
   public DbSet<Order> Orders { get; set; }
}