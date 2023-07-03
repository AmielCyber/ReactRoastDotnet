namespace ReactRoastDotnet.Data.Models.Order;

public record OrderDto
{
    public int Id { get; init; }

    public required List<OrderItemDto> OrderItems { get; init; }

    public int TotalQuantity { get; init; }

    public decimal TotalPrice { get; init; }

    public required DateTime DateCreated { get; init; }
}