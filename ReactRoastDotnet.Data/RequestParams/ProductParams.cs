namespace ReactRoastDotnet.Data.RequestParams;

/// <summary>
/// Requested query for products to return from user.
/// </summary>
public record ProductParams : PaginationParams
{
    /// <summary>Order by: 'name','popular', or 'price'.</summary>
    public string? Sort { get; init; }

    /// <summary>Search the name of a drink</summary>
    public string? DrinkName { get; init; }
}