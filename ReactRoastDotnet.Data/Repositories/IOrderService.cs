using System.Security.Claims;
using ErrorOr;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;
using ReactRoastDotnet.Data.Models.Pagination;
using ReactRoastDotnet.Data.RequestParams;

namespace ReactRoastDotnet.Data.Repositories;

public interface IOrderService
{
    public Task<PaginationList<OrderDto>> GetAllFromUserAsync(OrderParams orderParams, ClaimsPrincipal user);
    public Task<ErrorOr<OrderDto>> GetAsync(int id, ClaimsPrincipal user);
    public Task<ErrorOr<OrderReceiptDto>> CreateOrderAsync(ClaimsPrincipal user);
}