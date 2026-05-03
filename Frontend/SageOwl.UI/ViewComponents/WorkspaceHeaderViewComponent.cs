using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.UI;

namespace SageOwl.UI.ViewComponents;

public class WorkspaceHeaderViewComponent : ViewComponent
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    public WorkspaceHeaderViewComponent(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string title,string url)
    {
        var accessToken = _authService.GetAccessToken();

        var user = await _userService.GetUserFromToken(accessToken);

        var model = new WorkspaceHeaderViewModel
        {
            Title = title,
            Url = url,
            Tooltip = "Ir al workspace",
            ProfileInfo = new ProfileInfoViewModel
            {
                Name = $"{user.Name} {user.Surname}",
                Username = user.Username,
            }
        };

        return View(model);
    }
}
