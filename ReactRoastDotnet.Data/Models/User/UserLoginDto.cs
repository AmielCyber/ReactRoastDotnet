using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.Models.User;

/// <summary>
/// Login object from a request body.
/// </summary>
public record UserLoginDto
{
    /// <summary>Unique email address from user.</summary>
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    /// <summary>Password that meets identity core's default password requirements</summary>
    [Required]
    [MinLength(6)]
    public required string Password { get; init; }
}