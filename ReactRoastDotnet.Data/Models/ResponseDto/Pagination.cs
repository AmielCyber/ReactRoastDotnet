namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record Pagination
{
    public int CurrentPage { get; init; } = 1;
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}