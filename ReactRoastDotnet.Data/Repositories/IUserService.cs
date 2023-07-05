using System.Security.Claims;
using ErrorOr;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.User;

namespace ReactRoastDotnet.Data.Repositories;

public interface IUserService
{
    public Task<ErrorOr<User>> LoginAsync(UserLoginDto userLoginDto);

    public Task<ErrorOr<UserDto>> RegisterAsync(UserRegisterDto userRegisterDto);

    // TODO: Get rid of for full production.
    public Task<ErrorOr<UserDto>> GetCurrentUserAsync(ClaimsPrincipal user);
}