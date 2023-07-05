using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Models.Cart;
using ReactRoastDotnet.Data.Repositories;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// The cart controller. Only authorized users can access.
/// </summary>
/// <response code="401">The user is not logged in.</response>
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    /// <returns>Cart with a list product items ordered and the date the cart was created.</returns>
    /// <response code="200">If the user is authenticated and has a cart saved.</response>
    /// <response code="404">If the user does not have a cart saved.</response>
    [HttpGet(Name = nameof(GetCart))]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        ErrorOr<CartDto> result = await _cartService.GetCartAsync(User);
        return result.Match(Ok, GetProblemResult);
    }


    // POST: api/cart
    /// <summary>
    /// Adds an item to an existing cart. If the cart does not exist, then a new cart is created.
    /// </summary>
    /// <param name="id">The product id to add to the cart. (Currently 1-11)</param>
    /// <param name="quantity">The amount of items to add to the cart (Max = 10).</param>
    /// <returns>Cart created/modified.</returns>
    /// <response code="201">Successfully added item to cart.</response>
    /// <response code="400">Invalid types entered for the product id or quantity is more than 10.</response>
    [HttpPost("Products/{id}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CartDto>> AddItemToCart(int id, [Range(1, 10), DefaultValue(1)] int quantity = 1)
    {
        ErrorOr<CartDto> result = await _cartService.AddProductItemAsync(User, id, quantity);
        return result.Match(cartDto => CreatedAtAction(nameof(GetCart), cartDto), GetProblemResult);
    }

    // DELETE: api/cart
    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="id">The product id to remove from the cart.</param>
    /// <param name="quantity">The number of items to remove from the cart.</param>
    /// <returns>No Content</returns>
    /// <response code="204">Successfully remove item from cart.</response>
    /// <response code="400">Invalid types entered for the product or quantity.</response>
    /// <response code="404">If the product id item was not in the cart.</response>
    [HttpDelete("Products/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveCartItem(int id, [DefaultValue(1)] int quantity = 1)
    {
        ErrorOr<Deleted> result = await _cartService.RemoveProductItemAsync(User, id, quantity);
        return result.Match(_ => NoContent(), GetProblemResult);
    }
}