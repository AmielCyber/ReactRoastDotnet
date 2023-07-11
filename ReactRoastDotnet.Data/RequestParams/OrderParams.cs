using System.ComponentModel;

namespace ReactRoastDotnet.Data.RequestParams;

public record OrderParams : PaginationParams
{
    /// <summary>Order by: 'price','oldest', or 'newest' (default).</summary>
    [DefaultValue("newest")]
    public string Sort { get; init; } = "newest";
}