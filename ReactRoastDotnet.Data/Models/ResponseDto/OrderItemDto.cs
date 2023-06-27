namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record OrderItemDto : ProductItemDto
{
    public int Quantity { get; init; }
}