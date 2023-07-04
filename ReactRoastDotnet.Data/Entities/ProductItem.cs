using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

/// <summary>
/// Product item in our database.
/// </summary>
public class ProductItem
{
    public int Id { get; set; }

    /// <summary>Type of product. Only "Drinks" supported for V.1</summary>
    [Required]
    public required string Type { get; set; }

    /// <summary>The name of the product.</summary>
    [Required]
    [MaxLength(64)]
    public required string Name { get; set; }

    /// <summary>The number of ounces that a drink has if its a drink.</summary>
    public double? Ounces { get; set; }

    /// <summary>Product description.</summary>
    [Required]
    public required string Description { get; set; }

    /// <summary>Price</summary>
    [Required]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    /// <summary>Image string url.</summary>
    [Required]
    public required string Image { get; set; }

    /// <summary>The image creator from unsplash.com</summary>
    public string ImageCreator { get; set; } = "Unknown";
}