using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactRoastDotnet.API.Entities;

public class Cart
{
    // Has one user.
    [Required] [Key] public int UserId { get; set; }
    public virtual User? User { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime DateCreated { get; set; }

    // Has many cart items.
    public virtual ICollection<CartItem>? CartItems { get; set; }
}