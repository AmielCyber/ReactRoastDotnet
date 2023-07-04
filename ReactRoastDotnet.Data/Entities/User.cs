using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ReactRoastDotnet.Data.Entities;

public class User : IdentityUser<int>
{
    [Required] public override required string? Email { get; set; }
    [Required] [MaxLength(64)] public required string FirstName { get; set; }

    [Required] [MaxLength(64)] public required string LastName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public required DateTime DateCreated { get; set; }

    // Relationships.

    // Has at most one cart.
    public Cart? Cart { get; set; }

    // May have one/many orders.
    public ICollection<Order>? Orders { get; set; }
}