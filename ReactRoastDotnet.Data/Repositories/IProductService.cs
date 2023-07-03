using ErrorOr;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Repositories;

public interface IProductService
{
    public Task<PaginationList<ProductItem>> GetAllProductItemsAsync(ProductParams productParams);
    public Task<ErrorOr<ProductItem>> GetProductItemAsync(int id);
    public Task<ErrorOr<ProductItem>> CreateNewProductItemAsync(EditProductDto createProductDto);
    public Task<ErrorOr<ProductItem>> EditExistingProductItemAsync(EditProductDto editProductDto, int id);
    public Task<ErrorOr<Deleted>> DeleteProductItemAsync(int id);
}