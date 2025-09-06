using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Controllers;

[Route("team")]
[AuthorizeToken]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet("{teamId}/mainpage")]
    public IActionResult MainPage(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Teams";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    [HttpGet("{teamId}/qualifications")]
    public IActionResult Qualifications(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("{teamId}/description")]
    public IActionResult Description(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Description";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("{teamId}/forms")]
    public IActionResult Forms(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Forms";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("{teamId}/announcements")]
    public IActionResult Announcements(string teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Announcements";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["HeaderTitle"] = "Create Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateTeamViewModel createTeam)
    {
        ViewData["HeaderTitle"] = "Create Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");

        await _teamService.CreateTeam(createTeam);

        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(createTeam);
    }
}
