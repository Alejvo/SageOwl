using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;

namespace SageOwl.UI.ViewComponents;

public class TeamNavigationViewComponent : ViewComponent
{
    private readonly CurrentTeam _currentTeam;

    public TeamNavigationViewComponent(CurrentTeam currentTeam)
    {
        _currentTeam = currentTeam;
    }

    public IViewComponentResult Invoke()
    {
        return View(_currentTeam); 
    }
}
