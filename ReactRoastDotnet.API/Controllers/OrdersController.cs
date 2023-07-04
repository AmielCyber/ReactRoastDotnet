using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Common.Errors;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.Repositories;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.API.Controllers;

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
        PaginationList<OrderDto> orderList = await _orderService.GetAllFromUserAsync(paginationParams, User);
        return orderList;
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
        ErrorOr<OrderDto> result = await _orderService.GetAsync(id, User);
        return result.Match(Ok, MapErrorsToProblemResult);
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
        ErrorOr<OrderReceiptDto> result = await _orderService.CreateOrderAsync(User);
        return result.Match(
            receipt => CreatedAtAction(
                nameof(GetOrder),
                new { id = receipt.OrderNumber },
                receipt
            ),
            MapErrorsToProblemResult);
    }

    private ActionResult MapErrorsToProblemResult(List<Error> errors)
    {
        Error firstError = errors[0];

        if ((int)firstError.Type == MyErrorTypes.Forbidden)
        {
            return Problem(statusCode: StatusCodes.Status403Forbidden, detail: firstError.Description);
        }
        if ((int)firstError.Type == MyErrorTypes.BadRequest)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: firstError.Description);
        }

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: firstError.Description);
    }
}