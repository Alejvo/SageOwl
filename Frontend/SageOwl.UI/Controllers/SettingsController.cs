using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        ViewData["HeaderTitle"] = "Settings";
        return View();
    }
}
