using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReactRoastDotnet.API.RequestParams;

public record PaginationParams
{
    [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
    [DefaultValue(6)]
    public int PageSize { get; set; } = 6;

    [Range(1, int.MaxValue, ErrorMessage = "Page Number must be greater than 0.")]
    [DefaultValue(1)]
    public int PageNumber { get; init; } = 1;
}