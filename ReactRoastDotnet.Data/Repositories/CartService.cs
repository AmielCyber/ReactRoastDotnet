using System.Security.Claims;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data.Common.Errors;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Cart;

namespace ReactRoastDotnet.Data.Repositories;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<CartDto>> GetCartAsync(ClaimsPrincipal user)
    {
        Cart? cart = await RetrieveCart(user);

        if (cart is null)
        {
            return Errors.Cart.NotFound("Could not find cart for this user.");
        }

        return MapCartToCartDto(cart);
    }

    public async Task<ErrorOr<CartDto>> AddProductItemAsync(ClaimsPrincipal user, int id, int quantity = 1)
    {
        // Get cart or create cart.
        Cart cart = await RetrieveCart(user) ?? CreateCart(user);
        // Get ProductItem.
        ProductItem? productItem = await _context.ProductItems.FindAsync(id);

        if (productItem is null)
        {
            return Errors.Cart.NotFound($"Could not find product item with id: {id}.");
        }

        // Add ProductItem.
        AddItem(cart, productItem, quantity);

        // Save Changes.
        var success = await _context.SaveChangesAsync() > 0;

        if (!success)
        {
            return Errors.Cart.FailedToSaveChanges("Problem saving item to cart.");
        }

        return MapCartToCartDto(cart);
    }

    public async Task<ErrorOr<Deleted>> RemoveProductItemAsync(ClaimsPrincipal user, int id, int quantity = 1)
    {
        // Get cart
        Cart? cart = await RetrieveCart(user);
        if (cart is null)
        {
            return Errors.Cart.NotFound("Could not find an existing cart for this user.");
        }

        // Check if product is in cart.
        if (!cart.Items.Exists(item => item.ProductItem.Id == id))
        {
            return Errors.Cart.NotFound($"Could not find product item id: {id} in cart.");
        }

        // Remove item or update.
        RemoveItem(cart, id, quantity);

        // Save Changes.
        var success = await _context.SaveChangesAsync() > 0;

        if (!success)
        {
            return Errors.Cart.FailedToSaveChanges("Problem deleting item to cart.");
        }

        return Result.Deleted;
    }

    /// <summary>
    /// Retrieves a cart from user's claim id.
    /// </summary>
    /// <returns>Cart object if user has a cart or null if user does not have a cart.</returns>
    private async Task<Cart?> RetrieveCart(ClaimsPrincipal user)
    {
        int userId = GetUserId(user);

        Cart? cart = await _context.Carts
            .Include(cart => cart.Items)
            .ThenInclude(cartItem => cartItem.ProductItem)
            .FirstOrDefaultAsync(cart => cart.UserId == userId);

        return cart;
    }

    /// <summary>
    /// Gets the current user id from the User's claims.
    /// </summary>
    /// <returns>An integer of the current user's Id.</returns>
    /// <exception cref="Exception">If id is not integer or id not found in user claims.</exception>
    private int GetUserId(ClaimsPrincipal user)
    {
        var claims = user.Claims;
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
    /// Creates a new cart. 
    /// </summary>
    /// <returns>Cart object</returns>
    private Cart CreateCart(ClaimsPrincipal user)
    {
        int userId = GetUserId(user);

        var cart = new Cart
        {
            UserId = userId,
            LastModified = DateTime.UtcNow
        };

        _context.Carts.Add(cart);
        return cart;
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

        cart.LastModified = DateTime.UtcNow;
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

        cart.LastModified = DateTime.UtcNow;
    }
}