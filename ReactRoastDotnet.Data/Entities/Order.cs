using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[Index(nameof(Email))]
public class Order
{
    public int Id { get; set; }

    // Belongs to a user.
    public int? UserId { get; set; }
    public User? User { get; set; }

    [Required] [MaxLength(256)] public required string Email { get; set; }

    [Required] public int TotalQuantity { get; set; }

    [Precision(18, 2)]
    [Required] public decimal TotalPrice { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;


    // Has many order items.
    public ICollection<OrderItem> Items { get; set; } = null!;
}