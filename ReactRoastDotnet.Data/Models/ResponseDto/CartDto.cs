namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record CartDto
{
    public List<CartItemDto> Items { get; init; } = new();

    public required DateTime DateCreated { get; init; }
}