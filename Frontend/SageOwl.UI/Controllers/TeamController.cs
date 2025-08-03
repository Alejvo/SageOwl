using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

[Route("team/{teamId}")]
public class TeamController : Controller
{
    [HttpGet("mainpage")]
    public IActionResult MainPage(string teamId)
    {
        ViewBag.TeamId = teamId;
        return View();
    }

    [HttpGet("qualifications")]
    public IActionResult Qualifications(string teamId)
    {
        ViewBag.TeamId = teamId;
        return View();
    }

    [HttpGet("description")]
    public IActionResult Description(string teamId)
    {
        ViewBag.TeamId = teamId;
        return View();
    }

    [HttpGet("forms")]
    public IActionResult Forms(string teamId)
    {
        ViewBag.TeamId = teamId;
        return View();
    }

    [HttpGet("announcements")]
    public IActionResult Announcements(string teamId)
    {
        ViewBag.TeamId = teamId;
        return View();
    }
}
