using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.API.Models.ResponseDto;

public record ProductItemResponseDto
{
    public int Id { get; set; }
    
    public required ProductType ProductType { get; set; }

    public required string Name { get; set; }

    public double? Ounces { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public required string Image { get; set; }

    public string? ImageCreator { get; set; }
}