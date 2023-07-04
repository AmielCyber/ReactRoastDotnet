using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.API.Services;
using ReactRoastDotnet.Data.Common.Errors;
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
    /// <response code="200">Returns the user object along with a bearer token.</response>
    /// <response code="400">If user creds has invalid values.</response>
    /// <response code="401">If invalid credentials entered.</response>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [HttpPost("Login")]
    public async Task<ActionResult<LoggedInUserDto>> Login(UserLoginDto userLoginDto)
    {
        ErrorOr<User> result = await _userService.LoginAsync(userLoginDto);

        if (result.IsError)
        {
            return MapErrorsToProblemResult(result.Errors);
        }

        return await GetLoggedInUserAsync(result.Value);
    }

    // TODO: Redirect to Login in our client app.
    /// <summary>
    /// Registers a new user for our application.
    /// </summary>
    /// <param name="userRegisterDto">User credentials and information.</param>
    /// <returns>The new user object.</returns>
    /// <response code="201">Successful registration</response>
    /// <response code="400">If user creds has invalid input values.</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(UserRegisterDto userRegisterDto)
    {
        ErrorOr<UserDto> result = await _userService.RegisterAsync(userRegisterDto);
        return result.Match(userDto => CreatedAtAction(nameof(Login), userDto), MapErrorsToProblemResult);
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
        ErrorOr<UserDto> result = await _userService.GetCurrentUserAsync(User);
        return result.Match(Ok, MapErrorsToProblemResult);
    }

    private async Task<LoggedInUserDto> GetLoggedInUserAsync(User user)
    {
        // Generate token to serve client.
        var token = await _tokenService.GenerateToken(user);

        return new LoggedInUserDto(user.FirstName, user.LastName, user.Email, token);
    }

    private ActionResult MapErrorsToProblemResult(List<Error> errors)
    {
        Error firstError = errors[0];

        if (firstError.Type == ErrorType.Validation)
        {
            return MapValidationErrorToProblemResult(errors);
        }

        if ((int)firstError.Type == MyErrorTypes.Unauthorized)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: firstError.Description);
        }

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: firstError.Description);
    }

    private ActionResult MapValidationErrorToProblemResult(List<Error> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem();
    }
}