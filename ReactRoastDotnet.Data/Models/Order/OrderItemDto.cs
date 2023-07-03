using ReactRoastDotnet.Data.Models.ProductItem;

namespace ReactRoastDotnet.Data.Models.Order;

public record OrderItemDto : ProductItemDto
{
    public int Quantity { get; init; }
}