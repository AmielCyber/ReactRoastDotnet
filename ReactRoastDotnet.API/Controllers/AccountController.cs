using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.API.Models.RequestDto;
using ReactRoastDotnet.API.Models.ResponseDto;
using ReactRoastDotnet.API.Services;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Implement an AuthService
[ApiController]
[Route("api/auth/")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<User> userManager, TokenService tokenService, AppDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return Unauthorized();
        }

        var token = await _tokenService.GenerateToken(user);

        return new UserDto(user.FirstName, user.LastName, user.Email, token);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var user = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            UserName = userRegisterDto.Email,
            DateCreated = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        await _userManager.AddToRoleAsync(user, UserRole.Name);

        return StatusCode(201);
    }

    [Authorize]
    [HttpGet("currentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        string email = User?.Identity?.Name ?? string.Empty;
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return NotFound();
        }
        
        var token = await _tokenService.GenerateToken(user);
        return new UserDto(user.FirstName, user.LastName, user.Email, token);
    }
}