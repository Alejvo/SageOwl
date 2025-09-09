﻿using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels;

namespace SageOwl.UI.ViewComponents;

public class WorkspaceHeaderViewComponent : ViewComponent
{
    private readonly IUserService _userService;
    private readonly CurrentUser _currentUser;
    public WorkspaceHeaderViewComponent(IUserService userService, CurrentUser currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    public async Task<IViewComponentResult> InvokeAsync(string title,string url)
    {
        var token = HttpContext.Request.Cookies["AccessToken"];

        User? user = null;
        if (!string.IsNullOrEmpty(token))
        {
            user = await _userService.GetUserFromToken(token);
            _currentUser.Id = user.Id;
            _currentUser.Name = user.Name;
            _currentUser.Email = user.Email;
            _currentUser.Surname = user.Surname;
            _currentUser.CreatedAt = user.CreatedAt;
            _currentUser.Username = user.Username;
        }



        var model = new WorkspaceHeaderViewModel
        {
            Title = title,
            Url = url,
            Tooltip = "Ir al workspace",
            ProfileInfo = new ProfileInfoViewModel
            {
                Name = $"{_currentUser.Name} {_currentUser.Surname}",
                Username = _currentUser.Username,
            }

        };

        return View(model);
    }
}
