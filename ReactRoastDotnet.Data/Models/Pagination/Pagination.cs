namespace ReactRoastDotnet.Data.Models.Pagination;

/// <summary>
/// Pagination result that we will return to client when the request a list of products or orders.
/// </summary>
/// <param name="PageSize">The number of items per page.</param>
/// <param name="TotalCount">The total number of items in the list from the DB.</param>
/// <param name="TotalPages">Total pages with the passed page size.</param>
/// <param name="CurrentPage">The current page to be viewed.</param>
public record Pagination(int PageSize, int TotalCount, int TotalPages, int CurrentPage = 1);