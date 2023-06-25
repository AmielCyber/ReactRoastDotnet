using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.Entities;

public class ProductItem
{
    [Required] public int Id { get; set; }
    
    [Required] public required ProductType ProductType { get; set; }

    [Required] [MaxLength(64)] public required string Name { get; set; }

    public double? Ounces { get; set; }

    [Required] public required string Description { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public required string Image { get; set; }

    public string? ImageCreator { get; set; }

}