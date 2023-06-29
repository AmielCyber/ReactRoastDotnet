using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.Data.Models.ResponseDto;

public record ProductItemListDto(List<ProductItem> Items, Pagination Pagination);