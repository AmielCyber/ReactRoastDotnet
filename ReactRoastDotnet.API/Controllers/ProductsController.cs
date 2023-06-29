using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.API.Extensions;
using ReactRoastDotnet.API.RequestParams;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.ResponseDto;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Implement a ProductService.
// TODO: Implement full CRUD operations.
/// <summary>
/// Product CRUD controller.
/// </summary>
public class ProductsController : ApiController
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    /// <summary>
    /// Get a list of available products as requested from a user query.
    /// </summary>
    /// <param name="productParams">Product search query to get drink name and or search products by name or popularity.</param>
    /// <returns>Product Item list requested by user.</returns>
    [ProducesResponseType(typeof(ProductItemListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<ProductItemListDto>> GetProducts([FromQuery] ProductParams productParams)
    {
        // Set up request query from user.
        int skipToPageNumber = (productParams.PageNumber - 1) * productParams.PageSize;

        var query = _context.ProductItems
            .AsNoTracking()
            .Skip(skipToPageNumber)
            .Take(productParams.PageSize)
            .SearchDrink(productParams.DrinkName)
            .Sort(productParams.Sort)
            .AsQueryable();

        // Set up pagination.
        int totalCount = await _context.ProductItems.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)productParams.PageSize);

        var pagination = new Pagination(
            productParams.PageSize,
            totalCount,
            totalPages,
            productParams.PageSize
        );

        List<ProductItem> productItems = await query.ToListAsync();

        return Ok(new ProductItemListDto(productItems, pagination));
    }

    // GET: api/products/{id}
    /// <summary>
    /// Gets a product from our database if the id is in our DB.
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <returns>The product item if found.</returns>
    [ProducesResponseType(typeof(ProductItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProduct(int id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem is null)
        {
            return NotFound();
        }

        return Ok(productItem);
    }
}