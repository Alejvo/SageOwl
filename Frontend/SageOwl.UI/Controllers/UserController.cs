using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Users;

namespace SageOwl.UI.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var users = await _userService.GetUsers(1, 10, searchTerm, null, null);
   
        return PartialView("~/Views/Shared/PartialViews/_UserList.cshtml", users);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateUserViewModel user)
    {
        if (ModelState.IsValid)
        {
            await _userService.Update(user);
            return RedirectToAction("Index", "Home");
        }
        return View(user);
    }
}
