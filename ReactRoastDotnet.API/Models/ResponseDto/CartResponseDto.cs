namespace ReactRoastDotnet.API.Models.ResponseDto;

public record CartResponseDto
{
    public List<CartItemResponseDto> CartItems { get; init; } = new List<CartItemResponseDto>();
}