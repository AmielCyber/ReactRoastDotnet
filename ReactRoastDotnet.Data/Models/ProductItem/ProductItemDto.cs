namespace ReactRoastDotnet.Data.Models.ProductItem;

/// <summary>The product details for a cart or order item.</summary>
public record ProductItemDto
{
    /// <summary>Unique product Id.</summary>
    public int Id { get; init; }

    /// <summary>Type of product. As of now only "Drinks" are available.</summary>
    public required string Type { get; init; }

    /// <summary>Product's name</summary>
    public required string Name { get; init; }

    /// <summary>Price for this product when ordered or currently if in cart.</summary>
    public decimal Price { get; init; }

}