namespace ReactRoastDotnet.Data.Models.Pagination;

public record PaginationList<T>(List<T> Items, Pagination Pagination);