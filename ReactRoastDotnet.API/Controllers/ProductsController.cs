using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.Repositories;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// Product CRUD controller.
/// </summary>
public class ProductsController : ApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    /// <summary>
    /// Get a list of available products as requested from a user query.
    /// </summary>
    /// <param name="productParams">Product search query to get drink name and or search products by name or popularity.</param>
    /// <returns>Product Item list requested by user.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationList<ProductItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginationList<ProductItem>>> GetProducts([FromQuery] ProductParams productParams)
    {
        ErrorOr<PaginationList<ProductItem>> result = await _productService.GetAllItemsAsync(productParams);
        return result.Match(Ok, GetProblemResult);
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
        ErrorOr<ProductItem> result = await _productService.GetItemAsync(id);
        return result.Match(Ok, GetProblemResult);
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
        ErrorOr<ProductItem> result = await _productService.CreateItemAsync(createProductDto);
        return result.Match(
            createdProduct => CreatedAtAction(
                nameof(GetProduct),
                new { id = createdProduct.Id },
                createdProduct
            ),
            GetProblemResult);
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
        ErrorOr<ProductItem> result = await _productService.EditItemAsync(editProductDto, id);
        return result.Match(Ok, GetProblemResult);
    }

    // DELETE: api/products/{id}
    /// <summary>
    /// Deletes a product item from our database.
    /// Only accessible for admins.
    /// </summary>
    /// <param name="id">The product id to be deleted.</param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        ErrorOr<Deleted> result = await _productService.DeleteItemAsync(id);
        return result.Match(_ => NoContent(), GetProblemResult);
    }
}