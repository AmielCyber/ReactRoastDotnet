using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.API.Services;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.User;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Refactor to Implement an AccountService.
/// <summary>
/// User Login and Registration controller.
/// </summary>
public class AccountController : ApiController
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    // TODO: Maybe sent cart too?
    // GET: api/account/login
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="userLoginDto">User credentials needed to login.</param>
    /// <returns>User object including the token to access this app's services.</returns>
    /// <response code="200">Returns the user object along with a bearer token.</response>
    /// <response code="400">If user creds has invalid values.</response>
    /// <response code="401">If invalid credentials entered.</response>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.Email);

        // Validate user's creds.
        if (user is null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return Unauthorized();
        }

        // Generate token to serve client.
        var token = await _tokenService.GenerateToken(user);

        return Ok(new UserDto(user.FirstName, user.LastName, user.Email, token));
    }

    // TODO: Redirect to Login in our client app.
    /// <summary>
    /// Registers a new user for our application.
    /// </summary>
    /// <param name="userRegisterDto">User credentials and information.</param>
    /// <returns>TODO</returns>
    /// <response code="201">TODO</response>
    /// <response code="400">If user creds has invalid input values.</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost("Register")]
    public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
    {
        // Get user's info.
        var user = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            UserName = userRegisterDto.Email,
            DateCreated = DateTime.Now
        };

        // Validate if email is not taken and password is valid, if so then create new user.
        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
        {
            // Return a list of invalid inputs. 
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        // Add new user role in our DB.
        await _userManager.AddToRoleAsync(user, UserRole.Name);

        return StatusCode(201);
    }

    // TODO: Get rid of for production.
    /// <summary>
    /// Test identity core controller.
    /// </summary>
    /// <returns>A user object.</returns>
    [Authorize]
    [HttpGet("Current-User")]
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