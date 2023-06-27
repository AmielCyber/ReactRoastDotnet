namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record CartItemDto : ProductItemDto
{
    public int Quantity { get; init; }
}