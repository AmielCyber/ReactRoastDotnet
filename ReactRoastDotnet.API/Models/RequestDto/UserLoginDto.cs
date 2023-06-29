using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.API.Models.RequestDto;

public record UserLoginDto
{
    /// <summary>Unique email address from user.</summary>
    [Required] [EmailAddress] public required string Email { get; init; }

    /// <summary>Password that meets identity core's default password requirements</summary>
    [Required] [MinLength(6)] public required string Password { get; init; }
}