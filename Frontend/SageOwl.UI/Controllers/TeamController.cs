using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Models.Qualifications;
using SageOwl.UI.Models.Users;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Announcements;
using SageOwl.UI.ViewModels.Forms;
using SageOwl.UI.ViewModels.Qualifications;
using SageOwl.UI.ViewModels.Teams;
using SageOwl.UI.ViewModels.Teams.UI;

namespace SageOwl.UI.Controllers;

[Route("[controller]")]
[AuthorizeToken]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;
    private readonly CurrentUser _currentUser;
    private readonly IAnnouncementService _announcementService;
    private readonly IQualificationService _qualificationService;
    private readonly IFormService _formService;
    private CurrentQualifications _currentQualifications;

    public TeamController(
        ITeamService teamService,
        IAnnouncementService announcementService,
        CurrentUser currentUser,
        IQualificationService qualificationService,
        IFormService formService,
        CurrentQualifications currentQualifications)
    {
        _teamService = teamService;
        _announcementService = announcementService;
        _currentUser = currentUser;
        _qualificationService = qualificationService;
        _formService = formService;
        _currentQualifications = currentQualifications;
    }

    // GET Method
    [HttpGet("{teamId}/mainpage")]
    public async Task<IActionResult> MainPage(Guid teamId)
    {
        var teams = await _teamService.GetTeamById(teamId);
        var forms = await _formService.GetFormsByTeamId(teamId);
        var announcements = await _announcementService.GetAnnouncementsByTeamId(teamId);

        var teamsViewModel = announcements.Select(a => new AnnouncementViewModel
        {
            Title = a.Title,
            Content = a.Content,
            PublisherName = a.Author,
            SentAt = a.CreatedAt
        }).ToList();

        var teamMainVM = new MainPageViewModel
        {
            TeamId = teamId,
            Announcements = teamsViewModel,
            Forms = [.. forms.Select(f => new FormViewModel
            {
                Id = f.Id,
                Deadline = f.Deadline,
                TeamId = f.TeamId,
                Title = f.Title
            })]
        };

        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{teams.Name}";
        ViewData["HeaderUrl"] = Url.Action("Teams", "Workspace");
        return View(teamMainVM);
    }

    [HttpGet("{teamId}/forms")]
    public async Task<IActionResult> Forms(Guid teamId)
    {
        var team = await _teamService.GetTeamById(teamId);
        var forms = team.Forms.Select(f => new FormViewModel
        {
            Id = f.Id,
            Title = f.Title,
            Deadline = f.Deadline,
            TeamId = f.TeamId
        }).ToList();

        ViewData["HeaderTitle"] = $"{team.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        return View(new TeamFormsPageViewModel
        {
            Forms = forms,
            TeamId = teamId
        });
    }

    [HttpGet("{teamId}/qualifications")]
    public async Task<IActionResult> Qualifications(Guid teamId)
    {
        _currentQualifications.Qualifications = 
            await _qualificationService.GetQualificationByTeamId(teamId);

        var qualificationList = new TeamQualificationsPageViewModel
        {
            TeamId = teamId,
            Qualifications = _currentQualifications.Qualifications.Select(q => new QualificationViewModel
            {
                QualificationId = q.Id,
                Period = q.Period,
                TotalGrades = q.TotalGrades,
                Descriptions = q.UserQualifications.Select(x => x.Description).Distinct().ToList(),
                UserQualifications = q.UserQualifications
                .GroupBy(uq => new { uq.UserId, uq.Name })
                .Select(g => new UserQualificationViewModel
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Grades = g.Select(x => x.Grade).ToList(),
                }).ToList()
            }).ToList()
        };

        foreach (var item in _currentQualifications.Qualifications)
        {
            qualificationList.QualificationKeys.Add(item.Id,item.Period);
        }

        ViewData["HeaderTitle"] = $"Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        return View(qualificationList);
    }

    [HttpGet("{teamId}/qualifications/save", Name = "SaveQualifications")]
    public async Task<IActionResult> SaveQualifications(Guid teamId)
    {
        ViewData["HeaderTitle"] = $"Save Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        var qualification = new SaveQualificationsViewModel();
        qualification.TeamId = teamId;
        qualification.Descriptions.Add("Grade 1");

        var team = await _teamService.GetTeamById(teamId);

        foreach (var item in team.Members.Where(m => m.Role != "Admin"))
        {
            qualification.UserQualifications.Add(new SaveUserQualificationViewModel
            {
                UserId = item.Id,
                Name = item.Name + " " + item.Surname
            });
        }

        return View(qualification);
    }

    [HttpGet("{teamId}/description")]
    public async Task<IActionResult> Description(Guid teamId)
    {
        var team = await _teamService.GetTeamById(teamId);

        ViewData["HeaderTitle"] = $"{team.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        return View(new TeamDescriptionViewModel
        {
            TeamId = team.TeamId,
            Description = team.Description
        });
    }

    [HttpGet("{teamId}/announcements")]
    public async Task<IActionResult> Announcements(Guid teamId)
    {
        var team = await _teamService.GetTeamById(teamId);
        var announcements = team.Announcements.Select(a => new AnnouncementViewModel
        {
            Content = a.Content,
            PublisherName = a.Author,
            SentAt = a.CreatedAt,
            Title = a.Title
        }).ToList();

        ViewData["HeaderTitle"] = $"{team.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        return View(new TeamAnnouncementsPageViewModel
        {
            Announcements = announcements,
            TeamId = teamId
        });
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["HeaderTitle"] = "Create Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");
        return View();
    }

    [HttpGet("{teamId}/update")]
    public async Task<IActionResult> UpdateTeam(Guid teamId)
    {
        ViewData["HeaderTitle"] = $"Update Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        var team = await _teamService.GetTeamById(teamId);

        var updateTeam = new UpdateTeamViewModel
        {
            TeamId = team.TeamId,
            Name = team.Name,
            Description = team.Description,
            Members = team.Members
        };

        return View(updateTeam);
    }

    [HttpGet("{teamId}/announcements/create")]
    public IActionResult CreateAnnouncement(Guid teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = "Create Announcement";
        ViewData["HeaderUrl"] = Url.Action("Teams", "Workspace");

        var newAnnouncement = new CreateAnnouncementViewModel
        {
            TeamId = teamId,
            AuthorId = _currentUser.Id!.Value
        };
        return View(newAnnouncement);
    }

    // POST Method
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateTeamViewModel createTeam)
    {
        ViewData["HeaderTitle"] = "Create Team";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team");

        await _teamService.CreateTeam(createTeam);

        if (ModelState.IsValid)
        {
            return RedirectToAction("Teams", "Workspace");
        }

        return View(createTeam);
    }

    [HttpPost("announcements/create")]
    public async Task<IActionResult> CreateAnnouncement(CreateAnnouncementViewModel createAnnouncement)
    {
        if (ModelState.IsValid)
        {
            await _announcementService.CreateAnnouncement(createAnnouncement);

            return RedirectToAction("MainPage", "Team", new { teamId = createAnnouncement.TeamId });
        }

        return View(createAnnouncement);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateTeam(UpdateTeamViewModel updateTeam)
    {
        if (ModelState.IsValid)
        {
            var team = await _teamService.GetTeamById(updateTeam.TeamId);

            var updatedTeam = new UpdateTeamDto
            {
                TeamId = updateTeam.TeamId,
                Description = updateTeam.Description,
                Name = updateTeam.Name,
                Members = updateTeam.Members
                    .Select(m => new MemberViewModel { Role = m.Role, UserId = m.Id}).ToList()
            };

            await _teamService.UpdateTeam(updatedTeam);

            return RedirectToAction("MainPage", "Team", new { teamId = updatedTeam.TeamId });

        }
        return View(updateTeam);
    }

}
