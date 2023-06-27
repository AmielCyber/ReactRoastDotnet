using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactRoastDotnet.Data.Entities;

public class Cart
{
    // Has one user.
    [Required] [Key] public int UserId { get; set; }
    public User User { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime DateCreated { get; set; }

    // Relationships.

    // Has many cart items.
    public virtual ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
}