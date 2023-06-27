using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.API.Models.RequestDto;

public record UserLoginDto
{
    [Required] public required string Email { get; init; }
    [Required] public required string Password { get; init; }
}