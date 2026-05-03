using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("404")]
    public IActionResult NotFound()
    {
        return View();
    }
}
