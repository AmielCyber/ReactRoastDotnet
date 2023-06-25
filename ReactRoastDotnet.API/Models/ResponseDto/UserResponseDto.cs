namespace ReactRoastDotnet.API.Models.ResponseDto;

public record UserResponseDto
{
    public int Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime DateCreated { get; init; }
}