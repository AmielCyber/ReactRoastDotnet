using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.Data.Extensions;

public static class OrderItemsExtensions
{
    public static IQueryable<Order> Sort(this IQueryable<Order> query, string? sortBy)
    {
        // SQLite does not support sorting by decimal type.
        query = sortBy switch
        {
            "newest" => query.OrderByDescending(o => o.DateCreated),
            "oldest" => query.OrderBy(o => o.DateCreated),
            "price" => query.OrderBy(o => o.TotalPrice),
            // Required to avoid sql warnings.
            _ => query.OrderByDescending(o => o.Id),
        };

        return query;
    }
}