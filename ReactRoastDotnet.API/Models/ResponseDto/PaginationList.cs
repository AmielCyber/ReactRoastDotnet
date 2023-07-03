using ReactRoastDotnet.Data.Models.ResponseDto;

namespace ReactRoastDotnet.API.Models.ResponseDto;

public record PaginationList<T>(List<T> Items, Pagination Pagination);