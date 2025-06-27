using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services;
using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid) 
        { 
            await _userService.Create(register);
            return RedirectToAction("Index","Home");
        }
        return View(register);
    }
}
