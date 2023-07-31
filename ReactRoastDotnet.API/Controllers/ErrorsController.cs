using Microsoft.AspNetCore.Mvc;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Get rid of for production when deployed with React.
/// <summary>
/// Test controller for exception handling in the client app. 
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ApiController
{
    [HttpGet("Not-Found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("Bad-Request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("THIS IS A BAD REQUEST");
    }

    [HttpGet("Unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("Validation-Error")]
    public ActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem 1", "First error.");
        ModelState.AddModelError("Problem 2", "Second error.");

        return ValidationProblem();
    }

    [HttpGet("Server-Error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a test error.");
    }
    [HttpGet("Custom-Problem")]
    public ActionResult GetCustomProblem()
    {
        return Problem(statusCode: 401, detail:"Detail goes here...");
    }
}