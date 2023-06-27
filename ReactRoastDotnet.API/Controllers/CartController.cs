using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.ResponseDto;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Refactor to use services.
// TODO: Add correct REST endpoints.
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        Cart? cart = await RetrieveCart();

        if (cart is null)
        {
            return NotFound();
        }

        return MapCartToCartDto(cart);
    }


    [HttpPost]
    public async Task<ActionResult> AddItemToCart(int productId, int quantity)
    {
        // Get cart or create cart.
        Cart? cart = await RetrieveCart() ?? CreateCart();
        // Get ProductItem.
        ProductItem? productItem = await _context.ProductItems.FindAsync(productId);

        if (productItem is null)
        {
            return BadRequest(new ProblemDetails { Title = "Product not found" });
        }

        // Add ProductItem.
        cart.AddItem(productItem, quantity);

        // Save Changes.
        var successful = await _context.SaveChangesAsync() > 0;

        if (!successful)
        {
            return BadRequest(new ProblemDetails { Title = "Problem saving item to cart" });
        }

        return CreatedAtRoute(nameof(GetCart), MapCartToCartDto(cart));
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveCartItem(int productId, int quantity)
    {
        // Get cart
        Cart? cart = await RetrieveCart();
        if (cart is null)
        {
            return NotFound();
        }
        // Remove item or update.
        cart.RemoveItem(productId, quantity);
        
        // Save Changes.
        var successful = await _context.SaveChangesAsync() > 0;

        if (!successful)
        {
            return BadRequest(new ProblemDetails { Title = "Problem saving item to cart" });
        }
        
        return Ok();
    }

    private static CartDto MapCartToCartDto(Cart cart)
    {
        var cartItemsDto = cart.Items.Select(cartItem => new CartItemDto
            {
                Id = cartItem.ProductItemId,
                Image = cartItem.ProductItem.Image,
                ImageCreator = cartItem.ProductItem.ImageCreator,
                Name = cartItem.ProductItem.Name,
                Price = cartItem.ProductItem.Price,
                Quantity = cartItem.Quantity,
                Type = cartItem.ProductItem.Type
            })
            .ToList();
        return new CartDto
        {
            DateCreated = cart.DateCreated,
            Items = cartItemsDto
        };
    }

    private async Task<Cart?> RetrieveCart()
    {
        var valid = int.TryParse(Request.Cookies["userId"], out var userId);
        if (!valid)
        {
            return null;
        }

        var cart = await _context.Carts
            .Include(cart => cart.Items)
            .ThenInclude(cartItem => cartItem.ProductItem)
            .FirstOrDefaultAsync(cart => cart.UserId == userId);
        return cart;
    }

    private Cart CreateCart()
    {
        // TODO: Use userId from identity
        var userId = Guid.NewGuid().GetHashCode();
        var cookieOptions = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTimeOffset.Now.AddDays(7)
        };
        Response.Cookies.Append("userId", userId.ToString(), cookieOptions);
        var cart = new Cart
        {
            UserId = userId,
        };

        _context.Carts.Add(cart);
        return cart;
    }
}