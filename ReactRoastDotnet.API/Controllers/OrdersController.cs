using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.API.Controllers;

public class OrdersController : ApiController
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /api/orders
    // TODO: Admin users can retrieve any order.
    /// <summary>
    /// Gets a history of a user's orders.
    /// </summary>
    /// <param name="paginationParams">Page query.</param>
    /// <returns>A list of previous orders.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(PaginationList<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginationList<OrderDto>>> GetOrders([FromQuery] PaginationParams paginationParams)
    {
        // Set up request query from user.
        int skipToPageNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

        int userId = GetUserId();

        List<Order> orders = await _context.Orders
            .AsNoTracking()
            .Skip(skipToPageNumber)
            .Take(paginationParams.PageSize)
            .Include(o => o.Items)
            .ThenInclude(item => item.ProductItem)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.DateCreated)
            .ToListAsync();

        // Set up pagination.
        int totalCount = await _context.ProductItems.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)paginationParams.PageSize);

        var pagination = new Pagination(
            paginationParams.PageSize,
            totalCount,
            totalPages,
            paginationParams.PageSize
        );

        List<OrderDto> ordersDto = orders.Select(MapOrderToOrderDto).ToList();

        return Ok(new PaginationList<OrderDto>(ordersDto, pagination));
    }

    // GET: /api/orders/{id}
    // TODO: Admin users can retrieve any order.
    /// <summary>
    /// Retrieve a previous order.
    /// </summary>
    /// <param name="id">The id of the order.</param>
    /// <returns>The order requested.</returns>
    [Authorize]
    [HttpGet("{id}", Name = "GetOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        int userId = GetUserId();

        Order? order = await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(item => item.ProductItem)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return NotFound();
        }

        if (order.UserId != userId)
        {
            return Unauthorized();
        }

        return Ok(MapOrderToOrderDto(order));
    }

    // POST: /api/orders
    // TODO: Implement create order for guest users.
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <returns>The newly created order receipt.</returns>
    /// <exception cref="Exception"></exception>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(OrderReceiptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderReceiptDto>> CreateOrder()
    {
        int userId = GetUserId();
        Cart? cart = await GetCart(userId);

        if (cart is null)
        {
            return BadRequest("Could not find cart.");
        }

        if (!cart.Items.Any())
        {
            return BadRequest("Can not create order on an empty cart.");
        }

        string userEmail = GetUserEmail();

        Order order = GetNewOrder(cart, userId, userEmail);

        var newOrder = _context.Orders.Add(order);
        _context.Carts.Remove(cart);

        bool savedSuccessfully = await _context.SaveChangesAsync() > 0;
        if (!savedSuccessfully)
        {
            // TODO: Change to a custom exception.
            throw new Exception("Failed to save new order.");
        }

        OrderReceiptDto receipt = GetReceipt(userEmail, newOrder.Entity);

        return Ok(receipt);
    }

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

    private string GetUserEmail()
    {
        var userEmail = User.Identity?.Name;
        if (userEmail is null)
        {
            // TODO: Change to a custom exception.
            throw new Exception("User email not found");
        }

        return userEmail;
    }

    private OrderReceiptDto GetReceipt(string userEmail, Order order)
    {
        List<CheckoutItem> checkoutItems = order.Items.Select(item => new CheckoutItem
        {
            ProductItemId = item.ProductItemId,
            Name = item.ProductItem.Name,
            Price = item.ProductItem.Price,
            Quantity = item.Quantity
        }).ToList();

        return new OrderReceiptDto
        {
            OrderNumber = order.Id,
            Email = userEmail,
            Items = checkoutItems,
            TotalQuantity = order.TotalQuantity,
            TotalPrice = order.TotalPrice
        };
    }

    private OrderDto MapOrderToOrderDto(Order order)
    {
        var orderItemsDto = order.Items.Select(orderItem => new OrderItemDto
        {
            Id = orderItem.ProductItemId,
            Type = orderItem.ProductItem.Type,
            Name = orderItem.ProductItem.Name,
            Price = orderItem.ProductItem.Price,
            Quantity = orderItem.Quantity,
        }).ToList();


        return new OrderDto
        {
            Id = order.Id,
            OrderItems = orderItemsDto,
            TotalQuantity = order.TotalQuantity,
            TotalPrice = order.TotalPrice,
            DateCreated = order.DateCreated
        };
    }

    private async Task<Cart?> GetCart(int userId)
    {
        Cart? cart = await _context.Carts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.ProductItem)
            .FirstOrDefaultAsync(cart => cart.UserId == userId);
        return cart;
    }

    private Order GetNewOrder(Cart cart, int? userId, string email)
    {
        var totalQuantity = 0;
        var totalPrice = 0m;
        var orderItems = new List<OrderItem>();

        cart.Items.ForEach(item =>
        {
            orderItems.Add(new OrderItem
            {
                ProductItem = item.ProductItem,
                Quantity = item.Quantity,
                Price = item.ProductItem.Price,
            });
            totalQuantity += item.Quantity;
            totalPrice += item.ProductItem.Price * item.Quantity;
        });

        var order = new Order
        {
            UserId = userId,
            Email = email,
            TotalQuantity = totalQuantity,
            TotalPrice = totalPrice,
            Items = orderItems,
        };

        return order;
    }
}