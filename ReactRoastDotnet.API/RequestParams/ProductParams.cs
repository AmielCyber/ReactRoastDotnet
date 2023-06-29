using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.API.RequestParams;

public record ProductParams: PaginationParams
{
    /// <summary>Order by: 'name' or 'popular'.</summary>
    public string? Sort { get; init; }

    /// <summary>Search the name of a drink</summary>
    public string? DrinkName { get; init; }
}