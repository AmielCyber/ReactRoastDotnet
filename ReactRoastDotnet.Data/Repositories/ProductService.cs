using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data.Common.Errors;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Extensions;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Repositories;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationList<ProductItem>> GetAllItemsAsync(ProductParams productParams)
    {
        // Set up request query from user.
        int skipToPageNumber = (productParams.PageNumber - 1) * productParams.PageSize;

        var query = _context.ProductItems
            .AsNoTracking()
            .SearchDrink(productParams.DrinkName)
            .Sort(productParams.Sort)
            .Skip(skipToPageNumber)
            .Take(productParams.PageSize)
            .AsQueryable();

        // Set up pagination.
        int totalCount = await _context.ProductItems.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)productParams.PageSize);

        var pagination = new Pagination(
            productParams.PageSize,
            totalCount,
            totalPages,
            productParams.PageNumber
        );

        List<ProductItem> productItems = await query.ToListAsync();

        return new PaginationList<ProductItem>(productItems, pagination);
    }

    public async Task<ErrorOr<ProductItem>> GetItemAsync(int id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem is null)
        {
            return Errors.Product.NotFound($"Product item with id: {id} was not found.");
        }

        return productItem;
    }

    public async Task<ErrorOr<ProductItem>> CreateItemAsync(EditProductDto createProductDto)
    {
        var productItem = MapCreateProductToProductItem(createProductDto);
        var newProductItem = _context.ProductItems.Add(productItem);

        var saved = await _context.SaveChangesAsync() > 0;

        if (!saved)
        {
            return Errors.Product.FailedToSaveChanges("Problem creating new product.");
        }

        return newProductItem.Entity;
    }

    public async Task<ErrorOr<ProductItem>> EditItemAsync(EditProductDto editProductDto, int id)
    {
        var existingProductItem = await _context.ProductItems.FindAsync(id);
        if (existingProductItem is null)
        {
            return Errors.Product.NotFound($"Product item with id: {id} was not found.");
        }

        existingProductItem.UpdateProductItem(editProductDto);

        var saved = await _context.SaveChangesAsync() > 0;

        if (!saved)
        {
            return Errors.Product.FailedToSaveChanges("Problem saving edit product.");
        }

        return existingProductItem;
    }

    public async Task<ErrorOr<Deleted>> DeleteItemAsync(int id)
    {
        ProductItem? productItem = await _context.ProductItems.FirstOrDefaultAsync(item => item.Id == id);

        if (productItem is null)
        {
            return Errors.Product.NotFound($"Product item with id: {id} was not found.");
        }

        _context.ProductItems.Remove(productItem);
        var saved = await _context.SaveChangesAsync() > 0;

        if (!saved)
        {
            return Errors.Product.FailedToSaveChanges("Problem deleting product.");
        }

        return Result.Deleted;
    }

    private static ProductItem MapCreateProductToProductItem(EditProductDto createProductDto)
    {
        return new ProductItem
        {
            Type = createProductDto.Type,
            Name = createProductDto.Name,
            Ounces = createProductDto.Ounces,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            Image = createProductDto.Image,
            ImageCreator = createProductDto.ImageCreator,
        };
    }
}