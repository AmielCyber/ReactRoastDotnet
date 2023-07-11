using System.Net.Mime;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.Repositories;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// Orders only open to authenticated users now.
/// Will implement guest orders in the future.
/// </summary>
[Produces(MediaTypeNames.Application.Json)]
public class OrdersController : ApiController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: /api/orders
    // TODO: Admin users can retrieve any order.
    /// <summary>
    /// Gets a list of orders from the logged in user.
    /// </summary>
    /// <param name="orderParams">Order query.</param>
    /// <returns>A list of previous orders.</returns>
    /// <response code="200">User has previous orders and is logged in.</response>
    /// <response code="401">User is not logged in.</response>
    [Authorize]
    [HttpGet]
    [Produces(typeof(PaginationList<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PaginationList<OrderDto>>> GetOrders([FromQuery] OrderParams orderParams)
    {
        PaginationList<OrderDto> orderList = await _orderService.GetAllFromUserAsync(orderParams, User);
        return orderList;
    }

    // GET: /api/orders/{id}
    // TODO: Admin users can retrieve any order.
    /// <summary>
    /// Retrieve a previous order.
    /// </summary>
    /// <param name="id">The id of the order.</param>
    /// <returns>The order requested.</returns>
    /// <response code="200">Return order requested.</response>
    /// <response code="400">Invalid id type.</response>
    /// <response code="403">User does not have access to get an order that does not belong to them.</response>
    /// <response code="404">Order not found.</response>
    [Authorize]
    [HttpGet("{id}", Name = "GetOrder")]
    [Produces(typeof(OrderDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        ErrorOr<OrderDto> result = await _orderService.GetAsync(id, User);
        return result.Match(Ok, GetProblemResult);
    }

    // POST: /api/orders
    // TODO: Implement create order for guest users.
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <returns>The newly created order receipt.</returns>
    /// <response code="201">Order was created.</response>
    /// <response code="400">Invalid body values were entered or cart has no items</response>
    /// <response code="404">Cart was not found.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(OrderReceiptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderReceiptDto>> CreateOrder()
    {
        ErrorOr<OrderReceiptDto> result = await _orderService.CreateOrderAsync(User);
        return result.Match(
            receipt => CreatedAtAction(
                nameof(GetOrder),
                new { id = receipt.OrderNumber },
                receipt
            ),
            GetProblemResult);
    }
}