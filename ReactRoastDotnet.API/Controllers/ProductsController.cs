using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.API.Extensions;
using ReactRoastDotnet.API.Models.RequestDto;
using ReactRoastDotnet.API.Models.ResponseDto;
using ReactRoastDotnet.API.RequestParams;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.ResponseDto;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Implement a ProductService.
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
    [HttpGet]
    [ProducesResponseType(typeof(ProductItemListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginationList<ProductItem>>> GetProducts([FromQuery] ProductParams productParams)
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

        return Ok(new PaginationList<ProductItem>(productItems, pagination));
    }

    // GET: api/products/{id}
    /// <summary>
    /// Gets a product from our database if the id is in our DB.
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <returns>The product item if found.</returns>
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ProductItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductItem>> GetProduct(int id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem is null)
        {
            return NotFound();
        }

        return Ok(productItem);
    }

    /// <summary>
    /// Create a new product. Only for admin roles.
    /// </summary>
    /// <param name="createProductDto">Product item to add to the database.</param>
    /// <returns>The created product item.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(ProductItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductItem>> CreateProduct(EditProductDto createProductDto)
    {
        var productItem = MapCreateProductToProductItem(createProductDto);
        var newProductItem = _context.ProductItems.Add(productItem);

        var result = await _context.SaveChangesAsync() > 0;
        
        if (!result)
        {
            return BadRequest(new ProblemDetails{Title = "Problem creating new product."});
        }
        return CreatedAtRoute("GetProduct", new { id = newProductItem.Entity.Id }, newProductItem.Entity.Id);
    }

    /// <summary>
    /// Edit product in the database.
    /// </summary>
    /// <param name="editProductDto">The product edited.</param>
    /// <param name="id">The id of the product item id to edit.</param>
    /// <returns>The updated product item.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductItem>> EditProduct(EditProductDto editProductDto, int id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem is null)
        {
            return NotFound();
        }
        
        productItem.UpdateProductItem(editProductDto);
        
        var result = await _context.SaveChangesAsync() > 0;
        
        if (!result)
        {
            return BadRequest(new ProblemDetails{Title = "Problem saving new edit product."});
        }

        return Ok(productItem);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        ProductItem? productItem = await _context.ProductItems.FirstOrDefaultAsync(item => item.Id == id);

        if (productItem is null)
        {
            return NotFound();
        }

        _context.ProductItems.Remove(productItem);
        var result = await _context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest(new ProblemDetails{Title = "Problem deleting product item."});
        }
        
        return Ok();
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