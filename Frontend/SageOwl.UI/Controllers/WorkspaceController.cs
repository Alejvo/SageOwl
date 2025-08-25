using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Implementations;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels;

namespace SageOwl.UI.Controllers;

[AuthorizeToken]
public class WorkspaceController : Controller
{
    private readonly ITeamService _teamService;

    public WorkspaceController(ITeamService teamService)
    {
        _teamService = teamService;
    }

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

    public async Task<IActionResult> Teams()
    {
        ViewData["HeaderTitle"] = "Teams";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        var token = HttpContext.Request.Cookies["AccessToken"];

        var teams = await _teamService.GetTeamsByUserToken(token);

        var teamsViewModel = teams.Select(t => new TeamCardViewModel
        {
            Initials = t.Name.Substring(0,2),
            Name=t.Name
        }).ToList();
        return View(teamsViewModel);
    }

    public IActionResult Qualifications()
    {
        ViewData["HeaderTitle"] = "Qualifications";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }
}
