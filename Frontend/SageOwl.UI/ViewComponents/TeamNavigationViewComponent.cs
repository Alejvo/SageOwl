using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.ViewComponents;

public class TeamNavigationViewComponent : ViewComponent
{
    private readonly CurrentUser _currentUser;
    private readonly ITeamService _teamService;

    public TeamNavigationViewComponent( CurrentUser currentUser,ITeamService teamService)
    {
        _currentUser = currentUser;
        _teamService = teamService;
    }

    public async Task<IViewComponentResult> Invoke(Guid teamId)
    {
        var team = await _teamService.GetTeamById(teamId);

        var isAdmin = team.Members.Any(m =>
            m.Id == _currentUser.Id &&
            m.Role == "Admin");

        var teamVM = new GetTeamViewModel
        {
            TeamId = teamId,
            Members = team.Members,
            Announcements = team.Announcements,
            Description = team.Description,
            Forms = team.Forms,
            Name = team.Name,
            IsAdmin = isAdmin
        };

        return View(teamVM); 
    }
}
