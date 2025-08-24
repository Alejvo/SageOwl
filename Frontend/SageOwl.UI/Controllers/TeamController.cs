using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

[Route("team/{teamId}")]
public class TeamController : Controller
{
    [HttpGet("mainpage")]
    public IActionResult MainPage(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Teams";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    [HttpGet("qualifications")]
    public IActionResult Qualifications(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("description")]
    public IActionResult Description(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Description";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("forms")]
    public IActionResult Forms(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Forms";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("announcements")]
    public IActionResult Announcements(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Announcements";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }
}
