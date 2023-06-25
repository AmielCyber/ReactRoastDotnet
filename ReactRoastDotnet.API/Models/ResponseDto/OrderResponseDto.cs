namespace ReactRoastDotnet.API.Models.ResponseDto;

public record OrderResponseDto
{
    public required List<OrderItemResponseDto> OrderItems { get; init; }
    public required DateTime DateCreated { get; set; }
}