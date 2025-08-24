using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Services;
using SageOwl.UI.ViewModels;

namespace SageOwl.UI.ViewComponents;

public class WorkspaceHeaderViewComponent : ViewComponent
{
    private readonly IUserService _userService;
    public WorkspaceHeaderViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string title,string url)
    {
        var token = HttpContext.Request.Cookies["AccessToken"];

        User? user = null;
        if (!string.IsNullOrEmpty(token))
        {
            user = await _userService.GetUserFromToken(token);
        }

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
