using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Announcements;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Controllers;

[Route("team")]
[AuthorizeToken]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;
    private readonly CurrentTeam _currentTeam;
    public TeamController(ITeamService teamService,CurrentTeam currentTeam)
    {
        _teamService = teamService;
        _currentTeam = currentTeam;

    }

    [HttpGet("{teamId}/mainpage")]
    public async Task<IActionResult> MainPage(Guid teamId)
    {
        if (string.IsNullOrEmpty(_currentTeam.Name))
        {
            var team = await _teamService.GetTeamById(teamId);

            _currentTeam.Name = team.Name;
            _currentTeam.TeamId = team.TeamId;
            _currentTeam.Description = team.Description;
        }

        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("Teams", "Workspace");
        return View();
    }

    [HttpGet("{teamId}/qualifications")]
    public IActionResult Qualifications(Guid teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Team Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        return View();
    }

    [HttpGet("{teamId}/description")]
    public IActionResult Description(Guid teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        return View();
    }

    [HttpGet("{teamId}/forms")]
    public async Task<IActionResult> Forms(Guid teamId)
    {
        if (string.IsNullOrEmpty(_currentTeam.Name))
        {
            var team = await _teamService.GetTeamById(teamId);
            _currentTeam.Name = team.Name;
            _currentTeam.TeamId = team.TeamId;
            _currentTeam.Description = team.Description;
        }
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        return View();
    }

    [HttpGet("{teamId}/announcements")]
    public async Task<IActionResult> Announcements(Guid teamId)
    {
        if (string.IsNullOrEmpty(_currentTeam.Name))
        {
            var team = await _teamService.GetTeamById(teamId);
            _currentTeam.Name = team.Name;
            _currentTeam.TeamId = team.TeamId;
            _currentTeam.Description = team.Description;
        }
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
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

    [HttpGet("{teamId}/announcements/create")]
    public IActionResult CreateAnnouncement(Guid teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Create Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpPost("announcements/create")]
    public IActionResult CreateAnnouncement(CreateAnnouncementViewModel createAnnouncement)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(createAnnouncement);
    }
}
