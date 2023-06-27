using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.API.Models.RequestDto;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Implement an AuthService
// TODO: Implement ResponseDto
[ApiController]
[Route("api/auth/")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return Unauthorized();
        }

        return user;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var user = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email
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
}