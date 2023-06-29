using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.Data.Models.ResponseDto;

/// <summary>
/// Product List request from user.
/// Product List will be sorted and search for name of product based on user params.
/// </summary>
/// <param name="Items">Product items</param>
/// <param name="Pagination">Page navigation object</param>
public record ProductItemListDto(
    List<ProductItem> Items,
    Pagination Pagination
);