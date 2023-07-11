using System.Security.Claims;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.Data.Common.Errors;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Extensions;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Repositories;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationList<OrderDto>> GetAllFromUserAsync(OrderParams orderParams,
        ClaimsPrincipal user)
    {
        // Set up request query from user.
        int skipToPageNumber = (orderParams.PageNumber - 1) * orderParams.PageSize;

        int userId = GetUserId(user);

        List<Order> orders = await _context.Orders
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .ThenInclude(item => item.ProductItem)
            .Sort(orderParams.Sort)
            .Skip(skipToPageNumber)
            .Take(orderParams.PageSize)
            .ToListAsync();

        // Set up pagination.
        int totalCount = await _context.Orders.Where(o => o.UserId == userId).CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)orderParams.PageSize);

        var pagination = new Pagination(
            orderParams.PageSize,
            totalCount,
            totalPages,
            orderParams.PageSize
        );

        List<OrderDto> ordersDto = orders.Select(MapOrderToOrderDto).ToList();

        return new PaginationList<OrderDto>(ordersDto, pagination);
    }

    public async Task<ErrorOr<OrderDto>> GetAsync(int id, ClaimsPrincipal user)
    {
        int userId = GetUserId(user);

        Order? order = await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(item => item.ProductItem)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return Errors.Order.NotFound($"Order number {id} not found.");
        }

        // TODO: Make an exception for admin.
        if (order.UserId != userId)
        {
            return Errors.Order.Forbidden("User does not have access to this order.");
        }

        return MapOrderToOrderDto(order);
    }

    public async Task<ErrorOr<OrderReceiptDto>> CreateOrderAsync(ClaimsPrincipal user)
    {
        int userId = GetUserId(user);
        Cart? cart = await GetCart(userId);

        if (cart is null)
        {
            return Errors.Cart.NotFound("Could not find cart.");
        }

        if (!cart.Items.Any())
        {
            return Errors.Cart.BadRequest("Could not create order on an empty cart.");
        }

        string userEmail = GetUserEmail(user);

        Order order = GetNewOrder(cart, userId, userEmail);

        var newOrder = _context.Orders.Add(order);
        _context.Carts.Remove(cart);

        bool savedSuccessfully = await _context.SaveChangesAsync() > 0;
        if (!savedSuccessfully)
        {
            return Errors.Order.FailedToSaveChanges("Failed to save new order.");
        }

        OrderReceiptDto receipt = GetReceipt(userEmail, newOrder.Entity);

        return receipt;
    }

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

    private string GetUserEmail(ClaimsPrincipal user)
    {
        var userEmail = user.Identity?.Name;
        if (userEmail is null)
        {
            // TODO: Change to a custom exception.
            throw new Exception("User email not found");
        }

        return userEmail;
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
}