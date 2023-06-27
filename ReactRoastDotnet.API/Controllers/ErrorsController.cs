using Microsoft.AspNetCore.Mvc;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Get rid of for production
// Test controller for exception handling in the client app.
[ApiController]
[Route("api/[controller]")]
public class ErrorsController: ControllerBase
{
    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }
    
    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request.");
    }
    
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }
    
    [HttpGet("validation-error")]
    public ActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem 1", "First error.");
        ModelState.AddModelError("Problem 2", "Second error.");

        return ValidationProblem();
    }
    
    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a test error.");
    }
}