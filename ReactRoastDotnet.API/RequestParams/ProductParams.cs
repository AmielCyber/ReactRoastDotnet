namespace ReactRoastDotnet.API.RequestParams;

/// <summary>
/// Requested query for products to return from user.
/// </summary>
public record ProductParams : PaginationParams
{
    // TODO: RegEx to check accepted values?
    /// <summary>Order by: 'name' or 'popular'.</summary>
    public string? Sort { get; init; }

    /// <summary>Search the name of a drink</summary>
    public string? DrinkName { get; init; }
}