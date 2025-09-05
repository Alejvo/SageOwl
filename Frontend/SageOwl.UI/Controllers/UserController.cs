using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using System.Threading.Tasks;

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
}
