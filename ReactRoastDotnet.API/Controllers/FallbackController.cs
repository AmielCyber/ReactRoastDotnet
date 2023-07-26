using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactRoastDotnet.API.Controllers;

/// <summary>
/// Base class for an MVC controller with view. Effectively a view for our client application (React App). 
/// </summary>
// No authentication
[AllowAnonymous]
public class FallbackController : Controller
{
    public IActionResult Index()
    {
        // Serve our client application (React).
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}