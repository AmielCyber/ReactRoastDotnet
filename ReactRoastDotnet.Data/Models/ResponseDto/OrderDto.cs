namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record OrderDto
{
    public int OrderId { get; init; }

    public required List<OrderItemDto> Items { get; init; }

    public int TotalQuantity { get; init; }

    public decimal TotalPrice { get; init; }

    public required DateTime DateCreated { get; init; }
}