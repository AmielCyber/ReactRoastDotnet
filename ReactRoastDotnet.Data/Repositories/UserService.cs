using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using ReactRoastDotnet.Data.Common.Errors;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.User;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.Data.Repositories;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<User>> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.Email);

        // Validate user's creds.
        if (user is null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return Errors.User.Unauthorized("Invalid credentials.");
        }

        return user;
    }

    public async Task<ErrorOr<UserDto>> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        // Get user's info.
        var user = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            UserName = userRegisterDto.Email,
            DateCreated = DateTime.UtcNow
        };

        // Validate if email is not taken and password is valid, if so then create new user.
        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

        if (!result.Succeeded)
        {
            return result.Errors.Select(error => Errors.User.ValidationProblem(error.Code, error.Description)).ToList();
        }

        // Add new user role in our DB.
        await _userManager.AddToRoleAsync(user, UserRole.Name);

        return new UserDto(user.FirstName, user.LastName, user.Email);
    }

    // TODO: Remove from full production
    public async Task<ErrorOr<UserDto>> GetCurrentUserAsync(ClaimsPrincipal userClaim)
    {
        string email = userClaim?.Identity?.Name ?? string.Empty;
        var user = await _userManager.FindByEmailAsync(email);

        if (user?.Email is null)
        {
            return Errors.User.NotFound($"User with email: {email} not found.");
        }

        return new UserDto(user.FirstName, user.LastName, user.Email);
    }
}