using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(64)] public required string Email { get; set; }

    [Required] [MaxLength(64)] public required string Password { get; set; }

    [Required] [MaxLength(64)] public required string FirstName { get; set; }

    [Required] [MaxLength(64)] public required string LastName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime DateCreated { get; set; }

    // Has one cart.
    public virtual Cart? Cart { get; set; }

    // Has many orders.
    public virtual List<Order>? Orders { get; set; }
}