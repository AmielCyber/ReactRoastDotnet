namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record ProductItemDetailedDto : ProductItemDto
{
    public double? Ounces { get; init; }

    public required string Description { get; init; }
}