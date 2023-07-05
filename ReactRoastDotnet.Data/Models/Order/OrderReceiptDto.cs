namespace ReactRoastDotnet.Data.Models.Order;

public record OrderReceiptDto
{
    public int OrderNumber { get; init; }
    
    public required string Email { get; init; }
    
    public required List<CheckoutItem> Items { get; init; }

    public int TotalQuantity { get; init; }
    public decimal TotalPrice { get; init; }

    public DateTime OrderDate { get; init; } = DateTime.UtcNow;
    public DateTime EstimateReadyTime { get; init; } = DateTime.UtcNow.AddMinutes(10);
}