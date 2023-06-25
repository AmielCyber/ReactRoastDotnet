namespace ReactRoastDotnet.API.Models.ResponseDto;

public record OrderItemResponseDto
{
    public required ProductItemResponseDto ProductItem { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}