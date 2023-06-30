using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactRoastDotnet.Data.Entities;

public class Cart
{
    // Has one user.
    [Key] public int UserId { get; set; }
    public User User { get; set; } = null!;

    // TODO: Add generation for SQL Server / PostgresSQL
    public DateTime LastModified { get; set; } = DateTime.Now;

    // Has many cart items.
    public List<CartItem> Items { get; set; } = new();
}