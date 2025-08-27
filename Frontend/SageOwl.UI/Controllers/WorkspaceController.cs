using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Implementations;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels;
using SageOwl.UI.ViewModels.Announcements;
using System.Threading.Tasks;

namespace SageOwl.UI.Controllers;

[AuthorizeToken]
public class WorkspaceController : Controller
{
    private readonly ITeamService _teamService;
    private readonly IAnnouncementService _announcementService;

    public WorkspaceController(ITeamService teamService, IAnnouncementService announcementService)
    {
        _teamService = teamService;
        _announcementService = announcementService;
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

    public async Task<IActionResult> Announcements() 
    {
        ViewData["HeaderTitle"] = "Announcements";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");

        var announcements = await _announcementService.GetAnnouncements();

        var announcementsViewModel = announcements.Select(a => new AnnouncementViewModel
        {
            Content = a.Content,
            Title = a.Title,
            SentAt = a.CreatedAt,
            PublisherName = a.Author
        }).ToList();
        return View(announcementsViewModel);
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
