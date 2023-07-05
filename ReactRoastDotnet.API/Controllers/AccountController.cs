using System.Net.Mime;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.API.Services;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.User;
using ReactRoastDotnet.Data.Repositories;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// User Login and Registration controller.
/// </summary>
public class AccountController : ApiController
{
    private readonly TokenService _tokenService;
    private readonly IUserService _userService;

    public AccountController(IUserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    // TODO: Maybe sent cart too?
    // GET: api/account/login
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="userLoginDto">User credentials needed to login.</param>
    /// <returns>User object including the token to access this app's services.</returns>
    /// <response code="200">Returns the user logged-in object along with a bearer token.</response>
    /// <response code="400">If invalid types for password or email are entered.</response>
    /// <response code="401">If user's credentials are invalid.</response>
    [HttpPost("Login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(LoggedInUserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoggedInUserDto>> Login(UserLoginDto userLoginDto)
    {
        ErrorOr<User> result = await _userService.LoginAsync(userLoginDto);

        if (result.IsError)
        {
            return GetProblemResult(result.Errors);
        }

        return await GetLoggedInUserAsync(result.Value);
    }

    // TODO: Redirect to Login in our client app.
    /// <summary>
    /// Registers a new user for this application.
    /// </summary>
    /// <param name="userRegisterDto">User's credentials and information.</param>
    /// <returns>The new user object.</returns>
    /// <response code="201">Successfully register user to this application.</response>
    /// <response code="400">If user credentials has invalid input values.</response>
    [HttpPost("Register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Register(UserRegisterDto userRegisterDto)
    {
        ErrorOr<UserDto> result = await _userService.RegisterAsync(userRegisterDto);
        return result.Match(userDto => CreatedAtAction(nameof(Login), userDto), GetProblemResult);
    }

    /// <summary>
    /// Gets a user object with the access token.
    /// </summary>
    /// <param name="user">User object to add token.</param>
    /// <returns>User object with access token.</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    private async Task<LoggedInUserDto> GetLoggedInUserAsync(User user)
    {
        // Generate token to serve client.
        var token = await _tokenService.GenerateToken(user);

        if (user.Email is null)
        {
            throw new Exception("User email is not defined.");
        }
        
        return new LoggedInUserDto(user.FirstName, user.LastName, user.Email, token);
    }
}