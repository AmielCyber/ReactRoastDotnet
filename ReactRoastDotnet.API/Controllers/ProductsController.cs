using System.Net.Mime;
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
[Produces(MediaTypeNames.Application.Json)]
public class ProductsController : ApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    /// <summary>
    /// Get a list of available products.
    /// </summary>
    /// <param name="productParams">Product search query to get drink name and or search products by name, popularity, or price.</param>
    /// <returns>Product Item list requested by user.</returns>
    /// <response code="200">Successfully obtained the paged products.</response>
    /// <response code="400">Invalid query entered.</response>
    [HttpGet]
    [Produces(typeof(PaginationList<ProductItem>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginationList<ProductItem>>> GetProducts([FromQuery] ProductParams productParams)
    {
        ErrorOr<PaginationList<ProductItem>> result = await _productService.GetAllItemsAsync(productParams);
        return result.Match(Ok, GetProblemResult);
    }

    // GET: api/products/{id}
    /// <summary>
    /// Gets a product item.
    /// </summary>
    /// <param name="id">The product item ID to retrieve.</param>
    /// <returns>The product item if found.</returns>
    /// <response code="200">The product item requested has been fetched.</response>
    /// <response code="400">Invalid id type entered.</response>
    /// <response code="404">Product item not found.</response>
    [HttpGet("{id}", Name = "GetProduct")]
    [Produces(typeof(ProductItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <response code="201">Successfully created new product item.</response>
    /// <response code="400">Invalid type entered in the body.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(ProductItem), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// Edit product in the database. Only for admins.
    /// </summary>
    /// <param name="editProductDto">The product item with new values.</param>
    /// <param name="id">The id of the product item to edit.</param>
    /// <returns>The updated product item.</returns>
    /// <response code="200">Successfully updated the product item with the new values passed.</response>
    /// <response code="400">Invalid type entered in the body.</response>
    /// <response code="400">Product item does not exist.</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [Produces(typeof(ProductItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductItem>> EditProduct(EditProductDto editProductDto, int id)
    {
        ErrorOr<ProductItem> result = await _productService.EditItemAsync(editProductDto, id);
        return result.Match(Ok, GetProblemResult);
    }

    // DELETE: api/products/{id}
    /// <summary>
    /// Deletes a product item from our database.
    /// Only for admins.
    /// </summary>
    /// <param name="id">The product id to be deleted.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Successfully removed product item.</response>
    /// <response code="400">Invalid id type.</response>
    /// <response code="404">Product item does not exist.</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        ErrorOr<Deleted> result = await _productService.DeleteItemAsync(id);
        return result.Match(_ => NoContent(), GetProblemResult);
    }
}