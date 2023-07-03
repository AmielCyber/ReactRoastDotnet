using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.API.Models.RequestDto;

public record OrderRequestDto
{
    [EmailAddress]
    [Required]
    public required string Email { get; set; }
}