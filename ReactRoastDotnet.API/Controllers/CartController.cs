using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Models.Cart;
using ReactRoastDotnet.Data.Repositories;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// The cart controller. Only authorized users can access.
/// </summary>
[Authorize]
public class CartController : ApiController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // GET: api/cart
    /// <summary>
    /// Gets an existing cart from the current authenticated user.
    /// </summary>
    /// <returns>Cart with a list product items ordered and the date created.</returns>
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet(Name = nameof(GetCart))]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        ErrorOr<CartDto> result = await _cartService.GetCartAsync(User);
        return result.Match(Ok, GetProblemResult);
    }


    // POST: api/cart
    /// <summary>
    /// Adds an item to an existing cart. If cart does not exist, then it creates a new cart.
    /// </summary>
    /// <param name="id">The product id in the query params to add to the cart.</param>
    /// <param name="quantity">The quantity  number in the query params to add to the cart.</param>
    /// <returns>Cart created/modified.</returns>
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost("Products/{id}")]
    public async Task<ActionResult<CartDto>> AddItemToCart(int id, [Range(1, 10), DefaultValue(1)] int quantity = 1)
    {
        ErrorOr<CartDto> result = await _cartService.AddProductItemAsync(User, id, quantity);
        return result.Match(cartDto => CreatedAtAction(nameof(GetCart), cartDto), GetProblemResult);
    }

    // DELETE: api/cart
    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="id">The product id in the query params to remove from the cart.</param>
    /// <param name="quantity">The quantity  number in the query params to remove from the cart.</param>
    /// <returns>No Content</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpDelete("Products/{id}")]
    public async Task<ActionResult> RemoveCartItem(int id, [Range(1, 10), DefaultValue(1)] int quantity = 1)
    {
        ErrorOr<Deleted> result = await _cartService.RemoveProductItemAsync(User, id, quantity);
        return result.Match(_ => NoContent(), GetProblemResult);
    }
}