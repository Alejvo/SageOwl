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

[Route("team")]
[AuthorizeToken]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;
    private readonly CurrentTeam _currentTeam;
    private readonly CurrentUser _currentUser;
    private readonly IAnnouncementService _announcementService;
    private readonly IQualificationService _qualificationService;

    public TeamController(
        ITeamService teamService,
        CurrentTeam currentTeam,
        IAnnouncementService announcementService,
        CurrentUser currentUser,
        IQualificationService qualificationService)
    {
        _teamService = teamService;
        _currentTeam = currentTeam;
        _announcementService = announcementService;
        _currentUser = currentUser;
        _qualificationService = qualificationService;
    }

    [HttpGet("{teamId}/mainpage")]
    public async Task<IActionResult> MainPage(Guid teamId)
    {
        await GetTeam(teamId);

        var teams = await _announcementService.GetAnnouncementsByTeamId(teamId);

        var teamsViewModel = teams.Select(a => new AnnouncementViewModel
        {
            Title = a.Title,
            Content = a.Content,
            PublisherName = a.Author,
            SentAt = a.CreatedAt
        }).ToList();
        var teamMainVM = new TeamMainViewModel
        {
            Announcements = teamsViewModel
        };
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("Teams", "Workspace");
        return View(teamMainVM);
    }

    [HttpGet("{teamId}/qualifications")]
    public async Task<IActionResult> Qualifications(Guid teamId)
    {
        ViewData["HeaderTitle"] = $"{_currentTeam.Name} Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        
         var qualifications = await _qualificationService.GetQualificationByTeamId(_currentTeam.TeamId);

        var qualification = qualifications.FirstOrDefault();

        QualificationViewModel qualificationVM = new QualificationViewModel
        {
            Descriptions = qualification.UserQualifications
                            .OrderBy(x => x.Position)   
                            .Select(x => x.Description)     
                            .Distinct()                     
                            .ToList(),
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

    [HttpGet("{teamId}/qualifications/save")]
    public async Task<IActionResult> SaveQualifications(Guid teamId)
    {

        await GetTeam(teamId);
        ViewData["HeaderTitle"] = $"Save Qualifications";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        var qualification = new SaveQualificationsViewModel();
        qualification.TeamId = _currentTeam.TeamId;
        qualification.Descriptions.Add("Grade 1");

        foreach (var item in _currentTeam.Members.Where(m => m.Role != "Admin"))
        {
            qualification.UserQualifications.Add(new SaveUserQualificationViewModel
            {
                UserId = item.Id,
                Name = item.Name + " " + item.Surname
            });
        }

        return View(qualification);
    }

    [HttpPost("qualifications/save")]
    public async Task<IActionResult> SaveQualifications(SaveQualificationsViewModel qualification)
    {
        if (ModelState.IsValid)
        {
            var newQualification = new SaveQualification
            {
                MaximumGrade = qualification.MaximumGrade,
                MinimumGrade = qualification.MinimumGrade,
                PassingGrade = qualification.PassingGrade,
                TotalGrades = qualification.TotalGrades,
                Period = qualification.Period,
                TeamId = qualification.TeamId,
                UserQualifications = qualification.UserQualifications
                .SelectMany(uq => uq.Grades.Select((grade, gIndex) => new SaveUserQualification
                {
                    UserId = uq.UserId,
                    Grade = grade,
                    Position = uq.Positions.Count > gIndex ? uq.Positions[gIndex] : 0,
                    Description = qualification.Descriptions.Count > gIndex ? qualification.Descriptions[gIndex] : string.Empty,
                    HasValue = true
                })).ToList()
            };

            foreach(var q in qualification.UserQualifications)
            {
                Console.WriteLine(q.UserId);
                foreach (var item in q.Positions)
                {
                    Console.WriteLine(item);
                }
            }

            await _qualificationService.SaveQualifications(newQualification);
            return RedirectToAction("MainPage", "Team", new { teamId = _currentTeam.TeamId });
        }
        return View(qualification);
    }

    [HttpGet("{teamId}/description")]
    public IActionResult Description(Guid teamId)
    {
        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        return View(model:_currentTeam.Description);
    }

    [HttpGet("{teamId}/forms")]
    public async Task<IActionResult> Forms(Guid teamId)
    {
        await GetTeam(teamId);

        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });

        var forms = new List<FormViewModel>();

        foreach (var item in _currentTeam.Forms)
        {
            var newform = new FormViewModel
            {
                Id = item.Id,
                Title= item.Title,
                Deadline = item.Deadline,
                TeamId = item.TeamId
            };
            forms.Add(newform);
        }
        return View(forms);
    }

    [HttpGet("{teamId}/announcements")]
    public async Task<IActionResult> Announcements(Guid teamId)
    {
        await GetTeam(teamId);

        ViewBag.TeamId = teamId;
        ViewData["HeaderTitle"] = $"{_currentTeam.Name}";
        ViewData["HeaderUrl"] = Url.Action("MainPage", "Team", new { teamId });
        var announcements = new List<AnnouncementViewModel>();

        foreach (var item in _currentTeam.Announcements)
        {
            var newAnnouncement = new AnnouncementViewModel
            {
                Content = item.Content,
                PublisherName = item.Author,
                SentAt = item.CreatedAt,
                Title = item.Title
            };
            announcements.Add(newAnnouncement);
        }

        return View(announcements);
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
        ViewData["HeaderTitle"] = "Create Announcement";
        ViewData["HeaderUrl"] = Url.Action("Teams", "Workspace");

        var newAnnouncement = new CreateAnnouncementViewModel
        {
            TeamId = teamId,
            AuthorId = _currentUser.Id
        };
        return View(newAnnouncement);
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

    [HttpGet("{teamId}/update")]
    public async Task<IActionResult> UpdateTeam(Guid teamId)
    {
        ViewData["HeaderTitle"] = $"Update: {_currentTeam.Name}";
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

    [HttpPost("update")]
    public async Task<IActionResult> UpdateTeam(UpdateTeamViewModel updateTeam)
    {
        if (ModelState.IsValid)
        {
            var updatedTeam = new UpdateTeamDto
            {
                TeamId = updateTeam.TeamId,
                Description = updateTeam.Description,
                Name = updateTeam.Name,
                Members = updateTeam.Members
                    .Select(m => new MemberViewModel { Role = m.Role, UserId = m.Id}).ToList()
            };

            await _teamService.UpdateTeam(updatedTeam);

            _currentTeam.Name = updatedTeam.Name;
            _currentTeam.Description = updatedTeam.Description;

            return RedirectToAction("MainPage", "Team", new { teamId = updatedTeam.TeamId });

        }
        return View(updateTeam);
    }

    private async Task GetTeam(Guid teamId)
    {
        if (_currentTeam.TeamId != teamId)
        {
            var team = await _teamService.GetTeamById(teamId);
            _currentTeam.Name = team.Name;
            _currentTeam.TeamId = team.TeamId;
            _currentTeam.Description = team.Description;
            _currentTeam.Announcements = team.Announcements;
            _currentTeam.Forms = team.Forms;
            _currentTeam.Members = team.Members;
        }
    }
}
