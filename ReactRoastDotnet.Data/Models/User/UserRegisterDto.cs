using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.Models.User;

/// <summary>
/// User registration object from the request body.
/// </summary>
public record UserRegisterDto : UserLoginDto
{
    /// <summary>A non-empty first name.</summary>
    [Required]
    [MaxLength(64)]
    public required string FirstName { get; init; }

    /// <summary>A non-empty last name.</summary>
    [Required]
    [MaxLength(64)]
    public required string LastName { get; init; }
}