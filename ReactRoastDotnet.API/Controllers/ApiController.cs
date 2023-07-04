using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using ReactRoastDotnet.Data.Common.Errors;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// Main controller that all controllers in this application will inherit from.
/// </summary>
[ApiController]
[Route("Api/[controller]")]
public class ApiController : ControllerBase
{
    protected ActionResult GetProblemResult(List<Error> errors)
    {
        Error firstError = errors[0];

        if (firstError.Type is ErrorType.Validation)
        {
            return MapValidationProblem(errors);
        }

        int statusCode = GetProblemStatusCode(firstError);

        return Problem(statusCode: statusCode, detail: firstError.Description);
    }

    private ActionResult MapValidationProblem(List<Error> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem();
    }

    private int GetProblemStatusCode(Error error)
    {
        return error.NumericType switch
        {
            (int)ErrorType.NotFound => StatusCodes.Status404NotFound,
            MyErrorTypes.BadRequest => StatusCodes.Status400BadRequest,
            MyErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            MyErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}