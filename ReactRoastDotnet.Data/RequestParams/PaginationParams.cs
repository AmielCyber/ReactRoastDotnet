using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.Data.RequestParams;

/// <summary>
/// Pagination query params.
/// </summary>
public record PaginationParams
{
    /// <summary>Page size per page.</summary>
    [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
    [DefaultValue(6)]
    public int PageSize { get; set; } = 6;

    /// <summary>Go to page number.</summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page Number must be greater than 0.")]
    [DefaultValue(1)]
    public int PageNumber { get; init; } = 1;
}