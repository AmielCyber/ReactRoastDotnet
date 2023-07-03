namespace ReactRoastDotnet.Data.Models.Cart;

/// <summary>
/// Cart Data Transfer Object to send to the user for all GET and POST to the
/// cart endpoint.
/// </summary>
public record CartDto
{
    /// <summary>Item list of products.</summary>
    public List<CartItemDto> Items { get; init; } = new();

    /// <summary>Last modified from a CRUD operation</summary>
    public required DateTime LastModified { get; init; }
}