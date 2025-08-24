using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;

namespace SageOwl.UI.Controllers;

[AuthorizeToken]
public class WorkspaceController : Controller
{
    public IActionResult Index()
    {
        ViewData["HeaderTitle"] = "Recent Activity";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    public IActionResult Forms()
    {
        ViewData["HeaderTitle"] = "Forms";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    public IActionResult Announcements() 
    {
        ViewData["HeaderTitle"] = "Announcements";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    public IActionResult Teams()
    {
        ViewData["HeaderTitle"] = "Teams";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    public IActionResult Qualifications()
    {
        ViewData["HeaderTitle"] = "Qualifications";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }
}
