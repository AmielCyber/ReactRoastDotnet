using Microsoft.AspNetCore.Mvc;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// Main controller that all controllers in this application will inherit from.
/// </summary>
[ApiController]
[Route("Api/[controller]")]
public class ApiController : ControllerBase
{
}