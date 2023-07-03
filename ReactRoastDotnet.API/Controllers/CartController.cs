using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Cart;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Refactor to Implement an CartService.
/// <summary>
/// The cart controller. Only authorized users can access.
/// </summary>
[Authorize]
public class CartController : ApiController
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
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
        Cart? cart = await RetrieveCart();

        if (cart is null)
        {
            return NotFound();
        }

        return Ok(MapCartToCartDto(cart));
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
    public async Task<ActionResult> AddItemToCart(int id, [Range(1, 10), DefaultValue(1)] int quantity = 1)
    {
        // Get cart or create cart.
        Cart cart = await RetrieveCart() ?? CreateCart();
        // Get ProductItem.
        ProductItem? productItem = await _context.ProductItems.FindAsync(id);

        if (productItem is null)
        {
            return BadRequest(new ProblemDetails { Title = "Product not found" });
        }

        // Add ProductItem.
        AddItem(cart, productItem, quantity);

        // Save Changes.
        var successful = await _context.SaveChangesAsync() > 0;

        if (!successful)
        {
            return BadRequest(new ProblemDetails { Title = "Problem saving item to cart" });
        }

        return CreatedAtRoute("GetCart", MapCartToCartDto(cart));
    }

    // DELETE: api/cart
    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="id">The product id in the query params to remove from the cart.</param>
    /// <param name="quantity">The quantity  number in the query params to remove from the cart.</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpDelete("Products/{id}")]
    public async Task<ActionResult> RemoveCartItem(int id, [Range(1, 10), DefaultValue(1)] int quantity = 1)
    {
        // Get cart
        Cart? cart = await RetrieveCart();
        if (cart is null)
        {
            return NotFound();
        }

        // Remove item or update.
        RemoveItem(cart, id, quantity);

        // Save Changes.
        var successful = await _context.SaveChangesAsync() > 0;

        if (!successful)
        {
            return BadRequest(new ProblemDetails { Title = "Problem saving item to cart" });
        }

        return Ok();
    }

    /// <summary>
    /// CartDto Mapper
    /// </summary>
    /// <param name="cart">Cart object to map</param>
    /// <returns>CartDto</returns>
    private static CartDto MapCartToCartDto(Cart cart)
    {
        var cartItemsDto = cart.Items.Select(cartItem => new CartItemDto
            {
                Id = cartItem.ProductItemId,
                Name = cartItem.ProductItem.Name,
                Price = cartItem.ProductItem.Price,
                Quantity = cartItem.Quantity,
                Type = cartItem.ProductItem.Type
            })
            .ToList();
        return new CartDto
        {
            LastModified = cart.LastModified,
            Items = cartItemsDto
        };
    }


    /// <summary>
    /// Retrieves a cart from user's claim id.
    /// </summary>
    /// <returns>Cart object if user has a cart or null if user does not have a cart.</returns>
    private async Task<Cart?> RetrieveCart()
    {
        int userId = GetUserId();

        Cart? cart = await _context.Carts
            .Include(cart => cart.Items)
            .ThenInclude(cartItem => cartItem.ProductItem)
            .FirstOrDefaultAsync(cart => cart.UserId == userId);

        return cart;
    }

    /// <summary>
    /// Creates a new cart. 
    /// </summary>
    /// <returns>Cart object</returns>
    private Cart CreateCart()
    {
        int userId = GetUserId();

        var cart = new Cart
        {
            UserId = userId,
            LastModified = DateTime.Now
        };

        _context.Carts.Add(cart);
        return cart;
    }

    /// <summary>
    /// Gets the current user id from the User's claims.
    /// </summary>
    /// <returns>An integer of the current user's Id.</returns>
    /// <exception cref="Exception">If id is not integer or id not found in user claims.</exception>
    private int GetUserId()
    {
        var claims = User.Claims;
        Claim? identifierClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (identifierClaim is null)
        {
            // TODO: Change to a custom exception.
            throw new Exception("User Id not found");
        }

        var validUserId = int.TryParse(identifierClaim.Value, out var userId);
        if (!validUserId)
        {
            // TODO: Change to a custom exception.
            throw new Exception("User Id not valid");
        }

        return userId;
    }

    /// <summary>
    /// Adds an item to the cart passed.
    /// </summary>
    /// <param name="cart">Cart to modified.</param>
    /// <param name="productItem">The product item to add to the cart.</param>
    /// <param name="quantity">The amount to add to the cart.</param>
    private void AddItem(Cart cart, ProductItem productItem, int quantity)
    {
        CartItem? existingCartItem = cart.Items.FirstOrDefault(cartItem => cartItem.ProductItemId == productItem.Id);

        if (existingCartItem is null)
        {
            cart.Items.Add(new CartItem
            {
                ProductItem = productItem,
                Quantity = quantity
            });
        }
        else
        {
            existingCartItem.Quantity += quantity;
        }

        cart.LastModified = DateTime.Now;
    }

    /// <summary>
    /// Removes an item from the cart passed.
    /// </summary>
    /// <param name="cart">Cart to modified.</param>
    /// <param name="productItemId">The product item id to remove from the cart.</param>
    /// <param name="quantity">The amount to remove from the cart.</param>
    private void RemoveItem(Cart cart, int productItemId, int quantity)
    {
        CartItem? existingCartItem = cart.Items.FirstOrDefault(cartItem => cartItem.ProductItemId == productItemId);

        if (existingCartItem is null) return;

        existingCartItem.Quantity -= quantity;
        if (existingCartItem.Quantity <= 0)
        {
            cart.Items.Remove(existingCartItem);
        }

        cart.LastModified = DateTime.Now;
    }
}