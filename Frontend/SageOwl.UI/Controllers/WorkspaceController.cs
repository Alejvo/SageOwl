using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Models.Qualifications;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Announcements;
using SageOwl.UI.ViewModels.Forms;
using SageOwl.UI.ViewModels.Qualifications;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Controllers;

[AuthorizeToken]
public class WorkspaceController : Controller
{
    private readonly ITeamService _teamService;
    private readonly IAnnouncementService _announcementService;
    private readonly IFormService _formService;
    private readonly IQualificationService _qualificationService;
    private readonly CurrentUser _currentUser;

    public WorkspaceController(ITeamService teamService, IAnnouncementService announcementService,IFormService formService, IQualificationService qualificationService,CurrentUser currentUser)
    {
        _teamService = teamService;
        _announcementService = announcementService;
        _formService = formService;
        _qualificationService = qualificationService;
        _currentUser = currentUser;
    }

    public IActionResult Index()
    {
        ViewData["HeaderTitle"] = "Recent Activity";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    public async Task<IActionResult> Forms()
    {
        ViewData["HeaderTitle"] = "Forms";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");

        var forms = await _formService.GetFormsByUserId();

        var formsViewModel = forms.Select(f => new FormViewModel
        {
            Id = f.Id,
            Title = f.Title,
            TeamId = f.TeamId,
            Deadline = f.Deadline
        }).ToList();

        return View(formsViewModel);
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

        var teams = await _teamService.GetTeamsByUser();

        var teamsViewModel = teams.Select(t => new TeamCardViewModel
        {
            TeamId = t.TeamId,
            Initials = t.Name.Substring(0,2),
            Name=t.Name
        }).ToList();

        return View(teamsViewModel);
    }

    public async Task<IActionResult> Qualifications()
    {
        ViewData["HeaderTitle"] = "My Qualifications";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");

        var qualifications = await _qualificationService.GetQualificationByUserId(_currentUser.Id);

        var qualification = qualifications.FirstOrDefault();

        QualificationViewModel qualificationVM = new QualificationViewModel
        {
            Descriptions = qualification.UserQualifications.Select(x => x.Description).ToList(),
            Period = qualification.Period,
            TotalGrades = qualification.TotalGrades,
            PeriodList = qualifications.Select(x => x.Period).ToList(),
            UserQualifications = qualification.UserQualifications
                .GroupBy(uq => new { uq.UserId, uq.Name })
                .Select(g => new UserQualificationViewModel
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Grades = g.OrderBy(x => x.Position).Select(x => x.Grade).ToList(),
                    Positions = g.OrderBy(x => x.Position).Select(x => x.Position).ToList()
                }).ToList()
        };
            
        return View(qualificationVM);
    }

}
