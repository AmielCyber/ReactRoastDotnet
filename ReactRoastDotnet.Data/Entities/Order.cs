using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[Index(nameof(UserEmail))]
public class Order
{
    [Required] public int Id { get; set; }

    // Belongs to a user.
    public int? UserId { get; set; }
    public virtual User? User { get; set; }

    [Required] [MaxLength(64)] public required string UserEmail { get; set; }

    [Required] public int TotalQuantity { get; set; }

    [Required] public decimal TotalPrice { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required DateTime DateCreated { get; set; }


    // Has many order items.
    public virtual ICollection<OrderItem>? OrderItems { get; set; } 
}