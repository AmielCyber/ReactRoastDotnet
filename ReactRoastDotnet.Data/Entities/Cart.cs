using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.Entities;

public class Cart
{
    // Has one user.
    [Key] public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    // Has many cart items.
    public List<CartItem> Items { get; set; } = new();
}