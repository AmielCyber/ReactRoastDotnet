using System.Data.Common;
using ErrorOr;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.Repositories;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Tests.RepositoryTests;

public class ProductServiceTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    public ProductServiceTest()
    {
        // FROM: https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database#inmemory-provider
        // Create and open a connection. This creates the SQLite in-memory database,
        // which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        // These options will be used by the context instances in this test suite,
        // including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;
        
        // Create the schema and seed some data
        using var context = new AppDbContext(_contextOptions);

        context.Database.EnsureCreated();
    }
    
    [Fact]
    public async Task GetAllItemsAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var context = CreateContext();
        var productService = new ProductService(context);
        var productParams = new ProductParams();

        // Act.
        var result = await productService.GetAllItemsAsync(productParams);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginationList<ProductItem>>(result);
        Assert.IsType<Pagination>(result.Pagination);
        Assert.IsType<List<ProductItem>>(result.Items);
        Assert.True(result.Items.Count > 0);
    }
    
    [Theory]
    [MemberData(nameof(GetValidPaginationParams))]
    public async Task GetAllItemsAsync_ShouldReturnTheCorrectPaginationFromQuery(ProductParams productParams)
    {
        // Arrange
        var context = CreateContext();
        var productService = new ProductService(context);
        
        // Act.
        var result = await productService.GetAllItemsAsync(productParams);

        // Assert.
        int totalCount = await context.ProductItems.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)productParams.PageSize);
        var expectedPagination =
            new Pagination(productParams.PageSize, totalCount, totalPages, productParams.PageNumber);
        Assert.Equal(expectedPagination, result.Pagination);
    }

    [Theory]
    [MemberData(nameof(GetAllProductParamsWithUniqueSortValues))]
    public async Task GetAllItemsAsync_ShouldSortWithQueryPassed(ProductParams productParams)
    {
        // Arrange
        var context = CreateContext();
        var productService = new ProductService(context);

        // Act.
        var result = await productService.GetAllItemsAsync(productParams);
        var productItems = result.Items;


        // Assert.
        for (int i = 1; i < productItems.Count; i++)
        {
            var prev = productItems[i - 1];
            var curr = productItems[i];
            switch (productParams.Sort)
            {
                case "name":
                    Assert.True(string.CompareOrdinal(prev.Name, curr.Name) <= 0);
                    break;
                case "popular":
                    Assert.True(prev.Id < curr.Id);
                    break;
                default:
                    Assert.True(prev.Id < curr.Id);
                    break;
                
            }
        }
    }

    [Theory]
    [MemberData(nameof(GetSampleDrinkNames))]
    public async Task GetAllItemsAsync_ShouldReturnOnlyDrinkNamesPassed(ProductParams productParams)
    {
        // Arrange
        var context = CreateContext();
        var productService = new ProductService(context);

        // Act.
        var result = await productService.GetAllItemsAsync(productParams);
        var productItems = result.Items;
        
        // Assert.
        Assert.All(productItems, p =>
        {
            if (productParams.DrinkName is null)
            {
                return;
            }
            Assert.Contains(productParams.DrinkName.ToLower(), p.Name.ToLower());
        });
    }

    [Fact]
    public async Task GetItemAsync_ShouldReturnAProductItem()
    {
        // Arrange
        var context = CreateContext();
        var expectedProductItem = await GetExpectedProductItem(context);
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.GetItemAsync(expectedProductItem.Id);
        var productItem = result.Value;
        
        // Assert.
        Assert.IsType<ProductItem>(productItem);
        Assert.Equivalent(expectedProductItem, productItem);
    }
    
    [Fact]
    public async Task GetItemAsync_ShouldReturnNotFoundError_WhenIdIsNotFound()
    {
        // Arrange
        var context = CreateContext();
        int id = 10000;
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.GetItemAsync(id);
        var errors = result.Errors;
        var firstError = errors[0];

        // Assert.
        Assert.Equal(ErrorType.NotFound, firstError.Type);
    }

    [Fact]
    public async Task CreateItemAsync_ShouldReturnProductItem_WhenSavedSuccessfully()
    {
        // Arrange
        var context = CreateContext();
        var newProductItem = GetSampleEditProductDto();
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.CreateItemAsync(newProductItem);
        var productItem = result.Value;
        
        // Assert.
        AssertEditProductDtoEqualToProductItem(newProductItem, productItem);
    }


    [Fact]
    public async Task EditItemAsync_ShouldReturnProductItem_WhenEditSuccessfully()
    {
        // Arrange
        var context = CreateContext();
        int existingId = 1;
        var updatedProductItem = GetSampleEditProductDto();
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.EditItemAsync(updatedProductItem, existingId);
        var productItem = result.Value;
        var confirmResult = await context.ProductItems.AsNoTracking().FirstAsync(p => p.Id == existingId);
        
        // Assert.
        AssertEditProductDtoEqualToProductItem(updatedProductItem, productItem);
        AssertEditProductDtoEqualToProductItem(updatedProductItem, confirmResult);
    }

    [Fact]
    public async Task EditItemAsync_ShouldReturnNotFoundError_WhenIdDoesNotExistInDB()
    {
        // Arrange
        var context = CreateContext();
        int nonExistingId = 100000;
        var updatedProductItem = GetSampleEditProductDto();
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.EditItemAsync(updatedProductItem, nonExistingId);
        var errors = result.Errors;
        var firstError = errors[0];
        
        // Assert
        Assert.Equal(ErrorType.NotFound, firstError.Type);
    }
    
    [Fact]
    public async Task DeleteItemAsync_ShouldReturnDeletedType_WhenSuccessfullyDeletedProductItem()
    {
        // Arrange
        var context = CreateContext();
        int existingId = 1;
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.DeleteItemAsync(existingId);
        var confirmResult = await context.ProductItems.AsNoTracking().FirstOrDefaultAsync(p => p.Id == existingId);
        
        // Assert.
        Assert.Equal(Result.Deleted, result.Value);
        Assert.Null(confirmResult);
    }
    
    [Fact]
    public async Task DeleteItemAsync_ShouldReturnNotFoundError_WhenIdDoesNotExistInDB()
    {
        // Arrange
        var context = CreateContext();
        int nonExistingId = 100000;
        var productService = new ProductService(context);
        
        // Act
        var result = await productService.DeleteItemAsync(nonExistingId);
        var errors = result.Errors;
        var firstError = errors[0];
        
        // Assert
        Assert.Equal(ErrorType.NotFound, firstError.Type);
    }

    private void AssertEditProductDtoEqualToProductItem(EditProductDto editProductDto, ProductItem productItem)
    {
        Assert.Equal(editProductDto.Type, productItem.Type);
        Assert.Equal(editProductDto.Description, productItem.Description);
        Assert.Equal(editProductDto.Image, productItem.Image);
        Assert.Equal(editProductDto.ImageCreator, productItem.ImageCreator);
        Assert.Equal(editProductDto.Name, productItem.Name);
        Assert.Equal(editProductDto.Ounces, productItem.Ounces);
        Assert.Equal(editProductDto.Price, productItem.Price);
    }


    private async Task<ProductItem> GetExpectedProductItem(AppDbContext context)
    {
        var items = await context.ProductItems.AsNoTracking().ToListAsync();
        var rnd = new Random();
        var randomIndex = rnd.Next(0, items.Count);

        return items[randomIndex];
    }
    
    private AppDbContext CreateContext() => new AppDbContext(_contextOptions);


    public void Dispose() => _connection.Dispose();
    
    public static IEnumerable<object[]> GetValidPaginationParams()
    {
        yield return new object[] { new ProductParams() };
        yield return new object[] { new ProductParams { PageSize = 1 } };
        yield return new object[] { new ProductParams { PageSize = 1, PageNumber = 2 } };
        yield return new object[] { new ProductParams { PageSize = 1, PageNumber = 100 } };
        yield return new object[] { new ProductParams {PageSize = 100} };
        yield return new object[] { new ProductParams { PageNumber = 2 } };
        yield return new object[] { new ProductParams { PageNumber = 100 } };
    }
    
    public static IEnumerable<object[]> GetAllProductParamsWithUniqueSortValues()
    {
        yield return new object[] { new ProductParams() };
        yield return new object[] { new ProductParams{Sort = "name"} };
        yield return new object[] { new ProductParams{Sort = "popular"} };
        yield return new object[] { new ProductParams{PageNumber = 2} };
        yield return new object[] { new ProductParams{Sort = "name", PageNumber = 2} };
        yield return new object[] { new ProductParams{Sort = "popular", PageNumber = 2} };
        // SQLite does not support sorting decimal types.
        // yield return new object[] { new ProductParams{Sort = "price"} };
    }

    public static IEnumerable<object[]> GetSampleDrinkNames()
    {
        yield return new object[] { new ProductParams{DrinkName = "Latte"} };
        yield return new object[] { new ProductParams{DrinkName = "Coffee"} };
    }
    
    private static EditProductDto GetSampleEditProductDto()
    {
        var newProductItem = new EditProductDto
        {
            Type = "Drink",
            Description = "Ok",
            Image = "Coffee Image",
            ImageCreator = "Image Creator",
            Name = "Coffee Test",
            Ounces = 4.0,
            Price = 3m
        };
        return newProductItem;
    }
}