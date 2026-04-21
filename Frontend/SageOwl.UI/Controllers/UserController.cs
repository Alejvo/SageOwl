using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Users;

namespace SageOwl.UI.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly CurrentUser _currentUser;

    public UserController(IUserService userService, CurrentUser currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var users = await _userService.GetUsers(1, 10, searchTerm, null, null);
   
        return PartialView("~/Views/Shared/PartialViews/_UserList.cshtml", users);
    }

    public async Task<IActionResult> Update(UpdateUserViewModel user)
    {
        if (ModelState.IsValid)
        {
            await _userService.Update(user);
            return RedirectToAction("Index", "Home");
        }
        return View(user);
    }

    public async Task<IActionResult> Delete()
    {
        if (ModelState.IsValid)
        {
            await _userService.DeleteUser(_currentUser.Id);
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

}
