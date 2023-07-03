using System.Security.Claims;
using ErrorOr;
using ReactRoastDotnet.Data.Models.Cart;

namespace ReactRoastDotnet.Data.Repositories;

public interface ICartService
{
    public Task<ErrorOr<CartDto>> GetCartAsync(ClaimsPrincipal user);
    public Task<ErrorOr<CartDto>> AddProductItemAsync(ClaimsPrincipal user, int id, int quantity = 1);
    public Task<ErrorOr<Deleted>> RemoveProductItemAsync(ClaimsPrincipal user, int id, int quantity = 1);
}