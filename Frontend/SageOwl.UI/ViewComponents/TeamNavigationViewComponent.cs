using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Models.Users;
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

    public async Task<IViewComponentResult> InvokeAsync(Guid teamId, string url)
    {
        var team = await _teamService.GetTeamById(teamId);

        var isAdmin = team.Members.Any(m =>
            m.Id == _currentUser.Id!.Value &&
            m.Role == "Admin");

        var teamVM = new GetTeamViewModel
        {
            TeamId = teamId,
            Name = team.Name,
            IsAdmin = isAdmin,
            Url = url
        };

        return View(teamVM); 
    }
}
