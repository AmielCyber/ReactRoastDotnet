using ErrorOr;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Repositories;

public interface IProductService
{
    public Task<PaginationList<ProductItem>> GetAllItemsAsync(ProductParams productParams);
    public Task<ErrorOr<ProductItem>> GetItemAsync(int id);
    public Task<ErrorOr<ProductItem>> CreateItemAsync(EditProductDto createProductDto);
    public Task<ErrorOr<ProductItem>> EditItemAsync(EditProductDto editProductDto, int id);
    public Task<ErrorOr<Deleted>> DeleteItemAsync(int id);
}