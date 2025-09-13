using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;

namespace SageOwl.UI.ViewComponents;

public class TeamNavigationViewComponent : ViewComponent
{
    private readonly CurrentTeam _currentTeam;
    private readonly CurrentUser _currentUser;

    public TeamNavigationViewComponent(CurrentTeam currentTeam, CurrentUser currentUser)
    {
        _currentTeam = currentTeam;
        _currentUser = currentUser;
    }

    public IViewComponentResult Invoke()
    {
        foreach (var member in _currentTeam.Members)
        {
            if (member.Id == _currentUser.Id)
            {
                if (member.Role == "Admin") _currentTeam.IsUserAdmin = true;
                else _currentTeam.IsUserAdmin = false;
                    break;
            }
        }

        return View(_currentTeam); 
    }
}
