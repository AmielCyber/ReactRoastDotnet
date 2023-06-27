namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record ProductItemDto
{
    public int Id { get; init; }

    public required string Type { get; init; }

    public required string Name { get; init; }

    public decimal Price { get; init; }

    public required string Image { get; init; }

    public required string ImageCreator { get; init; }
}