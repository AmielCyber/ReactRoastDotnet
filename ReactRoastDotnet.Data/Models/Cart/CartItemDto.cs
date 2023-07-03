using ReactRoastDotnet.Data.Models.ProductItem;

namespace ReactRoastDotnet.Data.Models.Cart;

/// <summary>Cart item containing the product details</summary>
public record CartItemDto : ProductItemDto
{
    /// <summary>Quantity for this product.</summary>
    public int Quantity { get; init; }
}