using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.Models.Order;

/// <summary>
/// EditProductDto to edit or create a product item.
/// </summary>
public record EditProductDto
{
    /// <summary>Type of product. Only "Drinks" supported for V.1</summary>
    [Required]
    public required string Type { get; init; } = "Drink";

    /// <summary>The name of the product.</summary>
    [Required]
    [MaxLength(64)]
    public required string Name { get; init; }

    /// <summary>The number of ounces that a drink has if its a drink.</summary>
    public double? Ounces { get; init; }

    /// <summary>Product description.</summary>
    [Required]
    public required string Description { get; init; }

    /// <summary>Price</summary>
    [Required]
    [Range(1.0, Double.MaxValue)]
    public decimal Price { get; init; }

    /// <summary>Image string url.</summary>
    [Required]
    public required string Image { get; init; }

    /// <summary>The image creator from unsplash.com</summary>
    public string ImageCreator { get; init; } = "Unknown";
}