using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;

namespace SageOwl.UI.Controllers;

//[AuthorizeToken]
public class WorkspaceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
