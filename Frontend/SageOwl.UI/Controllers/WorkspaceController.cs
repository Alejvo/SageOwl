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

    public IActionResult Forms()
    {
        return View();
    }

    public IActionResult Announcements() 
    {
        return View();
    }

    public IActionResult Teams()
    {
        return View();
    }

    public IActionResult Qualifications()
    {
        return View();
    }
}
